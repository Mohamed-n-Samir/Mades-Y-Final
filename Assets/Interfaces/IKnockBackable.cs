using UnityEngine;

public interface IKnockBackable
{
    public float MaxKnockBackTime {get; set;}

    public bool IsKnockBackActive {get; set;}
    public float KnockBackStartTime {get; set;}

    void KnockBack(float strength, Vector2 direction);
    void CheckKnockBack();
}