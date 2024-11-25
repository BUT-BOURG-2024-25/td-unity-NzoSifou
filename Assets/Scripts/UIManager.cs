using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private Joystick joystick;
    [SerializeField] private Button jumpButton;

    public Joystick Joystick => joystick;

    public Button JumpButton => jumpButton;
}
