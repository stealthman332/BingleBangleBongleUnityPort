using UnityEngine;
using UnityEngine.UI;

public class TavernIntroController : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextboxController textbox;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image enemyImage;

    [Header("Art")]
    [SerializeField] private Sprite tavernBackground;
    [SerializeField] private Sprite patronEnemyPlaceholder; // optional placeholder, can be null

    private int beat = 0;

    private void Start()
    {
        Debug.Log("TavernIntroController Start() fired");

        if (backgroundImage != null && tavernBackground != null)
            backgroundImage.sprite = tavernBackground;

        if (enemyImage != null)
            enemyImage.gameObject.SetActive(false);

        Next();
    }

    private void Next()
    {
        beat++;

        switch (beat)
        {
            // Wake-up + head bang
            case 1:
                textbox.DisplayLine("", "…", Next);
                break;
            case 2:
                textbox.DisplayLine("", "Your vision swims. The smell of stale ale clings to your clothes.", Next);
                break;
            case 3:
                textbox.DisplayLine("", "You try to sit up too fast.", Next);
                break;
            case 4:
                textbox.DisplayLine("", "THUNK.", Next);
                break;
            case 5:
                textbox.DisplayLine("", "Your forehead meets the bar with the enthusiasm of a falling brick.", Next);
                break;

            // Bartender appears (faceless protagonist, bartender is the “voice”)
            case 6:
                textbox.DisplayLine("Bartender", "Easy there.", Next);
                break;
            case 7:
                textbox.DisplayLine("Bartender", "You’re the third body I’ve had to scrape off this counter this month.", Next);
                break;
            case 8:
                textbox.DisplayLine("", "You swallow the metallic taste in your mouth and force your eyes open.", Next);
                break;

            // “Fake branch” question (different response, same path)
            case 9:
                textbox.DisplayLine("", "You manage a question that sounds more like a cough.", () =>
                {
                    textbox.ShowChoices(
                        new[] { "Where am I?", "Who are you?", "How long was I out?" },
                        choice =>
                        {
                            if (choice == 0)
                                textbox.DisplayLine("Bartender", "The Hollow Cup. Best bar in the district—if you ignore the stains.", Next);
                            else if (choice == 1)
                                textbox.DisplayLine("Bartender", "Just the bartender. People call me what they need to.", Next);
                            else
                                textbox.DisplayLine("Bartender", "Long enough to miss the part where you paid.", Next);
                        }
                    );
                });
                break;

            // Worldbuilding nudge
            case 10:
                textbox.DisplayLine("", "The room is warm, but something about it feels… staged.", Next);
                break;
            case 11:
                textbox.DisplayLine("Bartender", "You look like someone who woke up in the wrong version of their life.", Next);
                break;
            case 12:
                textbox.DisplayLine("", "Before you can answer, a chair scrapes harshly across the floor.", Next);
                break;

            // Patron confrontation
            case 13:
                textbox.DisplayLine("Patron", "Oy. That seat taken, barkeep?", Next);
                break;
            case 14:
                textbox.DisplayLine("Bartender", "Not tonight.", Next);
                break;
            case 15:
                textbox.DisplayLine("Patron", "Funny. Because I’m sitting anyway.", Next);
                break;
            case 16:
                textbox.DisplayLine("", "The bartender’s jaw tightens. You can feel the room holding its breath.", Next);
                break;
            case 17:
                textbox.DisplayLine("Patron", "And who’s the corpse on the counter? Your new pet?", Next);
                break;
            case 18:
                textbox.DisplayLine("Bartender", "Don’t.", Next);
                break;
            case 19:
                textbox.DisplayLine("Patron", "Make me.", Next);
                break;

            // Trigger “fight starts”
            case 20:
                textbox.DisplayLine("", "The patron’s hand darts toward you.", () =>
                {
                    StartFirstFight();
                });
                break;

            default:
                break;
        }
    }

    private void StartFirstFight()
    {
        // For now: just switch the presentation to “combat mode”
        if (enemyImage != null)
        {
            enemyImage.gameObject.SetActive(true);
            if (patronEnemyPlaceholder != null)
                enemyImage.sprite = patronEnemyPlaceholder;
        }

        textbox.DisplayLine("", "FIGHT START (placeholder).", () =>
        {
            // Next step: call CombatController here
            textbox.DisplayLine("Bartender", "Try not to die on my floor.", null);
        });
    }
}
