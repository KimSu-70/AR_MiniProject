using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleButton : MonoBehaviour
{
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                SceneManager.LoadScene("SampleScene");
            }
        }
        // 마우스 클릭으로도 동작하게 하고 싶은 경우 (PC용)
        else if (Input.GetMouseButtonDown(0))
        {
            // 마우스 왼쪽 버튼 클릭 시 씬을 전환
            SceneManager.LoadScene("SampleScene");
        }
    }
}
