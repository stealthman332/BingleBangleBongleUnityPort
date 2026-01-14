using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class MenuButtonFX : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("Audio")]
    [SerializeField] private AudioSource uiAudioSource;
    [SerializeField] private AudioClip hoverClip;
    [SerializeField] private AudioClip clickClip;
    [Range(0f, 1f)][SerializeField] private float hoverVolume = 0.6f;
    [Range(0f, 1f)][SerializeField] private float clickVolume = 0.8f;

    [Header("Text Highlight (TMP)")]
    [SerializeField] private TMP_Text tmpText;

    [Header("Normal Text")]
    [SerializeField] private Color normalFace = Color.white;
    [SerializeField] private Color normalOutline = new Color(0f, 0f, 0f, 1f);
    [Range(0f, 1f)][SerializeField] private float normalOutlineWidth = 0.12f;

    [Header("Hover Text")]
    [SerializeField] private Color hoverFace = new Color(1f, 0.95f, 0.75f, 1f);
    [SerializeField] private Color hoverOutline = new Color(1f, 0.8f, 0.2f, 1f);
    [Range(0f, 1f)][SerializeField] private float hoverOutlineWidth = 0.32f;

    private Material runtimeMat;
    private bool hoverPlayedThisEnter = false;

    private void Awake()
    {
        if (tmpText == null)
            tmpText = GetComponentInChildren<TMP_Text>(true);

        if (tmpText != null)
        {
            // Make a per-button material copy so hover doesn't affect ALL buttons
            runtimeMat = new Material(tmpText.fontMaterial);
            tmpText.fontMaterial = runtimeMat;

            ApplyTextStyle(normalFace, normalOutline, normalOutlineWidth);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ApplyTextStyle(hoverFace, hoverOutline, hoverOutlineWidth);

        if (!hoverPlayedThisEnter && uiAudioSource != null && hoverClip != null)
        {
            uiAudioSource.PlayOneShot(hoverClip, hoverVolume);
            hoverPlayedThisEnter = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ApplyTextStyle(normalFace, normalOutline, normalOutlineWidth);
        hoverPlayedThisEnter = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (uiAudioSource != null && clickClip != null)
            uiAudioSource.PlayOneShot(clickClip, clickVolume);
    }

    private void ApplyTextStyle(Color face, Color outline, float outlineWidth)
    {
        if (tmpText == null || runtimeMat == null) return;

        tmpText.color = face;
        runtimeMat.SetColor("_OutlineColor", outline);
        runtimeMat.SetFloat("_OutlineWidth", outlineWidth);
    }
}