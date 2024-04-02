using Dacodelaac.Core;
using UnityEngine;

public class PlayerAnimator : BaseMono
{
    private const string IS_WALKING = "IsWalking";

    [SerializeField] private Player player;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public override void Tick()
    {
        base.Tick();
        animator.SetBool(IS_WALKING, player.IsWalking());
    }
}
