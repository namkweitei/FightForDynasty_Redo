using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DailyRewardDisplay : MonoBehaviour
{
    [SerializeField] private int dailyRewardID;
    [SerializeField] TextMeshProUGUI rewardValue;
    [SerializeField] TextMeshProUGUI timeRemain;
    [SerializeField] GameObject timeRemainPanel;
    [SerializeField] Button claim;
   
    private void Start() {
        claim.onClick.AddListener(() =>
        {
            DailyRewardManager.Ins.ClaimReward(dailyRewardID);
        });
    }

    public void ShowToUI(DailyReward dailyReward){
        if(!dailyReward.IsClaim){
            ShowValue(dailyReward.RewardValue);
        }else{
            ShowTimeremain(dailyReward.TimeRemaining, dailyReward.RewardValue);
        }
    }

    private void ShowValue(int value)
    {
        if (!timeRemainPanel.activeSelf)
        {
            rewardValue.text = "+" + value.ToString();
        }
        else
        {
            timeRemainPanel.SetActive(false);
        }

    }
    private void ShowTimeremain(float time, int value)
    {
        if (timeRemainPanel.activeSelf)
        {
            rewardValue.text = "+" + value.ToString();
            int hours = (int)(time / 3600);
            int minutes = (int)((time % 3600) / 60);
            int seconds = (int)(time % 60);
            timeRemain.text = $"{hours}h {minutes}m {seconds}s";

        }
        else
        {
            timeRemainPanel.SetActive(true);
        }
    }
}
