using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
public interface IObserver
{
    void OnNotifyAddCurrency();
    
}
public class SaveLoadData : Singleton<SaveLoadData>
{
    [Header("---------TowerData----------")]
    public Dictionary<int, int> TowerData;
    [Header("---------PlayerData---------")]
    [SerializeField] private PlayerData playerData;
    [Header("---------CampCharacterData--")]
    [SerializeField] private CampCharacterData campCharacterData;
    [Header("---------MapData------------")]
    [SerializeField] private MapData mapData;
    [Header("---------DailyRewardData----")]
    [SerializeField] private DailyReward coinReward;
    [SerializeField] private DailyReward buckReward;
    public PlayerData PlayerData { get => playerData; }
    public CampCharacterData CampCharacterData { get => campCharacterData; }
    public MapData MapData { get => mapData; }
    public DailyReward CoinReward { get => coinReward; set => coinReward = value; }
    public DailyReward BuckReward { get => buckReward; set => buckReward = value; }
    public bool IsGetSpecialDeal;

    protected override void Awake()
    {
        base.Awake();
        OnInit();
    }
    [Button]
    public void SetId()
    {

        for (int i = 0; i < 100; i++)
        {
            TowerData.Add(i, 0);
        }
    }
    public void OnInit()
    {
        if (!ES3.KeyExists(Constants.DATA_PLAYERDATA, Constants.DATA_PLAYERDATA))
        {
            SavePlayerInfor();
        }
        else
        {
            playerData = LoadPlayerInFor();
        }
        if (!ES3.KeyExists(Constants.DATA_MAPDATA, Constants.DATA_MAPDATA))
        {
            SaveMapData();
        }
        else
        {
            campCharacterData = LoadCampCharacterInFor();
        }
        if (!ES3.KeyExists(Constants.DATA_CAMPCHARACTERDATA, Constants.DATA_CAMPCHARACTERDATA))
        {
            SaveCampCharacterInfor();
        }
        else
        {
            mapData = LoadMapData();
        }
        if (!ES3.KeyExists(Constants.DATA_TOWERDATA, Constants.DATA_TOWERDATA))
        {
            SaveTowerData();
        }
        else
        {
            TowerData = LoadTowerData();
        }
        if (!ES3.KeyExists(Constants.DATA_DAILYREWARDBUCKDATA, Constants.DATA_CAMPCHARACTERDATA))
        {
            SaveDailyRewardBuckData();
        }
        else
        {
            BuckReward = LoadDailyRewardBuckData();
        }
        if (!ES3.KeyExists(Constants.DATA_DAILYREWARDCOINDATA, Constants.DATA_CAMPCHARACTERDATA))
        {
            SaveDailyRewardCoinData();
        }
        else
        {
            CoinReward = LoadDailyRewardCoinData();
        }
        if (!ES3.KeyExists(Constants.DATA_DAILYREWARDCOINDATA, "IsGetSpecialDeal"))
        {
            SaveGetSpecialDeal();
        }
        else
        {
            IsGetSpecialDeal = LoadGetSpecialDeal();
        }


    }
    public void SavePlayerInfor()
    {
        ES3.Save(Constants.DATA_PLAYERDATA, playerData, Constants.DATA_PLAYERDATA);
    }
    public PlayerData LoadPlayerInFor()
    {
        return ES3.Load<PlayerData>(Constants.DATA_PLAYERDATA, Constants.DATA_PLAYERDATA);
    }
    public void SaveCampCharacterInfor()
    {
        ES3.Save(Constants.DATA_CAMPCHARACTERDATA, campCharacterData, Constants.DATA_CAMPCHARACTERDATA);
    }
    public CampCharacterData LoadCampCharacterInFor()
    {
        return ES3.Load<CampCharacterData>(Constants.DATA_CAMPCHARACTERDATA, Constants.DATA_CAMPCHARACTERDATA);
    }
    public void SaveMapData()
    {
        ES3.Save(Constants.DATA_MAPDATA, mapData, Constants.DATA_MAPDATA);
    }
    public MapData LoadMapData()
    {
        return ES3.Load<MapData>(Constants.DATA_MAPDATA, Constants.DATA_MAPDATA);
    }
    public void SaveTowerData()
    {
        ES3.Save(Constants.DATA_TOWERDATA, TowerData, Constants.DATA_TOWERDATA);
    }
    public Dictionary<int, int> LoadTowerData()
    {
        return ES3.Load<Dictionary<int, int>>(Constants.DATA_TOWERDATA, Constants.DATA_TOWERDATA);
    }
    public void SaveDailyRewardCoinData()
    {
        ES3.Save(Constants.DATA_DAILYREWARDCOINDATA, CoinReward, Constants.DATA_DAILYREWARDDATA);
    }
    public DailyReward LoadDailyRewardBuckData()
    {
        return ES3.Load<DailyReward>(Constants.DATA_DAILYREWARDBUCKDATA, Constants.DATA_DAILYREWARDDATA);
    }
    public void SaveDailyRewardBuckData()
    {
        ES3.Save(Constants.DATA_DAILYREWARDBUCKDATA, BuckReward, Constants.DATA_DAILYREWARDDATA);
    }
    public DailyReward LoadDailyRewardCoinData()
    {
        return ES3.Load<DailyReward>(Constants.DATA_DAILYREWARDCOINDATA, Constants.DATA_DAILYREWARDDATA);
    }
     public void SaveGetSpecialDeal()
    {
        ES3.Save(Constants.DATA_DAILYREWARDBUCKDATA, IsGetSpecialDeal, "IsGetSpecialDeal");
    }
    public bool LoadGetSpecialDeal()
    {
        return ES3.Load<bool>(Constants.DATA_DAILYREWARDCOINDATA, "IsGetSpecialDeal");
    }
    private void OnApplicationQuit()
    {
        SaveCampCharacterInfor();
        SaveMapData();
        SavePlayerInfor();
        SaveTowerData();
        SaveDailyRewardBuckData();
        SaveDailyRewardCoinData();
    }



}
[Serializable]
public class PlayerData
{
    [SerializeField] private float hp;
    [SerializeField] private float speed;
    [SerializeField] private float regenHp;
    [SerializeField] private float exp;
    [SerializeField] private float currentExp;
    [SerializeField] private int level;
    [SerializeField] private int coin;
    [SerializeField] private int buck;
    [SerializeField] private EquimentType equiType;
    [SerializeField] private List<Equiment> equimentDatas;
    #region Get/Set Hell
    public float Hp
    {
        get => hp;
        set
        {
            hp = value;
        }
    }
    public float Speed
    {
        get => speed;
        set
        {
            speed = value;
        }
    }
    public float RegenHp
    {
        get => regenHp;
        set
        {
            regenHp = value;
        }
    }
    public float Exp
    {
        get => exp;
        set
        {
            exp = value;
        }
    }
    public float CurrentExp
    {
        get => currentExp;
        set
        {
            currentExp = value;
            if(currentExp >= exp){
                Level ++;
                CurrentExp = 0;
                Exp += Exp * 0.5f;
                Hp += Hp / 100 * 15;
                Hp = Mathf.Ceil(Hp);
                RegenHp += 0.1f;
                Speed += Speed / 100 * 10f;
                Speed = Mathf.Ceil(Speed);
                LevelManager.Ins.LevelUp(level);
            }
            NotifyObserversAddCurrency();
        }
    }
    public int Level
    {
        get => level;
        set
        {
            level = value;
            NotifyObserversAddCurrency();
        }
    }
    public int Coin
    {
        get
        {
            return coin;
        }
        set
        {
            coin = value;
            NotifyObserversAddCurrency();
        }
    }
    public int Buck
    {
        get
        {
            return buck;
        }
        set
        {
            buck = value;
            NotifyObserversAddCurrency();
        }
    }
    public EquimentType EquiType
    {
        get => equiType;
        set
        {
            equiType = value;
        }
    }
    public List<Equiment> EquimentDatas
    {
        get => equimentDatas;
        set
        {
            equimentDatas = value;
        }
    }
    #endregion
    [Header("IObserver")]
    private List<IObserver> observers = new List<IObserver>();

    public void RegisterObserver(IObserver observer)
    {
        observers.Add(observer);
    }

    public void RemoveObserver(IObserver observer)
    {
        observers.Remove(observer);
    }

    public void NotifyObserversAddCurrency()
    {
        foreach (var observer in observers)
        {
            observer.OnNotifyAddCurrency();
        }
    }
}
[Serializable]
public class CampCharacterData
{
    [SerializeField] private float maxHp;
    [SerializeField] private float hp;
    [SerializeField] private int level;
    [SerializeField] private int coin;
    public float MaxHp
    {
        get => maxHp;
        set
        {
            maxHp = value;
        }
    }
    public float Hp
    {
        get => hp;
        set
        {
            hp = value;
        }
    }
    public int Level
    {
        get => level;
        set
        {
            level = value;
        }
    }
    public int Coin
    {
        get => coin;
        set
        {
            coin = value;
        }
    }
}

[Serializable]
public class MapData
{
    [SerializeField] private int level;
    [SerializeField] private int wave;
    public int Level
    {
        get => level;
        set
        {
            level = value;
        }
    }
    public int Wave
    {
        get => wave;
        set
        {
            wave = value;
        }
    }

}
