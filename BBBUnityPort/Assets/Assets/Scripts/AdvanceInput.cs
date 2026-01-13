using UnityEngine;
using UnityEngine.InputSystem;

public class AdvanceInput : MonoBehaviour
{
    [SerializeField] private TextboxController textbox;

    private void Update()
    {
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
            textbox.Advance();

        if (Keyboard.current != null &&
            (Keyboard.current.spaceKey.wasPressedThisFrame || Keyboard.current.enterKey.wasPressedThisFrame))
        {
            textbox.Advance();
        }
    }
}
