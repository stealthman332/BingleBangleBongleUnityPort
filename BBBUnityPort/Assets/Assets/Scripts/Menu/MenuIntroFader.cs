using System.Collections;
using UnityEngine;

public class MenuIntroFader : MonoBehaviour
{
    [Header("Fade Targets (CanvasGroups)")]
    [SerializeField] private CanvasGroup backgroundGroup;
    [SerializeField] private CanvasGroup menuGroup;
    [SerializeField] private AudioSource musicSource;

    [Header("Timing")]
    [SerializeField] private float backgroundFadeDuration = 2.0f;
    [SerializeField] private float menuFadeDuration = 1.4f;
    [SerializeField] private float menuFadeDelay = 0.25f;
    [SerializeField] private float delayBeforeStart = 0.05f;

    [Header("Audio")]
    [SerializeField] private float targetMusicVolume = 0.8f;

    private void Start()
    {
        InitGroup(backgroundGroup, false);
        InitGroup(menuGroup, false);

        if (menuGroup != null)
        {
            menuGroup.interactable = false;
            menuGroup.blocksRaycasts = false;
        }

        if (musicSource != null)
        {
            musicSource.volume = 0f;
            if (!musicSource.isPlaying)
                musicSource.Play();
        }

        StartCoroutine(FadeInRoutine());
    }

    private void InitGroup(CanvasGroup group, bool interactable)
    {
        if (group == null) return;

        group.alpha = 0f;
        group.interactable = interactable;
        group.blocksRaycasts = interactable;
    }

    private IEnumerator FadeInRoutine()
    {
        if (delayBeforeStart > 0f)
            yield return new WaitForSeconds(delayBeforeStart);

        float tBg = 0f;
        float tMenu = -menuFadeDelay;
        float bgDur = Mathf.Max(0.01f, backgroundFadeDuration);
        float menuDur = Mathf.Max(0.01f, menuFadeDuration);

        while (tBg < bgDur || tMenu < menuDur)
        {
            float dt = Time.unscaledDeltaTime;

            if (tBg < bgDur)
            {
                tBg += dt;
                float x = Mathf.Clamp01(tBg / bgDur);
                if (backgroundGroup != null)
                    backgroundGroup.alpha = EaseInOutCubic(x);
            }

            if (tMenu < menuDur)
            {
                tMenu += dt;
                float x = Mathf.Clamp01(tMenu / menuDur);
                if (menuGroup != null)
                    menuGroup.alpha = EaseInOutCubic(x);
            }

            if (musicSource != null)
            {
                float m = Mathf.Max(
                    backgroundGroup != null ? backgroundGroup.alpha : 1f,
                    menuGroup != null ? menuGroup.alpha : 1f
                );
                musicSource.volume = m * targetMusicVolume;
            }

            yield return null;
        }

        if (menuGroup != null)
        {
            menuGroup.alpha = 1f;
            menuGroup.interactable = true;
            menuGroup.blocksRaycasts = true;
        }

        if (backgroundGroup != null)
            backgroundGroup.alpha = 1f;

        if (musicSource != null)
            musicSource.volume = targetMusicVolume;
    }

    private float EaseInOutCubic(float x)
    {
        return x < 0.5f
            ? 4f * x * x * x
            : 1f - Mathf.Pow(-2f * x + 2f, 3f) / 2f;
    }
}
