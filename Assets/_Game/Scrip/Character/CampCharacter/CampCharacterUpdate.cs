using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CampCharacterUpdate : MonoBehaviour
{
    private bool isActive;
    [SerializeField] private int currentCoin;
    private float timeSpawn;
    private float targetTime;
    [SerializeField] Transform endPos;
    [SerializeField] private TextMeshPro coin;
    [SerializeField] private Image loadCircle;
    [SerializeField] private float loadTime = 5f;


    public Image LoadCircle { get => loadCircle; set => loadCircle = value; }

    // Start is called before the first frame update
    void Start()
    {
        isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            UpLevel();
        }

    }
    private void UpLevel()
    {
        LoadTower();
        if (LoadCircle.fillAmount >= 1)
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
                        SpawnCoin(Player.Ins.transform, endPos);
                        timeSpawn = targetTime;
                    }
                }
            }
            else if (currentCoin <= 0)
            {
                CampCharacter.Ins.SetLevel();
                LoadCircle.fillAmount = 0;
                currentCoin = SaveLoadData.Ins.CampCharacterData.Coin;
                coin.text = currentCoin.ToString();
            }
        }

    }
    public void SpawnCoin(Transform start, Transform end)
    {
        FakeCoin fakeCoin = SmartPool.Ins.Spawn<FakeCoin>(PoolType.FakeCoin, start.transform.position + Vector3.up, Quaternion.identity);
        fakeCoin.OnInit(start, end);
    }


    public void OnInit(int currentCoin)
    {
        loadCircle.fillAmount = 0;
        this.coin.text = currentCoin.ToString();

    }
    public void SetCurrentCoin(int currentCoin)
    {
        coin.text = currentCoin.ToString();
    }


    public void LoadTower()
    {
        loadCircle.fillAmount += 1 / loadTime * Time.deltaTime * 5;
    }
    private void OnTriggerEnter(Collider other)
    {
        isActive = true;
        CampCharacter.Ins.isUpdate = true;
    }
    private void OnTriggerExit(Collider other)
    {
        LoadCircle.fillAmount = 0;
        isActive = false;
        CampCharacter.Ins.isUpdate = false;

    }
}
