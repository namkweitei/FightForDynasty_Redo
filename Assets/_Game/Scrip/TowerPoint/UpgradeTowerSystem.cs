using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UpgradeTowerSystem : MonoBehaviour
{
    [SerializeField] private UpdateTower uptower;
    [SerializeField] private UpTowerSystem upTowerSystem;
    [SerializeField] private int currentCoin;
    [SerializeField] int targetCoin;
    [SerializeField] private float timeSpawn;
    [SerializeField] private float targetTime;
    [SerializeField] private TextMeshPro coin;
    private bool isActive;

    private void Update()
    {
        if (isActive)
        {
            LoadCircle();
        }
    }
    private void LoadCircle()
    {
        uptower.LoadTower();
        if (uptower.LoadCircle.fillAmount >= 1)
        {
            if (currentCoin > 0)
            {
                if (SaveLoadData.Ins.PlayerData.Coin > 0)
                {
                    timeSpawn -= Time.deltaTime;
                    if (timeSpawn <= 0)
                    {
                        SaveLoadData.Ins.PlayerData.Coin--;
                        currentCoin--;
                        coin.text = currentCoin.ToString();
                        SpawnCoin(Player.Ins.transform, upTowerSystem.transform);
                        timeSpawn = targetTime;
                    }
                }
            }
            else if (currentCoin <= 0)
            {

                upTowerSystem.SetTower();
                uptower.LoadCircle.fillAmount = 0;
                currentCoin = targetCoin + 20;
                targetCoin += 20;
                coin.text = currentCoin.ToString();
                if (!upTowerSystem.CheckTower())
                {
                    gameObject.SetActive(false);
                }
            }
        }



    }
    public void SpawnCoin(Transform start, Transform end)
    {
        FakeCoin fakeCoin = SmartPool.Ins.Spawn<FakeCoin>(PoolType.FakeCoin, start.transform.position + Vector3.up, Quaternion.identity);
        fakeCoin.OnInit(start, end);
    }
    private void OnTriggerEnter(Collider other)
    {
        isActive = true;
    }
    private void OnTriggerExit(Collider other)
    {
        uptower.LoadCircle.fillAmount = 0;
        isActive = false;
    }
}