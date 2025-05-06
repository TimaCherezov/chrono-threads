using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeController : MonoBehaviour
{
    [SerializeField] private Image fadeImage;  // ׸���� Image �� ���� �����
    [SerializeField] private float fadeInDuration = 0.5f;  // ������������ ���������

    // ���������� ����������
    public void InstantFadeOut()
    {
        fadeImage.color = new Color(0, 0, 0, 1);  // ����� = 1 (��������� ������)
    }

    // ������� ���������
    public void StartFadeIn()
    {
        StartCoroutine(FadeInCoroutine());
    }

    private IEnumerator FadeInCoroutine()
    {
        float timer = 0;
        Color startColor = new Color(0, 0, 0, 1);  // ������: ��������� ������
        Color endColor = new Color(0, 0, 0, 0);     // �����: ����������

        while (timer < fadeInDuration)
        {
            fadeImage.color = Color.Lerp(startColor, endColor, timer / fadeInDuration);
            timer += Time.deltaTime;
            yield return null;
        }

        fadeImage.color = endColor;  // ��������� ������ ������������
    }

    // �����: ���������� ���������� + ������� ���������
    public void InstantFadeOutThenIn()
    {
        InstantFadeOut();
        StartFadeIn();
    }
}