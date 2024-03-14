using System;
using UnityEngine;

[Serializable]
public class RoomTriggerInteraction : TriggerInteractionBase
{
    public enum DoorToSpawnAt
    {
        None,
        One,
        Two,
        Three,
        Four
    }

    [Header("Spawn To")]
    [SerializeField] private DoorToSpawnAt DoorToSpawnto;
    [SerializeField] private SceneField _sceneToLoad;
    [SerializeField] private Vector2 NewSceneMaxBound;
    [SerializeField] private Vector2 NewSceneMinBound;

    [Space(10f)]
    [Header("This Door")]
    public DoorToSpawnAt CurrentRoomPosition;

    public override void Interact()
    {
        SceneSwapManager.SwapSceneFromDoorUse(_sceneToLoad,DoorToSpawnto,NewSceneMaxBound,NewSceneMinBound);

    }
}