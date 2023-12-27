using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using static InfinityCode.UltimateEditorEnhancer.SceneTools.DropToFloor;

public class UIGamePlay : UICanvas, IObserver
{

    [SerializeField] private LevelUI levelUI;
    [SerializeField] private CoinPanel coinPanel;
    [SerializeField] private WavePanel wavePanel;
    [SerializeField] private GameObject unlockIcon;
    [SerializeField] private TextMeshProUGUI speedText;
    [SerializeField] private GameObject panelSpeedup;
    public bool isSpeedUp;
    public bool isSpeedX2;
    public int countSpeed;
    public float timeSpeed;

    void Start()
    {
        SaveLoadData.Ins.PlayerData.RegisterObserver(this);
        OnNotifyAddCurrency();
        speedText.text = "100%";
        AudioManager.Ins.PlayMusic(Constants.MUSIC_THEME);
        int countADS = PlayerPrefs.GetInt("countADS");
        if(countADS >= 3)
        {
            isSpeedX2 = true;
        }
    }
    private void Update()
    {
        CheckTimeSpeed();
    }
    [Button]
    public void OpenWaveStage(int count, int curWave,int wave)
    {
        wavePanel.transform.DOScale(1, 0.5f).OnComplete(() =>
        {
            SetTextWave(wave);
        });
        wavePanel.SetWaveStage(count);
        wavePanel.SetFill(curWave);
    }
    [Button]
    public void CloseWaveStage()
    {
        wavePanel.transform.DOScale(0, 0.5f).OnComplete(() =>
        {
        });
    }
    public void SetFill(int curWave)
    {
        wavePanel.SetFill(curWave);
    }
    public void Fill(float fil)
    {
        wavePanel.Fill(fil);
    }
     public void SetTextWave(int wave){
        wavePanel.SetTextWave(wave);
        wavePanel.Show();
    }
    public void InforButton()
    {
        AudioManager.Ins.PlaySfx(Constants.SFX_OPENUI_1);
        UIManager.Ins.OpenUI<UIPlayerInfor>();
        UIManager.Ins.GetUI<UIPlayerInfor>().StartPobUp();
        if (GameManager.IsState(GameState.Playing))
        {
            GameManager.ChangeState(GameState.Pause);
        }
    }
    public void SettingButton()
    {
        AudioManager.Ins.PlaySfx(Constants.SFX_OPENUI_2);
        UIManager.Ins.OpenUI<UISetting>();
        UIManager.Ins.GetUI<UISetting>().StartPobUp();
        if (GameManager.IsState(GameState.Playing))
        {
            GameManager.ChangeState(GameState.Pause);
        }
    }
    public void ShopButton()
    {
        AudioManager.Ins.PlaySfx(Constants.SFX_OPENUI_1);
        UIManager.Ins.OpenUI<UIShop>();
        UIManager.Ins.GetUI<UIShop>().StartPobUp();
        if (GameManager.IsState(GameState.Playing))
        {
            GameManager.ChangeState(GameState.Pause);
        }
        DailyRewardManager.Ins.ShowToUI(SaveLoadData.Ins.BuckReward);
        DailyRewardManager.Ins.ShowToUI(SaveLoadData.Ins.CoinReward);
    }
    public void SpecialDealButton()
    {
        UIManager.Ins.OpenUI<UISpecialDeal>();
        if (GameManager.IsState(GameState.Playing))
        {
            GameManager.ChangeState(GameState.Pause);
        }
    }
    public void TimeSkipButton()
    {
        if (countSpeed == 0)
        {
            timeSpeed = 180f;
            isSpeedUp = true;
            countSpeed++;
            speedText.text = "125%";
            Time.timeScale = 1.25f;
        }
        else if(countSpeed == 1)
        {
            countSpeed++;
            speedText.text = "150%";
            Time.timeScale = 1.5f;
        }
        else if(countSpeed == 2) 
        {
            if (isSpeedX2)
            {
                countSpeed++;
                speedText.text = "200%";
                Time.timeScale = 2f;
            }
            else
            {
                isSpeedUp = false;
                countSpeed = 0;
                speedText.text = "100%";
                Time.timeScale = 1;
            }
            
        }else if(countSpeed == 3)
        {
            isSpeedUp = false;
            countSpeed = 0;
            speedText.text = "100%";
            Time.timeScale = 1;
        }
    }
    public void CheckTimeSpeed()
    {
        if (isSpeedUp && !isSpeedX2)
        {
            timeSpeed -= Time.deltaTime;
            if(timeSpeed < 0)
            {
                panelSpeedup.SetActive(true);
                timeSpeed = 180;
            }
            
        }
    }
    public void OnNotifyAddCurrency()
    {
        if (CheckUpgradeInfor())
        {
            unlockIcon.SetActive(true);
        }else if(CheckUpgradeEqui()) 
        {
            unlockIcon.SetActive(true);
        }
        else
        {
            unlockIcon.SetActive(false);
        }
    }

    public bool CheckUpgradeInfor()
    {
        float currentcoin = 100 * SaveLoadData.Ins.PlayerData.Level;
        if (SaveLoadData.Ins.PlayerData.Coin >= currentcoin && SaveLoadData.Ins.PlayerData.CountUpgrade < 1)
        {
            return true;
        }
        return false;
    }
    public bool CheckUpgradeEqui()
    {
        for (int i = 0; i < SaveLoadData.Ins.PlayerData.EquimentDatas.Count; i++)
        {
            if (SaveLoadData.Ins.PlayerData.EquimentDatas[i].countUpdate < 1 && SaveLoadData.Ins.PlayerData.EquimentDatas[i].IsUnlock)
            {
                if(SaveLoadData.Ins.PlayerData.Coin > UIManager.Ins.GetUI<UIPlayerInfor>().EquimentPanel.EquimentDetails[i].CoinUpdate)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
