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
        // ���콺 Ŭ�����ε� �����ϰ� �ϰ� ���� ��� (PC��)
        else if (Input.GetMouseButtonDown(0))
        {
            // ���콺 ���� ��ư Ŭ�� �� ���� ��ȯ
            SceneManager.LoadScene("SampleScene");
        }
    }
}
