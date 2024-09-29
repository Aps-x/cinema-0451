using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] float fadeDuration;
    [SerializeField] float delay;
    void Start()
    {
        StartCoroutine(StartFadeIn());
    }
    private IEnumerator StartFadeIn()
    {
        float startAlpha = canvasGroup.alpha;
        float time = 0;

        yield return new WaitForSeconds(delay);

        // Fade out over the duration
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, 0, time / fadeDuration);
            yield return null; 
        }

        canvasGroup.alpha = 0;
    }
}
