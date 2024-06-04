using Dacodelaac.Core;
using Dacodelaac.Variables;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : BaseMono, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] InputHandlerDataVariable data;
    [SerializeField] float handleRange = 1;
    [SerializeField] float deadZone = 0;
    [SerializeField] AxisOptions axisOptions = AxisOptions.Both;
    [SerializeField] bool snapX = false;
    [SerializeField] bool snapY = false;
    [SerializeField] protected RectTransform background = null;
    [SerializeField] RectTransform handle = null;

    public float Horizontal =>
        (snapX) ? SnapFloat(data.Value.Direction.x, AxisOptions.Horizontal) : data.Value.Direction.x;

    public float Vertical => (snapY) ? SnapFloat(data.Value.Direction.y, AxisOptions.Vertical) : data.Value.Direction.y;
    public Vector2 Direction => new(Horizontal, Vertical);
    public bool Dragging { get; private set; }

    public float HandleRange
    {
        get => handleRange;
        set => handleRange = Mathf.Abs(value);
    }

    public float DeadZone
    {
        get => deadZone;
        set => deadZone = Mathf.Abs(value);
    }

    public AxisOptions AxisOptions
    {
        get => AxisOptions;
        set => axisOptions = value;
    }

    public bool SnapX
    {
        get => snapX;
        set => snapX = value;
    }

    public bool SnapY
    {
        get => snapY;
        set => snapY = value;    
    }

    Canvas canvas;
    RectTransform baseRect = null;
    Camera cam;

    public override void Initialize()
    {
        base.Initialize();
        data.Value = new InputHandlerData();
        HandleRange = handleRange;
        DeadZone = deadZone;
        baseRect = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        if (canvas == null)
        {
            Debug.LogError("The Joystick is not placed inside a canvas");
        }

        var center = new Vector2(0.5f, 0.5f);
        background.pivot = center;
        handle.anchorMin = center;
        handle.anchorMax = center;
        handle.pivot = center;
        handle.anchoredPosition = Vector2.zero;
        Dragging = false;

        cam = null;
        if (canvas.renderMode == RenderMode.ScreenSpaceCamera)
        {
            cam = canvas.worldCamera;
        }
    }

    public override void DoDisable()
    {
        base.DoDisable();
        ResetData();
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        var position = RectTransformUtility.WorldToScreenPoint(cam, background.position);
        var radius = background.sizeDelta / 2;
        data.Value.Direction = (eventData.position - position) / (radius * canvas.scaleFactor);
        FormatInput();
        HandleInput(data.Value.Direction.magnitude, data.Value.Direction.normalized, radius, cam);
        handle.anchoredPosition = data.Value.Direction * radius * handleRange;
        Dragging = true;
    }

    protected virtual void HandleInput(float magnitude, Vector2 normalised, Vector2 radius, Camera cam)
    {
        if (magnitude > deadZone)
        {
            if (magnitude > 1)
            {
                data.Value.Direction = normalised;
            }
        }
        else
        {
            data.Value.Direction = Vector2.zero;
        }
    }

    void FormatInput()
    {
        if (axisOptions == AxisOptions.Horizontal)
        {
            data.Value.Direction = new Vector2(data.Value.Direction.x, 0f);
        }
        else if (axisOptions == AxisOptions.Vertical)
        {
            data.Value.Direction = new Vector2(0f, data.Value.Direction.y);
        }
    }

    float SnapFloat(float value, AxisOptions snapAxis)
    {
        if (value == 0)
            return value;

        if (axisOptions == AxisOptions.Both)
        {
            var angle = Vector2.Angle(data.Value.Direction, Vector2.up);
            if (snapAxis == AxisOptions.Horizontal)
            {
                if (angle < 22.5f || angle > 157.5f)
                {
                    return 0;
                }
                else
                {
                    return (value > 0) ? 1 : -1;
                }
            }
            else if (snapAxis == AxisOptions.Vertical)
            {
                if (angle > 67.5f && angle < 112.5f)
                {
                    return 0;
                }
                else
                {
                    return (value > 0) ? 1 : -1;
                }
            }

            return value;
        }
        else
        {
            if (value > 0)
            {
                return 1;
            }

            if (value < 0)
            {
                return -1;
            }
        }

        return 0;
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        ResetData();
    }

    void ResetData()
    {
        if (data.Value != null)
        {
            data.Value.Direction = Vector2.zero;
        }

        handle.anchoredPosition = Vector2.zero;
        Dragging = false;
    }

    protected Vector2 ScreenPointToAnchoredPosition(Vector2 screenPosition)
    {
        var localPoint = Vector2.zero;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(baseRect, screenPosition, cam, out localPoint))
        {
            var size = new Vector2(baseRect.rect.width, baseRect.rect.height);
            var pivotOffset = baseRect.pivot * size;
            return localPoint - (background.anchorMax * size) + pivotOffset;
        }

        return Vector2.zero;
    }
}

public enum AxisOptions
{
    Both,
    Horizontal,
    Vertical
}