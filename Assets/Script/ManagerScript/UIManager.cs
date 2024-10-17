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

    public void SetMirrorBall()
    {
        if (mirrorBall.activeSelf)
        {
            mirrorBall.SetActive(false); // ��Ȱ��ȭ
        }
        else
        {
            mirrorBall.SetActive(true); // Ȱ��ȭ
        }
    }
}
