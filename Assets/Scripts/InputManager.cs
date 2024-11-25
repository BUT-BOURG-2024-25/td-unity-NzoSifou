using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;


public class InputManager : Singleton<InputManager>
{
    [SerializeField]
    private InputActionReference movementAction;
    [SerializeField]
    private InputActionReference jumpAction;
    [SerializeField]
    private InputActionReference clickAction;
    
    public Vector3 MovementInput { get; private set; }

    public Action<Vector2> FingerDownJoystickAction;

    public Action FingerDownJumpAction;
    

    public void RegisterOnJumpInput(Action<InputAction.CallbackContext> onJumpAction, bool register)
    {
        if(register)
            jumpAction.action.performed += onJumpAction;
        else 
            jumpAction.action.performed -= onJumpAction;
    }
    
    public void RegisterOnClick(Action<InputAction.CallbackContext> onClickAction, bool register)
    {
        if(register)
            clickAction.action.performed += onClickAction;
        else 
            clickAction.action.performed -= onClickAction;
    }

    private void Update()
    {
        var moveInput = movementAction.action.ReadValue<Vector2>();
        MovementInput = new Vector3(moveInput.x, 0, moveInput.y);
    }

    private void OnEnable()
    {
        TouchSimulation.Enable();
        EnhancedTouchSupport.Enable();

        Touch.onFingerDown += OnFingerDown;
    }
    
    private void OnDisable()
    {
        Touch.onFingerDown += OnFingerDown;
        
        TouchSimulation.Disable();
        EnhancedTouchSupport.Disable();
    }

    private void OnFingerDown(Finger finger)
    {
        var screenPosTouch = finger.screenPosition;
        var joystickRect = UIManager.Instance.Joystick.transform as RectTransform;
        var jumpButtonRect = UIManager.Instance.JumpButton.transform as RectTransform;

        if (jumpButtonRect != null)
        {
            var isInX = jumpButtonRect.offsetMin.x <= screenPosTouch.x && screenPosTouch.x <= jumpButtonRect.offsetMax.x;
            var isInY = jumpButtonRect.offsetMin.y <= screenPosTouch.y && screenPosTouch.y <= jumpButtonRect.offsetMax.y;
            if(isInX && isInY)
                FingerDownJumpAction.Invoke();
        }
        
        if (joystickRect != null)
        {
            var isInX = joystickRect.offsetMin.x <= screenPosTouch.x && screenPosTouch.x <= joystickRect.offsetMax.x;
            var isInY = joystickRect.offsetMin.y <= screenPosTouch.y && screenPosTouch.y <= joystickRect.offsetMax.y;
            if(!isInX || !isInY)
                FingerDownJoystickAction.Invoke(screenPosTouch);
        }
        
    }
}