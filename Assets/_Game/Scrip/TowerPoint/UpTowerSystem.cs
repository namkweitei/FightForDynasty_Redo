using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class UpTowerSystem : MonoBehaviour
{
    [SerializeField] private int id;
    [SerializeField] public int Level;
    [SerializeField] public List<Tower> towerPrefabs;
    [SerializeField] private ParticleSystem buildEffect;
    [SerializeField] public int towerLevel;
    [SerializeField] private UpdateTower uptower;
    [SerializeField] private Transform circle;
    [SerializeField] private bool hasTower;
    [SerializeField] private int currentCoin;
    [SerializeField] private GameObject upgrateTower;
    [SerializeField] private float timeSpawn;
    [SerializeField] private float targetTime;
    [SerializeField] private TowerPopUp towerPopUp;
    public int Id { get => id; set => id = value; }
    private bool isActive;
    public void OnInit(int lv)
    {
        if (lv > 0)
        {
            Level = SaveLoadData.Ins.TowerData[Id];
            hasTower = true;
            towerPrefabs[lv - 1].gameObject.SetActive(true);
            towerPrefabs[lv - 1].transform.DOScale(1, 1f);
            towerPopUp.OnInit(towerPrefabs[lv - 1].Damage, towerPrefabs[lv - 1].ShootSpeed, lv + 1);
            upgrateTower.gameObject.SetActive(false);
            circle.gameObject.SetActive(false);
            buildEffect.Stop();
        }
        else
        {
            gameObject.SetActive(false);
            buildEffect.Stop();
            upgrateTower.gameObject.SetActive(false);
        }
    }
    public void SetCurrentTower(int lv)
    {
        // for(int i = 0; i < SaveLoadData.Ins.mapData.TowerDatas.Count; i++){
        //         if(towerData.id == SaveLoadData.Ins.mapData.TowerDatas[i].id){
        //             towerData.Level = SaveLoadData.Ins.mapData.TowerDatas[i].Level;

        //         }
        // }
        Level = SaveLoadData.Ins.TowerData[Id];
        if (lv > 0)
        {
            hasTower = true;
            towerPrefabs[lv - 1].gameObject.SetActive(true);
            towerPrefabs[lv - 1].transform.DOScale(1, 1f);
            towerPopUp.OnInit(towerPrefabs[lv - 1].Damage, towerPrefabs[lv - 1].ShootSpeed, lv + 1 );
            if (Level >= towerPrefabs.Count)
            {
                upgrateTower.gameObject.SetActive(false);
            }
            else
            {
                upgrateTower.gameObject.SetActive(true);
            }
            circle.gameObject.SetActive(false);
            buildEffect.Stop();
        }
        else
        {
            hasTower = false;
            uptower.OnInit(currentCoin);
            upgrateTower.gameObject.SetActive(false);
            Level = 0;
            buildEffect.Stop();
        }
    }
    private void Update()
    {
        if (isActive)
        {
            LoadCircle();
        }
    }
    private void LoadCircle()
    {
        if (!hasTower)
        {
            uptower.LoadTower();
            if (uptower.LoadCircle.fillAmount >= 1)
            {
                if (currentCoin > 0)
                {
                    if (SaveLoadData.Ins.PlayerData.Coin > 0)
                    {
                        timeSpawn -= Time.deltaTime;
                        if (timeSpawn < 0)
                        {
                            timeSpawn = targetTime;
                            SpawnCoin(Player.Ins.transform, transform);
                            currentCoin--;
                            SaveLoadData.Ins.PlayerData.Coin--;
                            uptower.SetCurrentCoin(currentCoin);
                        }
                    }

                }
                else
                {
                    isActive = false;
                    hasTower = true;
                    circle.gameObject.SetActive(false);
                    upgrateTower.gameObject.SetActive(true);
                    SetTower();
                    uptower.LoadCircle.fillAmount = 0;
                }
            }
        }
    }
    public void SetTower()
    {
        if (Level < towerPrefabs.Count)
        {
            if (Level > 0)
            {
                towerPrefabs[Level - 1].gameObject.SetActive(false);
            }
            buildEffect.Play();
            towerPrefabs[Level].gameObject.SetActive(true);
            towerPrefabs[Level].transform.DOScale(1, 1f);
            towerPopUp.OnInit(towerPrefabs[Level].Damage, towerPrefabs[Level].ShootSpeed, Level + 1 );
            SaveLoadData.Ins.TowerData[Id]++;
            Debug.Log(SaveLoadData.Ins.TowerData[Id]);
            if (Level >= towerPrefabs.Count)
            {
                upgrateTower.gameObject.SetActive(false);
            }
            Debug.Log(".");
        }
    }
    public bool CheckTower()
    {
        if (Level < towerPrefabs.Count)
        {
            return true;
        }
        return false;
    }
    public void SpawnCoin(Transform start, Transform end)
    {
        FakeCoin fakeCoin = SmartPool.Ins.Spawn<FakeCoin>(PoolType.FakeCoin, start.transform.position + Vector3.up, Quaternion.identity);
        fakeCoin.OnInit(start, end);
    }

    private void OnTriggerEnter(Collider other)
    {
        isActive = true;
        if(hasTower) towerPopUp.PopUp(1);
        
    }
    private void OnTriggerExit(Collider other)
    {
        uptower.LoadCircle.fillAmount = 0;
        isActive = false;
        if(hasTower) towerPopUp.PopUp(0);
    }
}
