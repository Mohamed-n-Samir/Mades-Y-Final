using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwapManager : MonoBehaviour
{
    public static SceneSwapManager instance;

    private static bool _loadFromDoor = false;
    public static Vector2 _cameraMaxPosition;
    public static Vector2 _cameraMinPosition;

    private GameObject _player;
    private Collider2D _playerColl;
    private Collider2D _doorColl;
    private Vector3 _playerSpawnPosition;
    private CameraMovement cam;


    private RoomTriggerInteraction.DoorToSpawnAt _doorToSpawnTo;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        _player = GameObject.FindGameObjectWithTag("Player");
        _playerColl = _player.GetComponent<Collider2D>();
        cam = Camera.main.GetComponent<CameraMovement>();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

    }

    public static void SwapSceneFromDoorUse(SceneField myScene, RoomTriggerInteraction.DoorToSpawnAt doorToSpawnAt, Vector2 NewSceneMaxBound, Vector2 NewSceneMinBound)
    {
        _loadFromDoor = true;
        _cameraMaxPosition = NewSceneMaxBound;
        _cameraMinPosition = NewSceneMinBound;
        instance.StartCoroutine(instance.FadeOutThenChangeScene(myScene, doorToSpawnAt));
    }

    private IEnumerator FadeOutThenChangeScene(SceneField myScene, RoomTriggerInteraction.DoorToSpawnAt doorToSpawnAt = RoomTriggerInteraction.DoorToSpawnAt.None)
    {
        // start fading to black
        InputManager.DeactivatePlayerControls();
        ScreenFadeManager.instance.StartFadeOut();

        // keep fading out
        while (ScreenFadeManager.instance.IsFadingOut)
        {
            yield return null;
        }

        _doorToSpawnTo = doorToSpawnAt;
        SceneManager.LoadScene(myScene);

    }

    private IEnumerator ActivatePlayerControlsAfterFadeIn()
    {
        while (ScreenFadeManager.instance.IsFadingIn)
        {
            yield return null;
        }

        InputManager.ActivatePlayerControls();
    }

    //CALLED WHENEVER A NEW SCENE IS LOADED (INCLUDING THE START OF THE GAME)
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene != null)
        {
            ScreenFadeManager.instance.StartFadeIn();
            if (_loadFromDoor)
            {
                StartCoroutine(ActivatePlayerControlsAfterFadeIn());

                FindDoor(_doorToSpawnTo);
                _player.transform.position = _playerSpawnPosition;
                _loadFromDoor = false;
                cam.MinCameraView = _cameraMinPosition;
                cam.MaxCameraView = _cameraMaxPosition;
            }
        }
    }

    private void FindDoor(RoomTriggerInteraction.DoorToSpawnAt doorSpawnNumber)
    {
        RoomTriggerInteraction[] doors = FindObjectsOfType<RoomTriggerInteraction>();

        for (int i = 0; i < doors.Length; i++)
        {
            if (doors[i].CurrentRoomPosition == doorSpawnNumber)
            {
                _doorColl = doors[i].gameObject.GetComponent<Collider2D>();

                // Debug.Log(_doorColl);
                // Debug.Log(_playerColl);

                //calculate spawn Pos
                CalculateSpawnPosition();

                return;
            }
        }
    }

    private void CalculateSpawnPosition()
    {
        float colliderHeight = _playerColl.bounds.extents.y;
        _playerSpawnPosition = _doorColl.transform.position - new Vector3(0f, colliderHeight, 0f);

        if (_playerSpawnPosition.x < 0)
        {
            cam.transform.position = new Vector3(_playerSpawnPosition.x + CameraDimensions.GetCameraWidth() / 2, _playerSpawnPosition.y, cam.transform.position.z);
            _playerSpawnPosition.x += 1;

        }
        else
        {
            cam.transform.position = new Vector3(_playerSpawnPosition.x - CameraDimensions.GetCameraWidth() / 2, _playerSpawnPosition.y, cam.transform.position.z);
            _playerSpawnPosition.x -= 1;

        }
    }

}
