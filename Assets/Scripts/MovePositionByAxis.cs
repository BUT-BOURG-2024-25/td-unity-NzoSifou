using UnityEngine;
using UnityEngine.InputSystem;

public class MovePositionByAxis : MonoBehaviour
{
    [SerializeField] public float speed = 2.0f;
    [SerializeField] private Rigidbody physicsBody;
    [SerializeField] private float jumpPower = 20.0f;
    [SerializeField] private bool moveWithJoystick;
    private void Update()
    {
        if (moveWithJoystick)
        {
            var joystickDirection = new Vector3(
                UIManager.Instance.Joystick.Direction.x,
                0.0f,
                UIManager.Instance.Joystick.Direction.y
            );
            var newVelocity = joystickDirection * speed;
            newVelocity.y = physicsBody.velocity.y;
            physicsBody.velocity = newVelocity;
        }
        else
        {
            var newVelocity = InputManager.Instance.MovementInput * speed;
            newVelocity.y = physicsBody.velocity.y;
            physicsBody.velocity = newVelocity;
        }
    }

    private void Start()
    {
        InputManager.Instance.RegisterOnJumpInput(OnKeyPressed, true);
        InputManager.Instance.FingerDownJumpAction += OnFingerDown;
    }
    
    private void OnDestroy()
    {
        InputManager.Instance.RegisterOnJumpInput(OnKeyPressed, false);
        InputManager.Instance.FingerDownJumpAction -= OnFingerDown;
    }

    private void OnFingerDown()
    {
        Jump();
    }
    
    private void OnKeyPressed(InputAction.CallbackContext callbackContext)
    {
        Jump();
    }

    private void Jump()
    {
        physicsBody.AddForce(Vector3.up * jumpPower);
    }
}