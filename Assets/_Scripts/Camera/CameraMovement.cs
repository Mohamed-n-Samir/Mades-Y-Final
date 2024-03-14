using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// public class NewBehaviourScript : MonoBehaviour
// {
//     [SerializeField] private Transform target;
//     [SerializeField] private float followSpeed = 2f;
//     [SerializeField] private Vector2 offset = new Vector2(0f, 1f);

//     // Update is called once per frame
//     void Update()
//     {
//         if (transform.position != target.position)
//         {
//             Vector3 targetPosition = new Vector3(target.position.x + offset.x, target.position.y + offset.y, -10f);
//             transform.position = Vector3.Slerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
//         }
//     }
// }

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] private float smoothTime = 0.4f;
    [SerializeField] private Vector3 velocity = Vector3.zero;
    [SerializeField] private Vector3 offset = new Vector3(0f, 0f, -10f);
    [SerializeField] public Vector2 MaxCameraView;
    [SerializeField] public Vector2 MinCameraView;

    // Update is called once per frame
    void LateUpdate()
    {
        if (transform.position != target.position)
        {
            Vector3 targetPosition = target.position + offset;
            targetPosition.x = Mathf.Clamp(targetPosition.x,MinCameraView.x,MaxCameraView.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y,MinCameraView.y,MaxCameraView.y);
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
    }
}
