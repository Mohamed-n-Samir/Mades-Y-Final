using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(RoomTriggerInteraction))]
public class LabelHandle : Editor
{
    private static GUIStyle labelStyle;

    private void OnEnable(){
        labelStyle = new GUIStyle();
        labelStyle.normal.textColor = Color.red;
        labelStyle.alignment = TextAnchor.MiddleCenter; 
    }

    private void OnSceneGUI(){
        RoomTriggerInteraction room = (RoomTriggerInteraction)target;

        Handles.BeginGUI();
        Handles.Label(room.transform.position + new Vector3(0f,4f,0f), room.CurrentRoomPosition.ToString(),labelStyle);
        Handles.EndGUI();
    }
}