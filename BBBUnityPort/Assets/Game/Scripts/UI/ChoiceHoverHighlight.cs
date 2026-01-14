using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChoiceHoverHighlight : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TMP_Text tmp;

    [Header("Normal")]
    [SerializeField] private Color normalFace = Color.white;
    [SerializeField] private Color normalOutline = new Color(0f, 0f, 0f, 1f);
    [Range(0f, 1f)][SerializeField] private float normalOutlineWidth = 0.12f;

    [Header("Hover")]
    [SerializeField] private Color hoverFace = new Color(1f, 0.95f, 0.75f, 1f);
    [SerializeField] private Color hoverOutline = new Color(1f, 0.8f, 0.2f, 1f);
    [Range(0f, 1f)][SerializeField] private float hoverOutlineWidth = 0.32f;

    private Material runtimeMat;

    private void Awake()
    {
        if (tmp == null) tmp = GetComponentInChildren<TMP_Text>(true);

        // Per-button material so hover doesn't affect other buttons
        runtimeMat = new Material(tmp.fontMaterial);
        tmp.fontMaterial = runtimeMat;

        Apply(normalFace, normalOutline, normalOutlineWidth);
    }

    public void OnPointerEnter(PointerEventData eventData)
        => Apply(hoverFace, hoverOutline, hoverOutlineWidth);

    public void OnPointerExit(PointerEventData eventData)
        => Apply(normalFace, normalOutline, normalOutlineWidth);

    private void Apply(Color face, Color outline, float outlineWidth)
    {
        if (tmp == null || runtimeMat == null) return;
        tmp.color = face;
        runtimeMat.SetColor("_OutlineColor", outline);
        runtimeMat.SetFloat("_OutlineWidth", outlineWidth);
    }
}
