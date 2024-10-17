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
            // 애니메이션이 끝났는지 확인
            // GetCurrentAnimatorStateInfo(0) 현재 재생 중인 애니메이션 상태 정보 가져오는 메서드
            // IsName 현재 재생 중인 애니메이션이 이름이 맞는지 확인
            // normalizedTime은 애니메이션이 재생되는 비율을 나타내며, 0.0은 시작, 1.0은 끝을 의미
            if (cat.animator.GetCurrentAnimatorStateInfo(0).IsName("Food") &&
                cat.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                AudioManager.Instance.PlaySfx(AudioManager.Sfx.Food);
                cat.ChangeState(State.Jump); // 애니메이션이 끝나면 Jump 상태로 전환
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
                cat.ChangeState(State.Idle); // 애니메이션이 끝나면 Jump 상태로 전환
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

    #region 음식관련
    public void SetTargetFood(Transform foodTransform)
    {
        targetFood = foodTransform;
    }

    private void MoveToFood()
    {
        if (curState != State.Dead)
        {
            // 음식 방향으로 이동
            animator.Play("Run");
            Vector3 direction = (targetFood.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;

            // 음식에 도착했는지 확인
            if (Vector3.Distance(transform.position, targetFood.position) < 0.5f)
            {
                targetFood = null; // 음식에 도착하면 목표를 없앰
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
            // 현재 상태가 Christell이 아닐 때만 상태 전환
            if (curState != State.Christell)
            {
                ChangeState(State.Christell);
                UIManager.Instance.SetMirrorBall();
            }
            else
            {
                // 현재 Christell 상태라면 Exit 메서드를 호출하여 종료
                UIManager.Instance.SetMirrorBall();
                states[(int)curState].Exit();
                ChangeState(State.Idle); // Idle 상태로 전환
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