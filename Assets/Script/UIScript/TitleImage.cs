using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleImage : MonoBehaviour
{
    public Image image; // Image ������Ʈ�� ���� ����
    public float fadeDuration = 2.0f; // ���̵� ȿ�� �ð�
    public float delayDuration = 1.0f; // ���̵� ������ ������ �ð�

    private void Start()
    {
        StartCoroutine(FadeInOutLoop());
    }

    // �ݺ����� FadeIn�� FadeOut�� �����ϴ� �ڷ�ƾ
    IEnumerator FadeInOutLoop()
    {
        while (true) // ���� �ݺ�
        {
            // ���� ������ ��Ÿ����
            yield return StartCoroutine(FadeIn());

            // ��� ���
            yield return new WaitForSeconds(delayDuration);

            // �� ������ ������ �������
            yield return StartCoroutine(FadeOut());

            // ��� ���
            yield return new WaitForSeconds(delayDuration);
        }
    }

    IEnumerator FadeIn() // ���� ���� 0���� 1�� ���ϴ� �ڷ�ƾ (������ ���̰� ��)
    {
        Color startColor = image.color;
        startColor.a = 0; // ���� ���� ���� 0 (����)
        image.color = startColor;

        Color targetColor = image.color;
        targetColor.a = 1; // ���� ���� ���� 1 (������)

        float time = 0;

        // ���� ���� ������ ��ȭ��Ű�� while ����
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(startColor.a, targetColor.a, time / fadeDuration);
            image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
            yield return null; // �� �����Ӹ��� ����
        }

        // ���� ���� �� ���� (������)
        image.color = targetColor;
    }

    IEnumerator FadeOut()  // ���� ���� 1���� 0���� ���ϴ� �ڷ�ƾ (������ ������� ��)
    {
        Color startColor = image.color;
        startColor.a = 1; // ���� ���� ���� 1 (������)

        Color targetColor = image.color;
        targetColor.a = 0; // ���� ���� ���� 0 (����)

        float time = 0;

        // ���� ���� ������ ��ȭ��Ű�� while ����
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(startColor.a, targetColor.a, time / fadeDuration);
            image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
            yield return null; // �� �����Ӹ��� ����
        }

        // ���� ���� �� ���� (����)
        image.color = targetColor;
    }
}
