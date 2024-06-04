using System;
using System.Collections;
using System.ComponentModel;
using System.Numerics;
using Dacodelaac.Core;
using Dacodelaac.Events;
using Dacodelaac.Variables;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEditor.iOS;
using UnityEditorInternal;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class Player : BaseMono, IKitchenObjectParent
{
    public static Player Instance { get; set; }
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;

    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float dashSpeed = 22f;
    [SerializeField] private float dashDistanceMax = 5f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask counterLayerMask;
    [SerializeField] private Transform holdKitchenObjectPoint;
    [SerializeField] private Transform dashParticlePoint;
    [SerializeField] private float playerRadius = 0.7f;
    [SerializeField] private float playerHeight = 2f;
    [SerializeField] private ParticleSystem dashParticle;
    [SerializeField] protected PlayAudioEvent playAudioEvent;
    [SerializeField] private AudioClip[] footStepAudios;
    [SerializeField] private AudioClip[] dashAudios;
    [SerializeField] private InputHandlerDataVariable inputHandlerData;

    private bool _isWalking;
    private Vector3 _lastInteractDir;
    private Vector3 _dashDir;
    private BaseCounter _selectedCounter;
    private KitchenObject _kitchenObject;
    private bool _isDashing;
    private float _dashDistance;
    private float _dashCoolDown;
    private float footStepTimer;
    private float footStepTimerMax = 0.2f;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        gameInput.OnInteractAction += GameInputOnOnInteractAction;
        gameInput.OnInteractAlternateAction += GameInputOnOnInteractAlternateAction;
        gameInput.OnDashAction += GameInputOnOnDashAction;
    }

    public override void Tick()
    {
        base.Tick();
        HandleMovement();
        HandleInteract();
        HandleDash();
    }

    private void GameInputOnOnInteractAlternateAction(object sender, EventArgs e)
    {
        if (!KitchenGameManager.Instance.IsGamePlaying()) return;
        if (_selectedCounter != null)
        {
            _selectedCounter.InteractAlternate(this);
        }
    }

    private void GameInputOnOnInteractAction(object sender, EventArgs e)
    {
        if (!KitchenGameManager.Instance.IsGamePlaying()) return;
        if (_selectedCounter != null)
        {
            _selectedCounter.Interact(this);
        }
    }

    private void GameInputOnOnDashAction(object sender, EventArgs e)
    {
        if (!KitchenGameManager.Instance.IsGamePlaying()) return;
        if (_isDashing) return;
        var particle = pools.Spawn(dashParticle);
        particle.transform.position = dashParticlePoint.position;
        particle.transform.forward = _dashDir;
        particle.GetComponent<ParticleSystem>().Play();
        _isDashing = true;
        _dashDistance = 0;
        playAudioEvent.RaiseRandom(dashAudios);
    }

    void HandleInteract()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalize();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDir == Vector3.zero)
            moveDir = new Vector3(inputHandlerData.Value.Direction.x, 0f, inputHandlerData.Value.Direction.y).normalized;

        if (moveDir != Vector3.zero)
        {
            _lastInteractDir = moveDir;
        }

        float interactDistance = 2f;
        if (Physics.Raycast(transform.position, _lastInteractDir, out RaycastHit raycastHit, interactDistance,
                counterLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                SetSelectedCounter(baseCounter);
            }
            else SetSelectedCounter(null);
        }
        else SetSelectedCounter(null);
    }

    void HandleMovement()
    {
        if (!KitchenGameManager.Instance.IsGamePlaying()) return;

        if (_isDashing) return;

        Vector2 inputVector = gameInput.GetMovementVectorNormalize();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDir == Vector3.zero)
            moveDir = new Vector3(inputHandlerData.Value.Direction.x, 0f, inputHandlerData.Value.Direction.y)
                .normalized;

        float moveDistance = moveSpeed * Time.deltaTime;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight,
            playerRadius, moveDir, moveDistance);

        if (!canMove)
        {
            Vector3 moveDirX = new Vector3(moveDir.x, 0f, 0f).normalized;
            canMove = moveDirX != Vector3.zero && !Physics.CapsuleCast(transform.position,
                transform.position + Vector3.up * playerHeight,
                playerRadius, moveDirX, moveDistance);
            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                Vector3 moveDirZ = new Vector3(0f, 0f, moveDir.z).normalized;
                canMove = moveDirZ != Vector3.zero && !Physics.CapsuleCast(transform.position,
                    transform.position + Vector3.up * playerHeight,
                    playerRadius, moveDirZ, moveDistance);
                if (canMove)
                {
                    moveDir = moveDirZ;
                }
            }
        }

        if (canMove)
        {
            transform.position += moveDir * moveDistance;
        }

        _isWalking = moveDir != Vector3.zero;

        if (_isWalking)
        {
            if (footStepTimer < footStepTimerMax)
                footStepTimer += Time.deltaTime;
            else
            {
                footStepTimer = 0f;
                playAudioEvent.RaiseRandom(footStepAudios);
            }
        }

        float rotateSpeed = 23f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);

        _dashDir = transform.forward;
    }

    void HandleDash()
    {
        if (!_isDashing) return;
        if (!KitchenGameManager.Instance.IsGamePlaying()) return;

        var offSetDistance = 1f;

        if (_dashDistance < dashDistanceMax + offSetDistance)
        {
            Vector3 dashDir = _dashDir;

            float dashDistance = dashSpeed * Time.deltaTime;

            _dashDistance = _dashDistance + dashDistance;

            bool canDash = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight,
                playerRadius, dashDir, dashDistance);

            if (!canDash)
            {
                var offSet = 0.15f;
                var dirX = Math.Abs(dashDir.x) - offSet < 0f ? 0f : dashDir.x;
                Vector3 dashDirX = new Vector3(dirX, 0f, 0f).normalized;
                canDash = dashDirX != Vector3.zero && !Physics.CapsuleCast(transform.position,
                    transform.position + Vector3.up * playerHeight,
                    playerRadius, dashDirX, dashDistance);
                if (canDash)
                {
                    dashDir = dashDirX;
                }
                else
                {
                    var dirZ = Math.Abs(dashDir.z) - offSet < 0f ? 0f : dashDir.z;
                    Vector3 moveDirZ = new Vector3(0f, 0f, dirZ).normalized;
                    canDash = moveDirZ != Vector3.zero && !Physics.CapsuleCast(transform.position,
                        transform.position + Vector3.up * playerHeight,
                        playerRadius, moveDirZ, dashDistance);
                    if (canDash)
                    {
                        dashDir = moveDirZ;
                    }
                }
            }

            if (canDash && _dashDistance < dashDistanceMax)
            {
                transform.position += dashDir * dashDistance;
            }

            return;
        }

        _isDashing = false;
    }

    public bool IsWalking()
    {
        return _isWalking;
    }

    public bool IsDashing()
    {
        return _isDashing;
    }

    private void SetSelectedCounter(BaseCounter selectedCounter)
    {
        this._selectedCounter = selectedCounter;
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            selectedCounter = selectedCounter
        });
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this._kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject()
    {
        return this._kitchenObject;
    }

    public void ClearKitchenObject()
    {
        _kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return _kitchenObject != null;
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return holdKitchenObjectPoint;
    }
}