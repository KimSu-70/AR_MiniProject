using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Slider healths;
    public Slider hungers;
    public Slider happys;


    [SerializeField] CatController cat;
    [SerializeField] GameObject mirrorBall;

    private void Start()
    {
        Application.targetFrameRate = 60;
        //cat.OnDied += GameOver;
        AudioManager.Instance.PlayBgm(true);
        mirrorBall.SetActive(false);
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

    public void SetMirrorBall()
    {
        if (mirrorBall.activeSelf)
        {
            mirrorBall.SetActive(false); // 비활성화
        }
        else
        {
            mirrorBall.SetActive(true); // 활성화
        }
    }
}
