using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("Move State")]
    public float MoveSpeed = 6f;
    
    [Header("Dash State")]
    public float dashSpeedDrop = 3f;
    public float dashSpeed = 15f;
    public float dashSpeedMinimum = 5f;
}
