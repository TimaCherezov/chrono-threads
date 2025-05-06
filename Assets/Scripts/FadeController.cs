using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeController : MonoBehaviour
{
    [SerializeField] private Image fadeImage;  // Чёрный Image на весь экран
    [SerializeField] private float fadeInDuration = 0.5f;  // Длительность появления

    // Мгновенное затемнение
    public void InstantFadeOut()
    {
        fadeImage.color = new Color(0, 0, 0, 1);  // Альфа = 1 (полностью чёрный)
    }

    // Плавное появление
    public void StartFadeIn()
    {
        StartCoroutine(FadeInCoroutine());
    }

    private IEnumerator FadeInCoroutine()
    {
        float timer = 0;
        Color startColor = new Color(0, 0, 0, 1);  // Начало: полностью чёрный
        Color endColor = new Color(0, 0, 0, 0);     // Конец: прозрачный

        while (timer < fadeInDuration)
        {
            fadeImage.color = Color.Lerp(startColor, endColor, timer / fadeInDuration);
            timer += Time.deltaTime;
            yield return null;
        }

        fadeImage.color = endColor;  // Фиксируем полную прозрачность
    }

    // Комбо: мгновенное затемнение + плавное появление
    public void InstantFadeOutThenIn()
    {
        InstantFadeOut();
        StartFadeIn();
    }
}