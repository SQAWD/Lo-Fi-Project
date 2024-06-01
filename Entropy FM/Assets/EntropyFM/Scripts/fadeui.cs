using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OverlayFader : MonoBehaviour
{
    public Image overlay;
    public float fadeDuration = 1.0f;

    public void StartFade()
    {
        StartCoroutine(FadeOutOverlay());
    }

    private IEnumerator FadeOutOverlay()
    {
        Color initialColor = overlay.color;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            overlay.color = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);
            yield return null;
        }

        overlay.gameObject.SetActive(false);
    }
}
