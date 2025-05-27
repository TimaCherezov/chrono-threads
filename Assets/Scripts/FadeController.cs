using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeController : MonoBehaviour
{
    [SerializeField] private Image fadeImage;
    // [SerializeField] private float fadeInDuration = 0.5f; 

    public void StartFadeIn()
    {
        StartCoroutine(FadeInCoroutine(new Color(0, 0, 0, 0), new Color(0, 0, 0, 1), 1));
    }

    public void StartFadeOut()
    {
        StartCoroutine(FadeInCoroutine(new Color(0, 0, 0, 1), new Color(0, 0, 0, 0), 2));
    }

    private IEnumerator FadeInCoroutine(Color startColor, Color endColor, float fadeInDuration)
    {
        var timer = 0f;
        while (timer < fadeInDuration)
        {
            fadeImage.color = Color.Lerp(startColor, endColor, timer / fadeInDuration);
            timer += Time.deltaTime;
            yield return null;
        }

        fadeImage.color = endColor;
    }
}