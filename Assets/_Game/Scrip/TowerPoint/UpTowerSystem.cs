using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
public class UpTowerSystem : MonoBehaviour
{
    [SerializeField] private int id;
    [SerializeField] public int Level;
    [SerializeField] private List<Tower> towerPrefabs;
    [SerializeField] private Tower towerCurrent;
    [SerializeField] private Transform towerHolder;
    [SerializeField] private ParticleSystem buildEffect;
    [SerializeField] private UpdateTower uptower;
    [SerializeField] private Transform circle;
    [SerializeField] private bool hasTower;
    [SerializeField] private int currentCoin;
    [SerializeField] private GameObject upgrateTower;
    [SerializeField] private float timeSpawn;
    [SerializeField] private float targetTime;
    [SerializeField] private TowerPopUp towerPopUp;
    [SerializeField] private int targetcoin = 50;
    public int Id { get => id; set => id = value; }
    private bool isActive;
    public void OnInit()
    {
        Level = SaveLoadData.Ins.TowerData[Id];
        if (Level > 0)
        {
            hasTower = true;
             if(towerCurrent != null){
                towerCurrent.gameObject.SetActive(false);
            }
            towerCurrent = Instantiate( towerPrefabs[Level - 1], towerHolder.position, Quaternion.identity);
            towerCurrent.transform.SetParent(towerHolder);
            towerCurrent.transform.localScale = new Vector3(0,0,0);
            towerCurrent.transform.DOScale(1, 1f);
            towerPopUp.OnInit(towerCurrent.Damage, towerCurrent.ShootSpeed, Level + 1);
            upgrateTower.SetActive(false);
            circle.gameObject.SetActive(false);
            buildEffect.Stop();
        }
        else
        {
            gameObject.SetActive(false);
            buildEffect.Stop();
            upgrateTower.SetActive(false);
        }
    }
    public void SetCurrentTower()
    {
        Level = SaveLoadData.Ins.TowerData[Id];
        if (Level > 0)
        {
            hasTower = true;
            if(towerCurrent != null){
                Destroy(towerCurrent.gameObject);
            }
            towerCurrent = Instantiate( towerPrefabs[Level - 1], towerHolder.position, Quaternion.identity);
            towerCurrent.transform.SetParent(towerHolder);
            towerCurrent.transform.localScale = new Vector3(0,0,0);
            towerCurrent.transform.DOScale(1, 1f);
            towerPopUp.OnInit(towerCurrent.Damage, towerCurrent.ShootSpeed, Level + 1 );
            if (Level >= towerPrefabs.Count)
            {
                upgrateTower.SetActive(false);
            }
            else
            {
                upgrateTower.SetActive(true);
            }
            circle.gameObject.SetActive(false);
            buildEffect.Stop();
        }
        else
        {
            hasTower = false;
            
            if(towerCurrent != null)
            {
                Destroy(towerCurrent.gameObject);
                circle.gameObject.SetActive(true);
                currentCoin = targetcoin;
            }
            uptower.OnInit(currentCoin);
            upgrateTower.SetActive(false);
            Level = 0;
            buildEffect.Stop();
        }
    }
    private void Update()
    {
        if (isActive && !Player.Ins.GetMove())
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
                    upgrateTower.SetActive(true);
                    SetTower();
                    uptower.LoadCircle.fillAmount = 0;
                }
            }
        }
    }
    public void SetTower()
    {
        Level = SaveLoadData.Ins.TowerData[Id];
        if (Level < towerPrefabs.Count)
        {
            buildEffect.Play();
            if(towerCurrent != null){
                Destroy(towerCurrent.gameObject);

            }
            towerCurrent = Instantiate( towerPrefabs[Level],towerHolder.position, Quaternion.identity);
            towerCurrent.transform.SetParent(towerHolder);
            towerCurrent.transform.localScale = new Vector3(0,0,0);
            towerCurrent.transform.DOScale(1, 1f);
            towerPopUp.OnInit(towerCurrent.Damage, towerCurrent.ShootSpeed, Level + 1 );
            SaveLoadData.Ins.TowerData[Id]++;
            Debug.Log(SaveLoadData.Ins.TowerData[Id]);
            if (Level >= towerPrefabs.Count)
            {
                upgrateTower.SetActive(false);
            }
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
