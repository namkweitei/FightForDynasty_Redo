using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;
using System;

[Serializable]
public class WaveData
{
    public List<EnemyCate> firstWave;
    public List<Barrack> barrackWave;
    public List<EnemyCate> endWave;
}
public enum MapState
{
    Start,
    During,
    End,
}
public class Map : MonoBehaviour
{
    [SerializeField] public int id;
    [SerializeField] public MapState mapState;
    StateMachine stateMachine = new StateMachine();
    [Header("------Map------")]
    [SerializeField] private Transform gate;
    [SerializeField] private MeshRenderer plane;
    [SerializeField] private Material material;
    [SerializeField] private LoadAttackPoint attackPoint;
    [SerializeField] private Transform spawnPos;
    [SerializeField] private Flag flag;
    [SerializeField] private List<Enemy> listEnemy = new List<Enemy>();
    [Header("------Wave------")]
    [SerializeField] WaveData waveData;
    [Header("------EnemyPos------")]
    [SerializeField] private List<Transform> firstEnemyPos;
    [SerializeField] private List<Transform> endEnemyPos;
    [SerializeField] private List<UpTowerSystem> towerSystems;
    [SerializeField] private Transform playerPoint;
    [SerializeField] private Transform campPoint;
    [SerializeField] private Avoidance avoidance;
    int currentBarrack;
    int enemyCount;
    int enemyCountWave;
    int totalEnemy;
    int currentPos;
    bool isOpenMap;
    public List<Enemy> ListEnemy { get => listEnemy; set => listEnemy = value; }
    private float timeNextWave;

    Tween OnEnableTween;
    private void Start()
    {
        attackPoint.Map = this;
    }
    public void SetPosPlayer()
    {
        Player.Ins.transform.position = playerPoint.position;
        Player.Ins.transform.rotation = playerPoint.rotation;
        Player.Ins.OnInit();
    }
    public void SetPosCampCharacter()
    {
        CampCharacter.Ins.transform.position = campPoint.position;
        CampCharacter.Ins.transform.rotation = campPoint.rotation;
        CampCharacter.Ins.OnInit();
    }
    [Button]
    public void MoveCampCharacter()
    {
        CampCharacter.Ins.cameraFollow.enabled = true;

        DOVirtual.DelayedCall(2.5f, () =>
        {
            OnEnableTween?.Kill();
            OnEnableTween = CampCharacter.Ins.transform.DOScale(0, 1).SetEase(CampCharacter.Ins.curveIn).OnComplete(() =>
            {
                CampCharacter.Ins.transform.DOMove(campPoint.position, 3f).OnComplete(() =>
                {
                    CampCharacter.Ins.transform.rotation = campPoint.rotation;
                    CampCharacter.Ins.transform.DOScale(1, 1).SetEase(CampCharacter.Ins.curveOut);
                    CampCharacter.Ins.OnInit();
                    DOVirtual.DelayedCall(2.5f, () =>
                    {
                        CampCharacter.Ins.cameraFollow.enabled = false;

                    });
                });
            });
        });
    }
    public void OnInit(MapState state)
    {
        switch (state)
        {
            case MapState.Start:
                for (int i = 0; i < towerSystems.Count; i++)
                {
                    towerSystems[i].SetCurrentTower(SaveLoadData.Ins.TowerData[towerSystems[i].Id]);
                }
                for (int i = 0; i < waveData.barrackWave.Count; i++)
                {
                    waveData.barrackWave[i].map = this;
                }
                for (int i = 0; i < SaveLoadData.Ins.MapData.Wave - 1; i++)
                {
                    waveData.barrackWave[i].gameObject.SetActive(false);
                }
                if (SaveLoadData.Ins.MapData.Level == id)
                {
                    if (SaveLoadData.Ins.MapData.Wave == 0)
                    {
                        stateMachine.ChangeState(FirstWaveState);
                    }
                    else if (SaveLoadData.Ins.MapData.Wave > 0)
                    {
                        currentBarrack = SaveLoadData.Ins.MapData.Wave - 1;
                        stateMachine.ChangeState(BarrackWaveState);
                    }
                }
                DirectionArrowControl.Ins.SetPos(attackPoint.transform, Player.Ins.transform);
                DirectionArrowControl.Ins.gameObject.SetActive(true);
                isOpenMap = false;
                attackPoint.Map = this;
                attackPoint.gameObject.SetActive(true);
                SetBarack(currentBarrack);
                break;


            case MapState.During:
                for (int i = 0; i < towerSystems.Count; i++)
                {
                    towerSystems[i].SetCurrentTower(SaveLoadData.Ins.TowerData[towerSystems[i].Id]);
                }
                for (int i = 0; i < waveData.barrackWave.Count; i++)
                {
                    waveData.barrackWave[i].gameObject.SetActive(false);
                }
                flag.gameObject.SetActive(false);
                this.gate.gameObject.SetActive(false);
                plane.materials = new Material[] { material, material };
                if (attackPoint != null)
                {
                    attackPoint.gameObject.SetActive(false);

                }
                this.enabled = false;
                break;

            case MapState.End:
                for (int i = 0; i < towerSystems.Count; i++)
                {
                    towerSystems[i].OnInit(SaveLoadData.Ins.TowerData[towerSystems[i].Id]);
                }
                for (int i = 0; i < waveData.barrackWave.Count; i++)
                {
                    waveData.barrackWave[i].gameObject.SetActive(false);
                }
                flag.gameObject.SetActive(false);
                this.gate.gameObject.SetActive(false);
                plane.materials = new Material[] { material, material };
                if (attackPoint != null)
                {
                    attackPoint.gameObject.SetActive(false);

                }
                this.enabled = false;
                break;
        }
    }
    // public void ActiveMap(){
    //     for(int i = 0; i < towerSystems.Count; i++){
    //             for(int j = 0; j < SaveLoadData.Ins.MapData.TowerDatas.Count; j++){
    //                 if(towerSystems[i].towerData.id == SaveLoadData.Ins.MapData.TowerDatas[j].id){
    //                     towerSystems[i].SetCurrentTower(SaveLoadData.Ins.MapData.TowerDatas[j].Level);
    //                 }
    //             }
    //     }
    //     for(int i = 0; i < waveData.barrackWave.Count; i++){
    //         waveData.barrackWave[i].gameObject.SetActive(false);
    //     }
    //     flag.gameObject.SetActive(false);
    //     this.gate.gameObject.SetActive(false);
    //     plane.materials = new Material[]{material, material};
    //     attackPoint.gameObject.SetActive(false);
    //     this.enabled = false;
    // }
    // public void Init(){
    //     for(int i = 0; i < towerSystems.Count; i++){
    //         for(int j = 0; j < SaveLoadData.Ins.MapData.TowerDatas.Count; j++){
    //             if(towerSystems[i].towerData.id == SaveLoadData.Ins.MapData.TowerDatas[j].id){
    //                 towerSystems[i].OnInit(SaveLoadData.Ins.MapData.TowerDatas[j].Level);
    //             }
    //         }
    //     }
    //     for(int i = 0; i < waveData.barrackWave.Count; i++){
    //         waveData.barrackWave[i].gameObject.SetActive(false);
    //     }
    //     flag.gameObject.SetActive(false);
    //     this.gate.gameObject.SetActive(false);
    //     plane.materials = new Material[]{material, material};
    //     attackPoint.gameObject.SetActive(false);
    //     this.enabled = false;
    // }
    // public void SetMap(){
    //     currentBarrack = 0;
    //     stateMachine.ChangeState(FirstWaveState);
    //     isOpenMap = false;
    //     attackPoint.gameObject.SetActive(false);
    //     SetBarack(currentBarrack);
    // }
    // public void SetCurrentMap(){
    //     for(int i = 0; i < towerSystems.Count; i++){
    //         for(int j = 0; j < SaveLoadData.Ins.MapData.TowerDatas.Count; j++){
    //             if(towerSystems[i].towerData.id == SaveLoadData.Ins.MapData.TowerDatas[j].id){
    //                 towerSystems[i].SetCurrentTower(SaveLoadData.Ins.MapData.TowerDatas[j].Level);
    //             }
    //         }
    //     }
    //     for(int i = 0; i < SaveLoadData.Ins.MapData.CurrentMap.Wave; i++){
    //         waveData.barrackWave[i].gameObject.SetActive(false);
    //     }
    //     if(SaveLoadData.Ins.MapData.CurrentMap.id == id){
    //         currentBarrack = SaveLoadData.Ins.MapData.CurrentMap.Wave;
    //         stateMachine.ChangeState(BarrackWaveState);
    //     }else{
    //         currentBarrack = 0;
    //         stateMachine.ChangeState(FirstWaveState);

    //     }
    //     Player.Ins.transform.position = playerPoint.position;
    //     Player.Ins.transform.rotation = playerPoint.rotation;
    //     Player.Ins.OnInit();
    //     CampCharacter.Ins.transform.position = campPoint.position;
    //     CampCharacter.Ins.transform.rotation = campPoint.rotation;
    //     CampCharacter.Ins.OnInit();
    //     DirectionArrowControl.Ins.gameObject.SetActive(true);
    //     DirectionArrowControl.Ins.SetPos(attackPoint.transform, Player.Ins.transform);
    //     isOpenMap = false;
    //     attackPoint.gameObject.SetActive(true);
    //     SetBarack(currentBarrack);
    // }


    // Update is called once per frame
    void Update()
    {
        stateMachine?.Execute();
    }
    // Open Map
    public virtual void OpenMap()
    {

        UIManager.Ins.GetUI<UIGamePlay>().OpenWaveStage(waveData.barrackWave.Count + 1, SaveLoadData.Ins.MapData.Wave);
        DirectionArrowControl.Ins.gameObject.SetActive(false);
        GameManager.ChangeState(GameState.Playing);
        GameManager.Ins.currenGameState = GameState.Playing;
        OnEnableTween = this.gate.DOScaleY(0, 0.5f).OnComplete(() =>
        {
            this.gate.gameObject.SetActive(false);
            isOpenMap = true;
            Invoke(nameof(ActiveFirstEnemy), 0.1f);
        });

    }
    //--------------FIRST WAVE---------------
    public virtual void ActiveFirstEnemy()
    {
        for (int i = 0; i < listEnemy.Count; i++)
        {
            listEnemy[i].StateMachine.ChangeState(listEnemy[i].PatrolState);
        }
    }
    public virtual void SetFirstWave()
    {
        currentPos = 0;
        for (int i = 0; i < waveData.firstWave.Count; i++)
        {
            for (int j = 0; j < waveData.firstWave[i].enemyCount; j++)
            {
                if (j < firstEnemyPos.Count)
                {
                    Enemy enemy = SpawnEnemy(waveData.firstWave[i].enemyName, firstEnemyPos[currentPos].position, firstEnemyPos[currentPos].rotation);
                    enemy.SetTarget(campPoint);
                    enemy.StateMachine.ChangeState(enemy.IdleState);
                    currentPos++;
                }
            }
        }
    }
    //----------------BARRACK WAVE-------------
    public virtual void ActiveAttackWave()
    {
        waveData.barrackWave[currentBarrack].SpawnEnemy();
    }

    public void SetBarack(int index)
    {
        for (int i = index; i < waveData.barrackWave.Count; i++)
        {
            waveData.barrackWave[i].isSpawnEnemy = true;
            waveData.barrackWave[i].map = this;
        }
    }
    //-----------------END WAVE-------------------
    public virtual void SetEndEnemy()
    {
        currentPos = 0;
        for (int i = 0; i < waveData.endWave.Count; i++)
        {
            for (int j = 0; j < waveData.endWave[i].enemyCount; j++)
            {
                if (j < endEnemyPos.Count)
                {
                    Enemy enemy = SpawnEnemy(waveData.endWave[i].enemyName, spawnPos.position, spawnPos.rotation);
                    enemy.SetTarget(endEnemyPos[currentPos]);
                    enemy.StateMachine.ChangeState(enemy.FollowState);
                    currentPos++;
                }

            }
        }
    }
    public virtual void OpenFlag()
    {
        flag.enabled = true;
        flag.Map = this;
    }

    [Button]
    public virtual void CloseMap()
    {
        UIManager.Ins.GetUI<UIGamePlay>().CloseWaveStage();
        DirectionArrowControl.Ins.gameObject.SetActive(false);
        flag.enabled = false;
        plane.materials = new Material[] { material, material };
        attackPoint.gameObject.SetActive(true);
        SaveLoadData.Ins.MapData.Level++;
        SaveLoadData.Ins.MapData.Wave = 0;
        UIManager.Ins.OpenUI<UIWinn>();
        if (GameManager.IsState(GameState.Playing))
        {
            GameManager.ChangeState(GameState.Pause);
        }
    }

    //-----------------Enemy------------------

    public Enemy SpawnEnemy(PoolType poolType, Vector3 position, Quaternion rotation)
    {
        Enemy enemy = SmartPool.Ins.Spawn<Enemy>(poolType, position, rotation);
        listEnemy.Add(enemy);
        enemy.Avoidance = GameManager.Ins.Avoidance;
        enemy.OnDeathAction = OnEnemyDeath;
        enemy.OnInit();
        return enemy;
    }

    public void OnEnemyDeath(Enemy enemy)
    {
        listEnemy.Remove(enemy);
        enemyCount--;
        UIManager.Ins.GetUI<UIGamePlay>().Fill(((float)enemyCountWave - (float)enemyCount) / (float)enemyCountWave);
    }

    //-----------------StateMachine------------------
    private void FirstWaveState(ref Action onEnter, ref Action onExecute, ref Action onExit)
    {
        onEnter = () =>
        {
            SetFirstWave();
            timeNextWave = 2f;
            enemyCount = 0;
            for (int i = 0; i < waveData.firstWave.Count; i++)
            {
                this.enemyCount += waveData.firstWave[i].enemyCount;
            }
            enemyCountWave = enemyCount;
        };

        onExecute = () =>
        {
            if (ListEnemy.Count == 0)
            {
                timeNextWave -= Time.deltaTime;
                if (timeNextWave <= 0)
                {
                    stateMachine.ChangeState(BarrackWaveState);
                    timeNextWave = 2f;
                }
            }
        };

        onExit = () =>
        {
            // SaveLoadData.Ins.MapData.CurrentMap.id = id;
            SaveLoadData.Ins.MapData.Wave++;
            UIManager.Ins.GetUI<UIGamePlay>().SetFill(SaveLoadData.Ins.MapData.Wave);
            currentBarrack = 0;
        };
    }


    private void BarrackWaveState(ref Action onEnter, ref Action onExecute, ref Action onExit)
    {
        onEnter = () =>
        {
            enemyCount = 0;
            for (int i = 0; i < waveData.barrackWave[currentBarrack].enemyCates.Count; i++)
            {
                this.enemyCount += waveData.barrackWave[currentBarrack].enemyCates[i].enemyCount;
            }
            enemyCountWave = enemyCount;
        };

        onExecute = () =>
        {
            if (!isOpenMap) return;
            ActiveAttackWave();
            if (currentBarrack == waveData.barrackWave.Count - 1 && waveData.barrackWave[currentBarrack].isSpawnEnemy == false)
            {
                stateMachine.ChangeState(EndWaveState);
                SetEndEnemy();
            }
            if (enemyCount <= 0)
            {
                timeNextWave -= Time.deltaTime;
                if (timeNextWave <= 0)
                {
                    if (currentBarrack < waveData.barrackWave.Count - 1)
                    {
                        currentBarrack++;
                        SaveLoadData.Ins.MapData.Wave++;
                        for (int i = 0; i < waveData.barrackWave[currentBarrack].enemyCates.Count; i++)
                        {
                            enemyCount += waveData.barrackWave[currentBarrack].enemyCates[i].enemyCount;
                        }
                        enemyCountWave = enemyCount;
                    }
                    timeNextWave = 2f;
                }
            }

        };

        onExit = () =>
        {

        };
    }
    private void EndWaveState(ref Action onEnter, ref Action onExecute, ref Action onExit)
    {
        onEnter = () =>
        {

        };

        onExecute = () =>
        {
            if (enemyCount <= 0 && !DirectionArrowControl.Ins.gameObject.activeSelf)
            {
                DirectionArrowControl.Ins.gameObject.SetActive(true);
                DirectionArrowControl.Ins.SetPos(flag.transform, Player.Ins.transform);
            }
            if (ListEnemy.Count == 0)
            {
                Debug.Log("Win");
                OpenFlag();
                this.enabled = false;
            }
        };
        onExit = () =>
        {

        };
    }

}