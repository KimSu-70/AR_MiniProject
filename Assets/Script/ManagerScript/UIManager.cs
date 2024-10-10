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
        healths.maxValue = health; // 최대 체력 설정
        hungers.maxValue = hunger; // 최대 배고픔 설정
        happys.maxValue = happy;   // 최대 행복 설정
    }

    public void UpdateSliders(float health, float hunger, float happy)
    {
        healths.value = health; // 현재 체력
        hungers.value = hunger; // 현재 배고픔
        happys.value = happy;   // 현재 행복
    }
    //public void PlayerLife()
    //{
    //    if (playerLife > 0)
    //    {
    //        playerLife--;
    //        playerLifeText.text = playerLife.ToString();
    //        cat.Respawn();
    //    }
    //    else if (playerLife == 0) // 생명이 0이 되었을 때 게임 오버
    //    {
    //        AudioManager.Instance.PlaySfx(AudioManager.Sfx.GameOver);
    //        GameOver(); // 게임 오버 UI 호출
    //    }
    //}
}
