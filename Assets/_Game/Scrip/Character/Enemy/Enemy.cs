using System;
using NavMeshAvoidance;
using UnityEngine;
using UnityEngine.AI;
public class Enemy : Character
{
    [SerializeField] public Avoidance Avoidance { get; set; }
    StateMachine stateMachine = new StateMachine();
    [SerializeField] protected Transform target;
    [SerializeField] protected NavMeshAgent agent;

    [SerializeField] protected int coinSpawn;
    [SerializeField] protected int Exp;
    protected bool isFollow;
    protected bool isAttack;
    [SerializeField] protected float timer;
    protected Vector3 dir;
    [SerializeField] protected float attackRange;
    [SerializeField] protected float damage;


    protected float radius;
    public Action<Enemy> OnDeathAction;
    public StateMachine StateMachine { get => stateMachine; set => stateMachine = value; }

    void Start()
    {
        Avoidance.AddAgent(agent);
    }
    public override void OnInit()
    {
        agent.enabled = true;
        agent.speed = speed;
        ChangeAnim(Constants.ANIM_IDLE);
        float randomFloat = UnityEngine.Random.Range(0.2f, 0.6f);
        agent.radius = randomFloat;
        isAttack = false;
        isFollow = false;
        characterHit.OnInit(hp);
        base.OnInit();
    }
    void Update()
    {
        if (IsDead) return;
        if (!isAttack)
        {
            if (!GameManager.IsState(GameState.Playing))
            {
                stateMachine.ChangeState(IdleState);
                return;
            }
            stateMachine?.Execute();
        }



        //AnimControl();
    }
    public bool IsDestionation => Vector3.Distance(transform.position, target.position + (transform.position.y - target.position.y) * Vector3.up) < 0.2f;
    //---------------------------Move---------------------------
    public void Moving()
    {
        if (target != null)
        {
            Debug.Log("..");
            agent.SetDestination(target.position);


        }
    }
    public void Stop()
    {
        agent.SetDestination(transform.position);
    }
    //---------------------------Attack---------------------------
    public virtual void Attack()
    {
        isAttack = true;
        dir = Player.Ins.transform.position - transform.position;
        dir.y = 0f;
        dir.Normalize();
        Quaternion targetRotation = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 50);
        Invoke(nameof(DealDmg), 0.2f);
        Invoke(nameof(ResetAttack), 1f);
    }
    protected void ResetAttack()
    {
        if (IsDead) return;
        ChangeAnim(Constants.ANIM_IDLE);
        isAttack = false;
    }
    protected virtual void DealDmg()
    {
        Player.Ins.OnHit(0.5f);

    }
    //---------------------------Target---------------------------
    public void SetTarget(Transform target)
    {
        this.target = target;
    }
    public bool IsPlayer()
    {

        return Vector3.Distance(transform.position, Player.Ins.transform.position) < attackRange;

    }

    //---------------------------Hit---------------------------
    public override void OnHit(float damage)
    {
        if (!IsDead && GameManager.IsState(GameState.Playing))
        {
            hp -= damage;
            characterHit.SetHp(hp);
            if (IsDead)
            {

                SaveLoadData.Ins.PlayerData.CurrentExp += Exp;
                for (int i = 0; i < coinSpawn; i++)
                {
                    float rd = UnityEngine.Random.Range(0, 1f);
                    Vector3 newPos = transform.position + new Vector3(rd, 2f, rd);
                    Coin newCoin = SmartPool.Ins.Spawn<Coin>(PoolType.Coin, newPos, transform.rotation);
                    newCoin.OnInit();
                }
                OnDead();
            }
        }
    }
    //---------------------------Dead---------------------------
    protected override void OnDead()
    {
        OnDeathAction?.Invoke(this);
        Stop();
        ChangeAnim(Constants.ANIM_DEAD);
        base.OnDead();
    }


    protected override void OnDespawn()
    {
        Avoidance.RemoveAgent(agent);
        SmartPool.Ins.Despawn(gameObject);
    }

    //---------------------------StateMachine---------------------------
    public void PatrolState(ref Action onEnter, ref Action onExecute, ref Action onExit)
    {
        onEnter = () =>
        {
            ChangeAnim(Constants.ANIM_WALKFW);
            Moving();
        };

        onExecute = () =>
        {

            if (IsDestionation)
            {
                stateMachine.ChangeState(IdleState);
            }
            if (IsPlayer())
            {
                timer -= Time.deltaTime;
                if (timer < 0)
                {
                    timer = 1f;
                    ChangeAnim(Constants.ANIM_ATTACKRUN);
                    Attack();
                }
                else
                {
                    ChangeAnim(Constants.ANIM_WALKFW);
                }
            }
            else
            {
                ChangeAnim(Constants.ANIM_WALKFW);
            }
        };

        onExit = () =>
        {

        };
    }
    public void FollowState(ref Action onEnter, ref Action onExecute, ref Action onExit)
    {
        onEnter = () =>
        {
            ChangeAnim(Constants.ANIM_WALKFW);
            Moving();
            isFollow = false;
        };

        onExecute = () =>
        {
            if (isFollow)
            {
                if (IsPlayer())
                {
                    agent.isStopped = true;
                    transform.position += Vector3.zero;
                    ChangeAnim(Constants.ANIM_IDLE);
                    timer -= Time.deltaTime;
                    if (timer < 0)
                    {
                        timer = 1f;
                        ChangeAnim(Constants.ANIM_ATTACKIDLE);
                        Attack();
                    }
                }
                else
                {
                    agent.isStopped = true;
                    ChangeAnim(Constants.ANIM_WALKFW);
                    //SetPlayer();
                    dir = Player.Ins.transform.position - transform.position;
                    dir.y = 0f;
                    dir.Normalize();
                    Quaternion targetRotation = Quaternion.LookRotation(dir);
                    transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 50);
                    transform.position += transform.forward * Time.deltaTime * 2f;
                }
            }
            else
            {
                Moving();
                if (IsDestionation)
                {
                    ChangeAnim(Constants.ANIM_IDLE);
                    target = transform;

                }
            }
        };

        onExit = () =>
        {

        };
    }
    public void IdleState(ref Action onEnter, ref Action onExecute, ref Action onExit)
    {
        onEnter = () =>
        {
            ChangeAnim(Constants.ANIM_IDLE);
            Stop();
        };

        onExecute = () =>
        {
            if (GameManager.IsState(GameState.Playing))
            {
                stateMachine.ChangeState(PatrolState);
            }
        };

        onExit = () =>
        {

        };
    }
    protected void OnTriggerEnter(Collider other)
    {
        if (!IsDead)
        {
            if (other.CompareTag(Constants.TAG_PLAYERZONE) && stateMachine.name == "FollowState")
            {
                // Avoidance.RemoveAgent(agent);
                // agent.enabled = false;
                target = transform;
                isFollow = true;
            }
            else if (other.CompareTag(Constants.TAG_CAMPCHARACTERHIT))
            {
                other.transform.parent.GetComponent<CampCharacter>().OnHit(damage * 2f);
                hp = 0;
                OnDead();
            }
        }
    }
    protected void OnDisable()
    {
        anim.transform.position = Vector3.zero;
    }


}
