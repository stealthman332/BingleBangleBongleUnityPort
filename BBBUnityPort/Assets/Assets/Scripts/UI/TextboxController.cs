using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextboxController : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject textboxRoot;          // TextboxPanel
    [SerializeField] private TMP_Text speakerText;            // SpeakerText
    [SerializeField] private TMP_Text bodyText;               // BodyText
    [SerializeField] private GameObject continueIndicator;    // ContinueIndicator
    [SerializeField] private Transform choicesContainer;      // ChoicesContainer
    [SerializeField] private Button choiceButtonPrefab;       // ChoiceButton prefab

    [Header("Typewriter")]
    [SerializeField] private float charsPerSecond = 45f;

    private Coroutine typingRoutine;
    private bool isTyping;
    private string fullLine;

    private Action onContinue;
    private Action<int> onChoiceSelected;

    private readonly List<Button> activeChoiceButtons = new();

    private void Awake()
    {
        Hide();
    }

    public void Show()
    {
        textboxRoot.SetActive(true);
    }

    public void Hide()
    {
        textboxRoot.SetActive(false);
        ClearChoices();
        continueIndicator.SetActive(false);
        bodyText.text = "";
        speakerText.text = "";
        isTyping = false;
        fullLine = "";
    }

    /// <summary>
    /// Displays a line with optional speaker name. Clicking/Submit will either skip typing or continue.
    /// </summary>
    public void DisplayLine(string speaker, string line, Action onLineFinished = null)
    {
        Show();
        ClearChoices();
        continueIndicator.SetActive(false);

        speakerText.text = string.IsNullOrWhiteSpace(speaker) ? "" : speaker;

        onContinue = onLineFinished;

        // Restart typing
        if (typingRoutine != null) StopCoroutine(typingRoutine);
        typingRoutine = StartCoroutine(TypeLine(line));
    }

    private IEnumerator TypeLine(string line)
    {
        isTyping = true;
        fullLine = line;
        bodyText.text = "";

        float secondsPerChar = 1f / Mathf.Max(1f, charsPerSecond);

        foreach (char c in line)
        {
            bodyText.text += c;
            yield return new WaitForSeconds(secondsPerChar);
        }

        isTyping = false;
        continueIndicator.SetActive(true);
    }

    /// <summary>
    /// Call this when the player presses "advance" (click / space / enter).
    /// If typing, it will complete instantly; otherwise it will proceed.
    /// </summary>
    public void Advance()
    {
        if (!textboxRoot.activeSelf) return;

        if (isTyping)
        {
            // Skip typewriter
            if (typingRoutine != null) StopCoroutine(typingRoutine);
            bodyText.text = fullLine;
            isTyping = false;
            continueIndicator.SetActive(true);
            return;
        }

        // If choices are showing, don't auto-advance
        if (activeChoiceButtons.Count > 0) return;

        continueIndicator.SetActive(false);
        onContinue?.Invoke();
    }

    public void ShowChoices(IReadOnlyList<string> choices, Action<int> onSelected)
    {
        ClearChoices();
        onChoiceSelected = onSelected;

        continueIndicator.SetActive(false);

        for (int i = 0; i < choices.Count; i++)
        {
            int index = i;
            var btn = Instantiate(choiceButtonPrefab, choicesContainer);
            btn.gameObject.SetActive(true);

            var label = btn.GetComponentInChildren<TMP_Text>();
            if (label != null) label.text = choices[i];

            btn.onClick.AddListener(() => SelectChoice(index));
            activeChoiceButtons.Add(btn);
        }

        // optional: focus first button for keyboard/controller
        if (activeChoiceButtons.Count > 0)
            activeChoiceButtons[0].Select();
    }

    private void SelectChoice(int index)
    {
        ClearChoices();
        onChoiceSelected?.Invoke(index);
    }

    private void ClearChoices()
    {
        for (int i = 0; i < activeChoiceButtons.Count; i++)
        {
            if (activeChoiceButtons[i] != null)
                Destroy(activeChoiceButtons[i].gameObject);
        }
        activeChoiceButtons.Clear();
    }
}
