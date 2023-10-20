using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;

public class CampCharacter : Singleton<CampCharacter>
{
    [SerializeField] protected CampCharacterHit characterHit;
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected Transform healthBar;
    [SerializeField] protected float timeHealling;
    [SerializeField] protected float speedHealling;
    [SerializeField] protected List<UpTowerSystem> towerSystems;
    [SerializeField] protected DataCampCharacter dataCampCharacter;
    [SerializeField] public CinemachineVirtualCamera cameraFollow;
    [SerializeField] public AnimationCurve curveIn;
    [SerializeField] public AnimationCurve curveOut;

    public bool isUpdate;

    public bool IsDead => SaveLoadData.Ins.CampCharacterData.Hp <= 0;
    protected override void Awake()
    {
        base.Awake();
        cameraFollow.enabled = false;
    }
    private void Start()
    {
        OnInit();

    }
    public virtual void OnInit()
    {
        characterHit.OnInit(SaveLoadData.Ins.CampCharacterData.MaxHp, SaveLoadData.Ins.CampCharacterData.Level);
        SaveLoadData.Ins.CampCharacterData.Hp = SaveLoadData.Ins.CampCharacterData.MaxHp;
        characterHit.SetHp(SaveLoadData.Ins.CampCharacterData.Hp);
        towerSystems[0].SetCurrentTower(SaveLoadData.Ins.TowerData[towerSystems[0].Id]);
        towerSystems[1].SetCurrentTower(SaveLoadData.Ins.TowerData[towerSystems[1].Id]);
    }
    public void ResetTower()
    {
        SaveLoadData.Ins.TowerData[towerSystems[0].Id] = 0;
        SaveLoadData.Ins.TowerData[towerSystems[1].Id] = 0;
        towerSystems[0].SetCurrentTower(SaveLoadData.Ins.TowerData[towerSystems[0].Id]);
        towerSystems[1].SetCurrentTower(SaveLoadData.Ins.TowerData[towerSystems[1].Id]);
    }
    public virtual void OnHit(float damage)
    {
        if (!IsDead && GameManager.IsState(GameState.Playing))
        {
            SaveLoadData.Ins.CampCharacterData.Hp -= damage;
            characterHit.SetHp(SaveLoadData.Ins.CampCharacterData.Hp);
            if (IsDead)
            {
                OnDead();
            }
        }
    }
    public void SetLevel()
    {
        LevelUp();
        characterHit.SetLevel(SaveLoadData.Ins.CampCharacterData.Level);
    }
    public void Healling()
    {
        if (IsDead) return;
        if (SaveLoadData.Ins.CampCharacterData.Hp < SaveLoadData.Ins.CampCharacterData.MaxHp)
        {
            timeHealling -= Time.fixedDeltaTime;
            if (timeHealling < 0)
            {
                timeHealling = speedHealling;
                SaveLoadData.Ins.CampCharacterData.Hp++;
                SaveLoadData.Ins.PlayerData.Coin--;
                SpawnCoin(Player.Ins.transform, transform);
                characterHit.SetHp(SaveLoadData.Ins.CampCharacterData.Hp);
            }
        }
    }
    public void SpawnCoin(Transform start, Transform end)
    {
        FakeCoin fakeCoin = SmartPool.Ins.Spawn<FakeCoin>(PoolType.FakeCoin, start.transform.position + Vector3.up, Quaternion.identity);
        fakeCoin.OnInit(start, end);
    }
    protected virtual void OnDead()
    {
        Invoke(nameof(OnDespawn), 5f);
        GameManager.ChangeState(GameState.Pause);
    }
    protected void OnDespawn()
    {
        UIManager.Ins.OpenUI<UILose>();

    }
    private void OnTriggerEnter(Collider other)
    {
        healthBar.DOScale(1, 0.5f);
    }
    private void OnTriggerExit(Collider other)
    {
        healthBar.DOScale(0, 0.5f);
    }
    protected void LevelUp()
    {
        SaveLoadData.Ins.CampCharacterData.Level++;
        dataCampCharacter.SetInforLevel(SaveLoadData.Ins.CampCharacterData.Level);
        #region SwithCase
        // switch(SaveLoadData.Ins.CampCharacterData.Level){
        //     case 1:
        //         SaveLoadData.Ins.CampCharacterData.MaxHp = 80;
        //         SaveLoadData.Ins.CampCharacterData.Coin = 20;
        //         OnInit();
        //         break;
        //     case 2:
        //         SaveLoadData.Ins.CampCharacterData.MaxHp = 120;
        //         SaveLoadData.Ins.CampCharacterData.Coin = 40;
        //         OnInit();
        //         break;
        //     case 3:
        //         SaveLoadData.Ins.CampCharacterData.MaxHp = 160;
        //         SaveLoadData.Ins.CampCharacterData.Coin = 60;
        //         OnInit();
        //         break;
        //     case 4:
        //         SaveLoadData.Ins.CampCharacterData.MaxHp = 200;
        //         SaveLoadData.Ins.CampCharacterData.Coin = 100;
        //         OnInit();
        //         break;
        //     case 5:
        //         SaveLoadData.Ins.CampCharacterData.MaxHp = 250;
        //         SaveLoadData.Ins.CampCharacterData.Coin = 150;
        //         OnInit();
        //         break;
        //     case 6:
        //         SaveLoadData.Ins.CampCharacterData.MaxHp = 310;
        //         SaveLoadData.Ins.CampCharacterData.Coin = 210;
        //         OnInit();
        //         break;
        //     case 7:
        //         SaveLoadData.Ins.CampCharacterData.MaxHp = 380;
        //         SaveLoadData.Ins.CampCharacterData.Coin = 280;             
        //         OnInit();
        //         break;
        //     case 8:
        //         SaveLoadData.Ins.CampCharacterData.MaxHp = 460;
        //         SaveLoadData.Ins.CampCharacterData.Coin = 360;    
        //         OnInit();
        //         break;
        //     case 9:
        //         SaveLoadData.Ins.CampCharacterData.MaxHp = 550;
        //         SaveLoadData.Ins.CampCharacterData.Coin = 440;  
        //         OnInit();
        //         break;
        //     case 10:
        //         SaveLoadData.Ins.CampCharacterData.MaxHp = 650;
        //         SaveLoadData.Ins.CampCharacterData.Coin = 530;  
        //         OnInit();
        //         break;
        //     case 11:
        //         SaveLoadData.Ins.CampCharacterData.MaxHp = 760;
        //         SaveLoadData.Ins.CampCharacterData.Coin = 630;
        //         OnInit();
        //         break;
        //     case 12:
        //         SaveLoadData.Ins.CampCharacterData.MaxHp = 870;
        //         SaveLoadData.Ins.CampCharacterData.Coin = 740;
        //         OnInit();
        //         break;
        //     case 13:
        //         SaveLoadData.Ins.CampCharacterData.MaxHp = 980;
        //         SaveLoadData.Ins.CampCharacterData.Coin = 850;
        //         OnInit();
        //         break;
        //     case 14:
        //         SaveLoadData.Ins.CampCharacterData.MaxHp = 1090;
        //         SaveLoadData.Ins.CampCharacterData.Coin = 900;
        //         OnInit();
        //         break;
        //     case 15:
        //         SaveLoadData.Ins.CampCharacterData.MaxHp = 1200;
        //         SaveLoadData.Ins.CampCharacterData.Coin = 960;  
        //         OnInit();
        //         break;
        //     case 16:
        //         SaveLoadData.Ins.CampCharacterData.MaxHp = 1320;
        //         SaveLoadData.Ins.CampCharacterData.Coin = 1070; 
        //         OnInit();
        //         break;
        //     case 17:
        //         SaveLoadData.Ins.CampCharacterData.MaxHp = 1450;
        //         SaveLoadData.Ins.CampCharacterData.Coin = 1200;
        //         OnInit();
        //         break;
        // }
        #endregion
    }
}
