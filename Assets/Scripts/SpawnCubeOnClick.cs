using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnCubeOnClick : MonoBehaviour
{
    [SerializeField]
    private GameObject objectToSpawn;
    [SerializeField]
    private LayerMask groundLayer;
    private void Start()
    {
        InputManager.Instance.RegisterOnClick(OnClick, true);
        InputManager.Instance.FingerDownJoystickAction += OnFingerDown;
    }
    
    private void OnDestroy()
    {
        InputManager.Instance.RegisterOnClick(OnClick, false);
        InputManager.Instance.FingerDownJoystickAction -= OnFingerDown;
    }

    private void OnFingerDown(Vector2 screenPosition)
    {
        SpawnCube(screenPosition);
    }

    private void OnClick(InputAction.CallbackContext callbackContext)
    {
        SpawnCube(Input.mousePosition);
    }

    private void SpawnCube(Vector2 screenPosition)
    {
        if (!Camera.main)
            return;
        
        var cameraRay = Camera.main.ScreenPointToRay(screenPosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(cameraRay, out hitInfo, 10000, groundLayer) && objectToSpawn != null)
        {
            Instantiate(objectToSpawn, hitInfo.point + Vector3.up * 0.5f, Quaternion.identity);
        }
    }
}