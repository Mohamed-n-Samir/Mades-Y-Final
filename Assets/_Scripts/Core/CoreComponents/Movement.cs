using UnityEngine;

public class Movement : CoreComponent
{
    public Rigidbody2D RB { get; private set; }
    public Vector2 CurrentVelocity { get; private set; }
    public Vector2 NewVelocity { get; private set; }

    public Vector2 FacingDirection { get; private set; }

    public bool CanSetVelocity { get; set; }

    protected override void Awake()
    {
        base.Awake();

        RB = GetComponentInParent<Rigidbody2D>();

        FacingDirection = new Vector2(0,-1);
        CanSetVelocity = true;
    }

    public override void LogicUpdate()
    {
        CurrentVelocity = RB.velocity;
    }

    #region Set Functions

    public void SetVeloityZero(){
        NewVelocity = Vector2.zero;
        SetFinalVelocity();
    }

    public void SetVelocity(Vector2 velocity){
        NewVelocity = velocity;
        SetFinalVelocity();
    }

    public void SetFinalVelocity(){
        if(CanSetVelocity){
            RB.velocity = NewVelocity;
            CurrentVelocity = NewVelocity;
        }
    }

    

    #endregion


}