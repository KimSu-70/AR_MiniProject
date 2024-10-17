using System.Collections;
using UnityEngine;

public class CatController : MonoBehaviour
{
    public enum State
    {
        Idle, Christell, Food, Jump, Fear,
        Clicks, Spin, Roll, Dead, Size
    }
    [SerializeField] public State curState = State.Idle;
    private BaseState[] states = new BaseState[(int)State.Size];

    [SerializeField] Animator animator;
    private Transform targetFood;

    [Header("Cat")]
    [SerializeField] float health;
    [SerializeField] int maxHealth = 100;
    [SerializeField] float hunger;
    [SerializeField] int maxHunger = 100;
    [SerializeField] float happy;
    [SerializeField] int maxHappy = 100;
    [SerializeField] int moveSpeed = 3;

    private void Awake()
    {
        states[(int)State.Idle] = new IdleState(this);
        states[(int)State.Christell] = new ChristellState(this);
        states[(int)State.Food] = new FoodState(this);
        states[(int)State.Jump] = new JumpState(this);
        states[(int)State.Fear] = new FearState(this);
        states[(int)State.Clicks] = new ClicksState(this);
        states[(int)State.Spin] = new SpinState(this);
        states[(int)State.Roll] = new RollState(this);
        states[(int)State.Dead] = new DeadState(this);
    }

    private void Start()
    {
        states[(int)curState].Enter();
        UIManager.Instance.InitializeSlider(maxHealth, maxHunger, maxHappy);
        StartCoroutine(UpdateStats());
    }

    private void OnDestroy()
    {
        states[(int)curState].Exit();
    }

    private void Update()
    {
        states[(int)curState].Update();
        UIManager.Instance.UpdateSliders(health, hunger, happy);
        if (happy >= 100)
        {
            happy = maxHappy;
        }
        else if (happy <= 0)
        {
            happy = 0;
        }
    }

    public void ChangeState(State nextState)
    {
        if (curState != nextState)
        {
            states[(int)curState].Exit();
            curState = nextState;
            states[(int)curState].Enter();
        }
    }

    private class CatState : BaseState
    {
        public CatController cat;

        public CatState(CatController cat)
        {
            this.cat = cat;
        }
    }

    private class IdleState : CatState
    {
        public IdleState(CatController cat) : base(cat)
        {
        }

        public override void Enter()
        {
            cat.animator.Play("IdleA");
        }

        public override void Update()
        {
            if (cat.targetFood != null)
            {
                cat.MoveToFood();
            }
        }
    }

    private class ChristellState : CatState
    {
        public ChristellState(CatController cat) : base(cat)
        {
        }

        public override void Enter()
        {
            cat.animator.Play("IdleS");
            AudioManager.Instance.PlayBgm(false);
            AudioManager.Instance.PlaySfx(AudioManager.Sfx.Christell);
        }

        public override void Exit()
        {
            cat.hunger -= 10;
            AudioManager.Instance.StopSfx();
            AudioManager.Instance.PlayBgm(true);
        }
    }

    private class FoodState : CatState
    {
        public FoodState(CatController cat) : base(cat)
        {
        }

        public override void Enter()
        {
            cat.animator.Play("Food");
        }

        public override void Update()
        {
            // �ִϸ��̼��� �������� Ȯ��
            // GetCurrentAnimatorStateInfo(0) ���� ��� ���� �ִϸ��̼� ���� ���� �������� �޼���
            // IsName ���� ��� ���� �ִϸ��̼��� �̸��� �´��� Ȯ��
            // normalizedTime�� �ִϸ��̼��� ����Ǵ� ������ ��Ÿ����, 0.0�� ����, 1.0�� ���� �ǹ�
            if (cat.animator.GetCurrentAnimatorStateInfo(0).IsName("Food") &&
                cat.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                AudioManager.Instance.PlaySfx(AudioManager.Sfx.Food);
                cat.ChangeState(State.Jump); // �ִϸ��̼��� ������ Jump ���·� ��ȯ
            }
        }

        public override void Exit()
        {
            cat.hunger += 25;
            cat.happy += 3;
            if (cat.hunger >= 100)
            {
                cat.hunger = cat.maxHunger;
            }
        }
    }

    private class JumpState : CatState
    {
        public JumpState(CatController cat) : base(cat)
        {
        }

        public override void Enter()
        {
            cat.animator.Play("Jump");
        }

        public override void Update()
        {
            if (cat.animator.GetCurrentAnimatorStateInfo(0).IsName("Jump") &&
                cat.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                cat.ChangeState(State.Idle); // �ִϸ��̼��� ������ Jump ���·� ��ȯ
            }
        }
    }

    private class FearState : CatState
    {
        private bool isRunning = false;
        public FearState(CatController cat) : base(cat)
        {
        }

        public override void Enter()
        {
            cat.animator.Play("Fear");
            AudioManager.Instance.PlaySfx(AudioManager.Sfx.Fear);
            if (!isRunning)
            {
                isRunning = true;
                cat.StartCoroutine(cat.FearTime());
            }
        }
        public override void Exit()
        {
            isRunning = false;
        }
    }

    public IEnumerator FearTime()
    {
        yield return new WaitForSeconds(0.6f);
        ChangeState(State.Idle);
    }

    private class ClicksState : CatState
    {
        private bool isRunning = false;
        public ClicksState(CatController cat) : base(cat)
        {
        }

        public override void Enter()
        {
            cat.animator.Play("Click");
            AudioManager.Instance.PlaySfx(AudioManager.Sfx.Clicks);
            if (!isRunning)
            {
                isRunning = true;
                cat.StartCoroutine(cat.ClickTime());
            }
        }

        public override void Exit()
        {
            isRunning = false;
        }
    }

    public IEnumerator ClickTime()
    {
        yield return new WaitForSeconds(0.4f);
        ChangeState(State.Idle);
    }

    private class SpinState : CatState
    {
        private bool isRunning = false;
        public SpinState(CatController cat) : base(cat)
        {
        }

        public override void Enter()
        {
            cat.animator.Play("Spin");
            cat.hunger -= 5;
            AudioManager.Instance.PlaySfx(AudioManager.Sfx.Spin);
            if (!isRunning)
            {
                isRunning = true;
                cat.StartCoroutine(cat.SpinTime());
            }
        }
        public override void Exit()
        {
            isRunning = false;
        }
    }

    public IEnumerator SpinTime()
    {
        yield return new WaitForSeconds(1.5f);
        ChangeState(State.Idle);
    }


    private class RollState : CatState
    {
        private bool isRunning = false;
        public RollState(CatController cat) : base(cat)
        {
        }

        public override void Enter()
        {
            cat.animator.Play("Roll");
            cat.hunger -= 5;
            AudioManager.Instance.PlaySfx(AudioManager.Sfx.Roll);
            if (!isRunning)
            {
                isRunning = true;
                cat.StartCoroutine(cat.RollTime());
            }
        }
        public override void Exit()
        {
            isRunning = false;
        }
    }

    public IEnumerator RollTime()
    {
        yield return new WaitForSeconds(1);
        ChangeState(State.Idle);
    }

    private class DeadState : CatState
    {
        public DeadState(CatController cat) : base(cat)
        {
        }

        public override void Enter()
        {
            cat.animator.Play("Dead");
        }

        public override void Exit()
        {

        }
    }

    public void Clicks()
    {
        if (health > 0 && hunger > 0)
        {
            ChangeState(State.Clicks);
        }
    }

    private IEnumerator UpdateStats()
    {
        while (true)
        {
            hunger -= 1;
            if (hunger < 0)
            {
                hunger = 0;
            }

            if (hunger == 0)
            {
                health -= 1;
                happy -= 5;

                if (health <= 0)
                {
                    hunger = 0;
                    ChangeState(State.Dead);
                    yield break;
                }
            }
            yield return new WaitForSeconds(40f);
        }
    }

    #region ���İ���
    public void SetTargetFood(Transform foodTransform)
    {
        targetFood = foodTransform;
    }

    private void MoveToFood()
    {
        if (curState != State.Dead)
        {
            // ���� �������� �̵�
            animator.Play("Run");
            Vector3 direction = (targetFood.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;

            // ���Ŀ� �����ߴ��� Ȯ��
            if (Vector3.Distance(transform.position, targetFood.position) < 0.5f)
            {
                targetFood = null; // ���Ŀ� �����ϸ� ��ǥ�� ����
                AudioManager.Instance.PlaySfx(AudioManager.Sfx.Food2);
                ChangeState(State.Food);
            }
        }
    }
    #endregion
    public void UseAegyo()
    {
        if (curState != State.Dead && curState != State.Christell)
        {
            if (hunger > 70 && health > 70 && happy > 70)
            {
                ChangeState(State.Spin);
            }
            else if (hunger > 30 && health > 60 && happy > 50)
            {
                ChangeState(State.Roll);
            }
            else
            {
                ChangeState(State.Fear);
            }
        }
    }

    public void Sing()
    {
        if (hunger > 40 && health > 70 && happy > 70)
        {
            // ���� ���°� Christell�� �ƴ� ���� ���� ��ȯ
            if (curState != State.Christell)
            {
                ChangeState(State.Christell);
                UIManager.Instance.SetMirrorBall();
            }
            else
            {
                // ���� Christell ���¶�� Exit �޼��带 ȣ���Ͽ� ����
                UIManager.Instance.SetMirrorBall();
                states[(int)curState].Exit();
                ChangeState(State.Idle); // Idle ���·� ��ȯ
            }
        }
        else
        {
            StartCoroutine(BounceTime());
            animator.Play("Bounce");
            AudioManager.Instance.PlaySfx(AudioManager.Sfx.Bounce);
        }
    }

    IEnumerator BounceTime()
    {
        yield return new WaitForSeconds(0.6f);
        animator.Play("IdleA");
    }
}