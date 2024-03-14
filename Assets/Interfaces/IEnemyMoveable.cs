using UnityEngine;

public interface IEnemyMoveable
{ 
    Rigidbody2D EnemyRB { get; set; }

    bool IsFacingLeft { get; set; }

    void EnemyMove(Vector2 velocity);

    void CheackLeftOrRightFacing(Vector2 velocity);
}