using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;

public class UIGamePlay : UICanvas, IObserver
{

    [SerializeField] private LevelUI levelUI;
    [SerializeField] private CoinPanel coinPanel;
    [SerializeField] private WavePanel wavePanel;
    [SerializeField] private GameObject unlockIcon;
    public bool isSpeedUp;
    void Start()
    {
        SaveLoadData.Ins.PlayerData.RegisterObserver(this);
        OnNotifyAddCurrency();
        AudioManager.Ins.PlayMusic(Constants.MUSIC_THEME);
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
        if (isSpeedUp)
        {
            isSpeedUp = false;
            Time.timeScale = 1;
        }
        else
        {
            isSpeedUp = true;
            Time.timeScale = 2;
        }
    }

    public void OnNotifyAddCurrency()
    {
        float currentcoin = 100 * SaveLoadData.Ins.PlayerData.Level;
        if (SaveLoadData.Ins.PlayerData.Coin >= currentcoin)
        {
            unlockIcon.SetActive(true);
        }
        else
        {
            unlockIcon.SetActive(false);
        }
    }
}
