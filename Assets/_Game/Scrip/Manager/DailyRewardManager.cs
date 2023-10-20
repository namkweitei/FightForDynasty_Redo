using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[System.Serializable]
public class DailyReward
{
    [SerializeField]
    private int rewardID;
    [SerializeField]
    private int rewardValue;
    [SerializeField]
    private int rewardStep;
    [SerializeField]
    private bool isClaim;
    [Header("Time count")]
    [SerializeField]
    private float timeRemaining;
    [SerializeField]
    private double lastRewardTime;
    public int RewardID { get => rewardID; }
    public int RewardValue { get => rewardValue; }
    public bool IsClaim { get => isClaim; }
    public float TimeRemaining { get => timeRemaining; set => timeRemaining = value; }
    public double LastRewardTime { get => lastRewardTime; }


    public void DailyRewardInit()
    {
        isClaim = false;
        timeRemaining = 0;
        lastRewardTime = 0;
    }

    public void ClaimReward()
    {
        if (isClaim == false)
        {
            Debug.Log("Give reward to player: " + rewardValue);
            lastRewardTime = DailyRewardManager.Ins.GetCurrentTimestamp();
            isClaim = true;
            rewardValue += rewardStep;
        }
    }
}

public class DailyRewardManager : Singleton<DailyRewardManager>
{
    public float TimeToNextReward = 84600f;
    private void Start()
    {
        if (ShouldAssignDailyReward(SaveLoadData.Ins.CoinReward))
        {
            AssignDailyReward(SaveLoadData.Ins.CoinReward);
        }
        if (ShouldAssignDailyReward(SaveLoadData.Ins.BuckReward))
        {
            AssignDailyReward(SaveLoadData.Ins.BuckReward);
        }

    }
    private void Update()
    {
        if (!UIManager.Ins.IsOpened<UIShop>()) return;

        UpdateDailyRewardUIIfOpen(SaveLoadData.Ins.CoinReward);
        UpdateDailyRewardUIIfOpen(SaveLoadData.Ins.BuckReward);
    }
    private bool ShouldAssignDailyReward(DailyReward DailyReward)
    {
        return TimePassedSinceLastReward(DailyReward.LastRewardTime) >= TimeToNextReward && DailyReward.IsClaim;
    }

    public double TimePassedSinceLastReward(double lastRewardTime)
    {
        return GetCurrentTimestamp() - lastRewardTime;
    }

    public double GetCurrentTimestamp()
    {
        return (System.DateTime.UtcNow - new System.DateTime(1970, 1, 1)).TotalSeconds;
    }

    private void UpdateDailyRewardUIIfOpen(DailyReward dailyReward)
    {
        if (dailyReward.IsClaim)
        {
            dailyReward.TimeRemaining = TimeToNextReward - (float)TimePassedSinceLastReward(dailyReward.LastRewardTime);
            ShowToUI(dailyReward);
            if (dailyReward.TimeRemaining <= 0)
            {
                AssignDailyReward(dailyReward);
            }
        }
    }

    private void AssignDailyReward(DailyReward currentDailyReward)
    {
        currentDailyReward.DailyRewardInit();
        if (UIManager.Ins.IsOpened<UIShop>())
        {
            ShowToUI(currentDailyReward);
        }

        Debug.Log("Assign new daily reward");
    }

    public void ShowToUI(DailyReward currentDailyReward){
        switch (currentDailyReward.RewardID){
        case 0:
            UIManager.Ins.GetUI<UIShop>().DailyRewardCoinDisplay.ShowToUI(currentDailyReward);
            break;
        case 1:
            UIManager.Ins.GetUI<UIShop>().DailyRewardBuckDisplay.ShowToUI(currentDailyReward);
            break;
        default:
            break;
        }
        
    }

    public void ClaimReward(int ID)
    {
        if (SaveLoadData.Ins.CoinReward.RewardID == ID)
        {
            SaveLoadData.Ins.CoinReward.ClaimReward();
        }
        else if (SaveLoadData.Ins.BuckReward.RewardID == ID)
        {
            SaveLoadData.Ins.BuckReward.ClaimReward();
        }
    }

}