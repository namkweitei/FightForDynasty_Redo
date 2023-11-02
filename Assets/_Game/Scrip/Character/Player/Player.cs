using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;


[Serializable]
public enum EquimentType
{
    Sword,
    Crossbow,
    Bow,
    Spear,
}
public class Player : Character
{
    private static Player instance;
    public static Player Ins { get => instance; }
    [SerializeField] private bool needDontDestroy = false;
    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = (Player)this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        if (needDontDestroy) DontDestroyOnLoad(gameObject);
        SetUp();
    }
    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }
    protected virtual void SetUp()
    {

    }
    [SerializeField] Transform shootPointCrossBow;
    [SerializeField] private Transform shootPointBow;
    [SerializeField] private float attackTimer;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float damage;
    [SerializeField] private float timeHealling;
    [SerializeField] private SphereCollider attackRange;
    [SerializeField] private Transform ring;
    [SerializeField] private CinemachineVirtualCamera cameraFollow;
    [SerializeField] private List<GameObject> equiments;
    [SerializeField] private List<GameObject> armor;
    public List<GameObject> Armor { get => armor; set => armor = value; }
    private float horizontal;
    private float vertical;
    private Vector3 movement;
    private Vector3 direction;
    private bool isAttack;
    private void Start()
    {
        ChangeEquiment(SaveLoadData.Ins.PlayerData.EquiType);
        OnInit();
        SetArmor(SaveLoadData.Ins.PlayerData.Level);
    }
    public override void OnInit()
    {
        isAttack = false;
        maxHp = SaveLoadData.Ins.PlayerData.Hp;
        speed = SaveLoadData.Ins.PlayerData.Speed;
        base.OnInit();
    }
    private void Update()
    {
        Debug.Log(animName);
        if (IsDead) return;
        horizontal = UltimateJoystick.GetHorizontalAxis("PlayerJoystick");
        vertical = UltimateJoystick.GetVerticalAxis("PlayerJoystick");
        Healling();

        //AnimControl();
    }

    private void FixedUpdate()
    {
        if (IsDead) return;
        JoystickMovement();
        Rotate();
    }

    protected virtual void JoystickMovement()
    {
        movement = new Vector3(-horizontal, 0f, -vertical);
        movement.Normalize();
        rb.velocity = movement * speed * Time.fixedDeltaTime;
        if (!isAttack)
        {
            if (Mathf.Abs(horizontal) < 0.01f & Mathf.Abs(vertical) < 0.01f)
            {
                ChangeAnim(Constants.ANIM_IDLE);
            }
            else
            {
                ChangeAnim(Constants.ANIM_RUNFW);
            }

        }
        else
        {
            if (GetMove())
            {
                ChangeAnim(Constants.ANIM_ATTACKRUN);
            }
            else
            {
                ChangeAnim(Constants.ANIM_ATTACKIDLE);
            }
        }
    }

    protected virtual void Rotate()
    {
        if (attackZone.enemy.Count > 0 && GameManager.IsState(GameState.Playing))
        {
            if (attackZone.enemy[0].IsDead)
            {
                attackZone.enemy.Remove(attackZone.enemy[0]);
                attackTimer = attackSpeed;
            }
            else
            {
                this.attackTimer -= Time.fixedDeltaTime;
                if (this.attackTimer < 0)
                {
                    Attack(attackZone.enemy[0]);
                    this.attackTimer = attackSpeed;
                }
                direction = attackZone.enemy[0].transform.position - TF.position;
                direction.y = 0f;
                direction.Normalize();
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                TF.rotation = Quaternion.Lerp(TF.rotation, targetRotation, Time.deltaTime * 20);
            }
        }
        else if (horizontal != 0 || vertical != 0 && attackZone.enemy.Count > 0)
        {
            float targetAngle = Mathf.Atan2(-horizontal, -vertical) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0f, targetAngle, 0f);
            TF.rotation = Quaternion.Lerp(TF.rotation, targetRotation, Time.deltaTime * 10);
        }
        else if (horizontal != 0 || vertical != 0 && attackZone.enemy.Count < 0)
        {
            ResetAttack();
        }
    }
    private void Attack(Character target)
    {

        isAttack = true;
        Invoke(nameof(ResetAttack), attackSpeed);
        AttackType(target);
        //float time = anim.GetAnimatorStateInfo(1).length;


    }
    private IEnumerator DealDmg(Character target)
    {
        yield return new WaitForSeconds(0.7f);
        target.OnHit(damage);
    }
    private IEnumerator SpawnBullet(Character target)
    {
        yield return new WaitForSeconds(attackSpeed * 0.8f);
        Bullet bullet = SmartPool.Ins.Spawn<Bullet>(PoolType.Arrow, shootPointBow.position, Quaternion.LookRotation(direction));
        bullet.OnInit(target, damage);
        // bullet.targetObject = target;
        // bullet.Damage = damage;
        AudioManager.Ins.PlaySfx(Constants.SFX_SHOOT);

    }
    private void AttackType(Character target)
    {
        switch (SaveLoadData.Ins.PlayerData.EquiType)
        {
            case EquimentType.Sword:
                StartCoroutine(DealDmg(target));
                break;
            case EquimentType.Crossbow:
                Bullet bullet = SmartPool.Ins.Spawn<Bullet>(PoolType.Arrow, shootPointCrossBow.position, shootPointCrossBow.rotation);
                bullet.OnInit(target, damage);
                AudioManager.Ins.PlaySfx(Constants.SFX_SHOOT);
                break;
            case EquimentType.Bow:
                StartCoroutine(SpawnBullet(target));
                break;
            case EquimentType.Spear:
                StartCoroutine(DealDmg(target));
                break;
            default:
                break;
        }
    }
    public void ChangeEquiment(EquimentType equimentType)
    {
        for (int i = 0; i < SaveLoadData.Ins.PlayerData.EquimentDatas.Count; i++)
        {
            if (SaveLoadData.Ins.PlayerData.EquimentDatas[i].equimentType == equimentType)
            {
                SaveLoadData.Ins.PlayerData.EquiType = equimentType;
                anim.runtimeAnimatorController = SaveLoadData.Ins.PlayerData.EquimentDatas[i].anim;
                damage = SaveLoadData.Ins.PlayerData.EquimentDatas[i].damage;
                attackZone.transform.DOScale(SaveLoadData.Ins.PlayerData.EquimentDatas[i].range, 0.5f);
                cameraFollow.GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance = SaveLoadData.Ins.PlayerData.EquimentDatas[i].offset;
            }
        }
        for (int j = 0; j < equiments.Count; j++)
        {
            if (equimentType.ToString() == equiments[j].name)
            {
                equiments[j].SetActive(true);
            }
            else
            {
                equiments[j].SetActive(false);
            }
        }

    }
    Tween OnEnableTween;
    public void SetEquiment(EquimentType equimentType)
    {
        OnEnableTween?.Kill();
        for (int i = 0; i < SaveLoadData.Ins.PlayerData.EquimentDatas.Count; i++)
        {
            if (SaveLoadData.Ins.PlayerData.EquimentDatas[i].equimentType == equimentType)
            {
                SaveLoadData.Ins.PlayerData.EquiType = equimentType;
                anim.runtimeAnimatorController = SaveLoadData.Ins.PlayerData.EquimentDatas[i].anim;
                damage = SaveLoadData.Ins.PlayerData.EquimentDatas[i].damage;
                OnEnableTween = attackZone.transform.DOScale(SaveLoadData.Ins.PlayerData.EquimentDatas[i].range, 0.5f);

            }
        }
        for (int j = 0; j < equiments.Count; j++)
        {
            if (equimentType.ToString() == equiments[j].name)
            {
                equiments[j].SetActive(true);
            }
            else
            {
                equiments[j].SetActive(false);
            }
        }

    }
    private void ResetAttack()
    {
        isAttack = false;
        attackTimer = attackSpeed;
    }
    private bool IsMoveForward()
    {
        float angle = Vector3.Angle(movement, direction);
        return angle < 90f;
    }
    // private void AnimControl(){
    //     if (GetMove())
    //     {
    //         if(attackZone.enemy.Count > 0)
    //         {
    //             if(isMoveForward())
    //             {
    //                 ChangeAnim(Constants.ANIM_RUNFW);
    //             }else{
    //                 ChangeAnim(Constants.ANIM_RUNBW);
    //             }
    //         }else{
    //             ChangeAnim(Constants.ANIM_RUNFW);
    //         }
    //     }
    //     else
    //     {
    //         // if (isAttack)
    //         // {
    //         //     ChangeAnim(Constants.ANIM_ATTACK);
    //         // }else
    //         // {
    //         //     ChangeAnim(Constants.ANIM_IDLE);
    //         // }
    //     }
    // }
    public override void OnHit(float damage)
    {
        if (!IsDead && GameManager.IsState(GameState.Playing))
        {
            hp -= damage;
            characterHit.SetHp(hp);
            timeHealling = 1f;
            if (IsDead)
            {
                OnDead();
            }
        }
    }
    [Button]
    protected override void OnDead()
    {
        ChangeAnim(Constants.ANIM_DEAD);
        GameManager.ChangeState(GameState.Pause);
        Invoke(nameof(OnDespawn), 5f);
    }
    protected override void OnDespawn()
    {
        UIManager.Ins.OpenUI<UILose>();

    }
    private void Healling()
    {
        if (IsDead) return;
        if (hp < maxHp)
        {
            timeHealling -= Time.fixedDeltaTime;
            if (timeHealling < 0)
            {
                hp += Time.fixedDeltaTime * SaveLoadData.Ins.PlayerData.RegenHp;
                characterHit.SetHp(hp);
                if (hp >= maxHp)
                {
                    timeHealling = 1f;
                    characterHit.HitPointBar.SetActive(false);
                }
            }
        }
    }
    public bool GetMove()
    {
        return Mathf.Abs(horizontal) > 0.01f || Mathf.Abs(vertical) > 0.01f;
    }
    public void SetArmor(int lv)
    {
        if (lv > 1)
        {
            armor[0].SetActive(true);
        }
        else if (lv > 4)
        {
            armor[0].SetActive(true);
            armor[1].SetActive(true);
        }
        else if (lv > 9)
        {
            armor[0].SetActive(true);
            armor[1].SetActive(true);
            armor[2].SetActive(true);
        }
        else if (lv > 14)
        {
            armor[0].SetActive(true);
            armor[1].SetActive(true);
            armor[2].SetActive(true);
            armor[3].SetActive(true);
        }
        else if (lv > 19)
        {
            armor[0].SetActive(true);
            armor[1].SetActive(true);
            armor[2].SetActive(true);
            armor[3].SetActive(true);
            armor[4].SetActive(true);
        }

    }
}
