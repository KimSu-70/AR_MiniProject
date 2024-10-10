using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Slider healths;
    public Slider hungers;
    public Slider happys;

    [SerializeField] CatController cat;

    private void Start()
    {
        Application.targetFrameRate = 60;
        //cat.OnDied += GameOver;
        AudioManager.Instance.PlayBgm(true);
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void InitializeSlider(int health, int hunger, int happy)
    {
        healths.maxValue = health; // �ִ� ü�� ����
        hungers.maxValue = hunger; // �ִ� ����� ����
        happys.maxValue = happy;   // �ִ� �ູ ����
    }

    public void UpdateSliders(float health, float hunger, float happy)
    {
        healths.value = health; // ���� ü��
        hungers.value = hunger; // ���� �����
        happys.value = happy;   // ���� �ູ
    }
    //public void PlayerLife()
    //{
    //    if (playerLife > 0)
    //    {
    //        playerLife--;
    //        playerLifeText.text = playerLife.ToString();
    //        cat.Respawn();
    //    }
    //    else if (playerLife == 0) // ������ 0�� �Ǿ��� �� ���� ����
    //    {
    //        AudioManager.Instance.PlaySfx(AudioManager.Sfx.GameOver);
    //        GameOver(); // ���� ���� UI ȣ��
    //    }
    //}
}
