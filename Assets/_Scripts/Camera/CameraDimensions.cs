using UnityEngine;

public class CameraDimensions
{

    public static float GetCameraWidth()
    {
        Camera mainCamera = Camera.main; // Assuming you are using the main camera

        // Ensure the camera is not null
        if (mainCamera != null)
        {
            // Get the dimensions of the camera's view in world space
            float cameraWidth = mainCamera.orthographicSize * 2f * mainCamera.aspect;

            // Print or use the dimensions as needed
            Debug.Log("Camera Width: " + cameraWidth);
            return cameraWidth;
        }
        else
        {
            Debug.LogError("Main camera not found!");
            return 0;
        }
    }

    public static float GetCameraHeight()
    {
        Camera mainCamera = Camera.main; // Assuming you are using the main camera

        // Ensure the camera is not null
        if (mainCamera != null)
        {
            // Get the dimensions of the camera's view in world space
            float cameraHeight = mainCamera.orthographicSize * 2f;

            // Print or use the dimensions as needed
            Debug.Log("Camera Height: " + cameraHeight);
            return cameraHeight;

        }
        else
        {
            Debug.LogError("Main camera not found!");
            return 0;

        }
    }
}