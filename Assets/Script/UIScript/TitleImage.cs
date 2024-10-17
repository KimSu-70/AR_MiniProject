using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleImage : MonoBehaviour
{
    public Image image; // Image 컴포넌트를 연결 변수
    public float fadeDuration = 2.0f; // 페이드 효과 시간
    public float delayDuration = 1.0f; // 페이드 사이의 딜레이 시간

    private void Start()
    {
        StartCoroutine(FadeInOutLoop());
    }

    // 반복적인 FadeIn과 FadeOut을 구현하는 코루틴
    IEnumerator FadeInOutLoop()
    {
        while (true) // 무한 반복
        {
            // 먼저 서서히 나타나게
            yield return StartCoroutine(FadeIn());

            // 잠시 대기
            yield return new WaitForSeconds(delayDuration);

            // 그 다음에 서서히 사라지게
            yield return StartCoroutine(FadeOut());

            // 잠시 대기
            yield return new WaitForSeconds(delayDuration);
        }
    }

    IEnumerator FadeIn() // 알파 값이 0에서 1로 변하는 코루틴 (서서히 보이게 함)
    {
        Color startColor = image.color;
        startColor.a = 0; // 시작 알파 값은 0 (투명)
        image.color = startColor;

        Color targetColor = image.color;
        targetColor.a = 1; // 최종 알파 값은 1 (불투명)

        float time = 0;

        // 알파 값을 서서히 변화시키는 while 루프
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(startColor.a, targetColor.a, time / fadeDuration);
            image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
            yield return null; // 매 프레임마다 실행
        }

        // 최종 알파 값 설정 (불투명)
        image.color = targetColor;
    }

    IEnumerator FadeOut()  // 알파 값이 1에서 0으로 변하는 코루틴 (서서히 사라지게 함)
    {
        Color startColor = image.color;
        startColor.a = 1; // 시작 알파 값은 1 (불투명)

        Color targetColor = image.color;
        targetColor.a = 0; // 최종 알파 값은 0 (투명)

        float time = 0;

        // 알파 값을 서서히 변화시키는 while 루프
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(startColor.a, targetColor.a, time / fadeDuration);
            image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
            yield return null; // 매 프레임마다 실행
        }

        // 최종 알파 값 설정 (투명)
        image.color = targetColor;
    }
}
