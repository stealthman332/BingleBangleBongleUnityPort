using UnityEngine;
using UnityEngine.InputSystem;

public class AdvanceInput : MonoBehaviour
{
    [SerializeField] private TextboxController textbox;

    private void Update()
    {
        if (textbox == null) return;

        bool pressed =
            (Keyboard.current != null &&
             (Keyboard.current.spaceKey.wasPressedThisFrame ||
              Keyboard.current.enterKey.wasPressedThisFrame ||
              Keyboard.current.numpadEnterKey.wasPressedThisFrame)) ||
            (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame);

        if (pressed)
            textbox.Advance();
    }
}
