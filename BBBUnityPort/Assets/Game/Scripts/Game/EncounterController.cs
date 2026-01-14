using UnityEngine;
using UnityEngine.UI;

public class EncounterController : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image enemyImage;
    [SerializeField] private TextboxController textbox;

    [Header("Demo Art")]
    [SerializeField] private Sprite demoBackground;
    [SerializeField] private Sprite demoEnemy;

    private int beat = 0;

    private void Start()
    {
        // Load demo art (optional)
        if (demoBackground != null) backgroundImage.sprite = demoBackground;
        if (demoEnemy != null) enemyImage.sprite = demoEnemy;

        NextBeat();
    }

    public void NextBeat()
    {
        beat++;

        if (beat == 1)
            textbox.DisplayLine("", "The corridor narrows. The air tastes like rust.", NextBeat);
        else if (beat == 2)
            textbox.DisplayLine("???", "…You shouldn’t have come here.", NextBeat);
        else if (beat == 3)
        {
            textbox.DisplayLine("", "A shape stirs in the dark.", () =>
            {
                textbox.ShowChoices(new[] { "Stand your ground", "Run" }, choice =>
                {
                    textbox.DisplayLine("", choice == 0 ? "You brace yourself." : "You turn—too late.", NextBeat);
                });
            });
        }
        else
            textbox.DisplayLine("", "Encounter demo complete.", null);
    }
}
