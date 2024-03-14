using UnityEngine;

namespace CoreSystem
{
public class CollisionSenses : CoreComponent
{
    private Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }

    private Movement movement;

    #region Check Transforms
    public Transform GroundCheck
    {
        get => GenericNotImplementedError<Transform>.TryGet(groundCheck, core.transform.parent.name);
        private set => groundCheck = value;
    }
    #endregion

    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius;

    public bool Ground
    {
        get => true;
        // get => Physics2D.OverlapCircle(GroundCheck.position, groundCheckRadius, whatIsGround);
    }

}
}