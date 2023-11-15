using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
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
    [SerializeField] private float timeHealing;
    [SerializeField] private SphereCollider attackRange;
    [SerializeField] private Transform ring;
    [SerializeField] private CinemachineVirtualCamera cameraFollow;
    [SerializeField] private List<GameObject> equiments;
    [SerializeField] private List<GameObject> armor;
    [SerializeField] private List<Character> listEnemys;
    [SerializeField] private Character target;
    [SerializeField] private Animator bowAnimation;
    public List<GameObject> Armor { get => armor; set => armor = value; }
    public bool IsMove { get => isMove; set => isMove = value; }

    private float horizontal;
    private float vertical;
    private Vector3 movement;
    private Vector3 direction;
    public bool isAttack;
    private float rangeAttack;
    private bool isMove;
    private void Start()
    {
        ChangeEquiment(SaveLoadData.Ins.PlayerData.EquiType);
        OnInit();
        SetArmor(SaveLoadData.Ins.PlayerData.Level);
        
    }
    public override void OnInit()
    {
        isMove = true;
        isAttack = false;
        maxHp = SaveLoadData.Ins.PlayerData.Hp;
        speed = SaveLoadData.Ins.PlayerData.Speed;
        base.OnInit();
    }
    private void Update()
    {
        if (IsDead ) return;
        if(isMove){
            horizontal = UltimateJoystick.GetHorizontalAxisRaw("PlayerJoystick");
            vertical = UltimateJoystick.GetVerticalAxisRaw("PlayerJoystick");
        }else{
            horizontal = 0;
            vertical = 0;
        }

    }

    private void FixedUpdate()
    {
        if (IsDead) return;
        JoystickMovement();
        Rotate();
        Healling();
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

        }else{
            if (Mathf.Abs(horizontal) < 0.01f & Mathf.Abs(vertical) < 0.01f)
            {
                anim.Play("idle",0);
            }
            else
            {
                anim.Play("run", 0);
            }
        }

    }

    protected virtual void JoystickMovement()
    {
            movement = new Vector3(-horizontal, 0f, -vertical);
            movement.Normalize();
            rb.velocity = movement * speed * Time.fixedDeltaTime;
        
    }

    protected virtual void Rotate()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, rangeAttack);
        foreach (var collider in hitColliders)
        {
            Character character = collider.GetComponent<Character>();
            if (character != null && character.CompareTag("Enemy") )
            {
                if(!listEnemys.Contains(character)){
                    listEnemys.Add(character);
                }
               
            }
        }
        if (listEnemys.Count > 0 && GameManager.IsState(GameState.Playing))
        {
            if (listEnemys[0].IsDead || Vector3.Distance(transform.position, listEnemys[0].transform.position) > rangeAttack )
            {
                listEnemys.Remove(listEnemys[0]);
                attackTimer = attackSpeed;
            }
            else
            {
                attackTimer -= Time.fixedDeltaTime;
                if(attackTimer < 0){
                    Attack(listEnemys[0]);
                }
                direction = listEnemys[0].transform.position - TF.position;
                direction.y = 0f;
                direction.Normalize();
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                TF.rotation = Quaternion.Lerp(TF.rotation, targetRotation, Time.deltaTime * 20);
            }
        }
        else if (horizontal != 0 || vertical != 0 && listEnemys.Count > 0)
        {
            float targetAngle = Mathf.Atan2(-horizontal, -vertical) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0f, targetAngle, 0f);
            TF.rotation = Quaternion.Lerp(TF.rotation, targetRotation, Time.deltaTime * 10);
        }
        else if (Mathf.Abs(horizontal) > 0.01f || Mathf.Abs(vertical) > 0.01f )
        {
            if(listEnemys.Count <= 0){
                ResetAttack();
            }
           
        }
    }
    private void Attack(Character target)
    {
        if (!isAttack)
        {
            isAttack = true;
            ChangeAnim(Constants.ANIM_ATTACKIDLE);
            //anim.Play("attack", 1);
            // if (GetMove())
            // {
            //     ChangeAnim(Constants.ANIM_ATTACKRUN);
            // }
            // else
            // {
            //     ChangeAnim(Constants.ANIM_ATTACKIDLE);
            // }
            this.target = target; 
            Invoke(nameof(ResetAttack), 1f);
        }
    }
    public void ResetAttack()
    {
        Debug.Log("vao day2");
        isAttack = false;
        if (GetMove() )
        {
            if(!IsDead){

            ChangeAnim(Constants.ANIM_RUNFW);
            }
        }
        else
        {
            if(!IsDead){
            ChangeAnim(Constants.ANIM_IDLE);}
        }
        attackTimer = attackSpeed;
    }
    public void StartBow(){
        bowAnimation.ResetTrigger("Bow");
        bowAnimation.SetTrigger("Bow");
    }
    public void DealDmg()
    {
        target.OnHit(damage);
    }
    public void ShootArrow(){
         if (!target.IsDead)
        {
            Bullet bullet = null;
            Transform shootPoint = null;
            switch (SaveLoadData.Ins.PlayerData.EquiType)
            {
                case EquimentType.Bow:
                    shootPoint = shootPointBow;
                    break;
                case EquimentType.Crossbow:
                    shootPoint = shootPointCrossBow;
                    break;
            }

            if (shootPoint != null)
            {
                Debug.Log("vao day1");
                bullet = SmartPool.Ins.Spawn<Bullet>(PoolType.Arrow, shootPoint.position, Quaternion.LookRotation(direction));
                AudioManager.Ins.PlaySfx(Constants.SFX_SHOOT);
                if (bullet != null)
                {
                    bullet.OnInit(target, damage);
                }
            }
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
                //attackSpeed = SaveLoadData.Ins.PlayerData.EquimentDatas[i].attackSpeed;
                attackZone.transform.DOScale(SaveLoadData.Ins.PlayerData.EquimentDatas[i].range, 0.5f);
                rangeAttack = SaveLoadData.Ins.PlayerData.EquimentDatas[i].range;
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
    private bool IsMoveForward()
    {
        float angle = Vector3.Angle(movement, direction);
        return angle < 90f;
    }

    public override void OnHit(float damage)
    {
        if (!IsDead && GameManager.IsState(GameState.Playing))
        {
            hp -= damage;
            characterHit.SetHp(hp);
            timeHealing = 1f;
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
        UIManager.Ins.GetUI<UILose>().StartPobUp();

    }
    private void Healling()
    {
       if (IsDead) return;
        if (hp < maxHp)
        {
            timeHealing -= Time.fixedDeltaTime;
            if (timeHealing < 0)
            {
                hp += Time.fixedDeltaTime * SaveLoadData.Ins.PlayerData.RegenHp;
                characterHit.SetHp(hp);
                if (hp >= maxHp)
                {
                    timeHealing = 1f;
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
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangeAttack);
    }
}
