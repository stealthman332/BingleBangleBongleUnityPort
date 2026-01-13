using UnityEngine;
using UnityEngine.InputSystem;

public class TextboxTestDriver : MonoBehaviour
{
    [SerializeField] private TextboxController textbox;

    private int state = 0;

    private void Start()
    {
        Next();
    }

    private void Update()
    {
        // Mouse click
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
            textbox.Advance();

        // Space / Enter
        if (Keyboard.current != null &&
            (Keyboard.current.spaceKey.wasPressedThisFrame || Keyboard.current.enterKey.wasPressedThisFrame))
        {
            textbox.Advance();
        }
    }

    private void Next()
    {
        state++;

        if (state == 1)
            textbox.DisplayLine("", "…Where am I?", Next);
        else if (state == 2)
            textbox.DisplayLine("???", "A goblet waits in the dark.", Next);
        else
            textbox.DisplayLine("", "End of test.", null);
    }
}
