using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIPlayerInfor : UICanvas
{

    [SerializeField] private PlayerPanel PlayerPanel;
    [SerializeField] private EquimentPanel equimentPanel;
    [SerializeField] private int currentcoin;
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] Transform pobUp;
    [SerializeField] Image background;

    void Start()
    {
        currentcoin = 100 * SaveLoadData.Ins.PlayerData.Level;
        coinText.text = currentcoin.ToString();
        PlayerPanel.SetInfor(SaveLoadData.Ins.PlayerData.Hp, SaveLoadData.Ins.PlayerData.RegenHp, SaveLoadData.Ins.PlayerData.Speed, SaveLoadData.Ins.PlayerData.CurrentExp, SaveLoadData.Ins.PlayerData.Exp, SaveLoadData.Ins.PlayerData.Level);
        PlayerTv.Ins.ChangeEquip((int)SaveLoadData.Ins.PlayerData.EquiType);
        PlayerTv.Ins.SetArmor(SaveLoadData.Ins.PlayerData.Level);
    }
    public void StartPobUp()
    {
        Time.timeScale = 0;
        background.DOFade(0.7f, 0.5f);
        pobUp.localScale = Vector3.zero;
        pobUp.DOScale(1, 0.5f).SetUpdate(true);

    }
    public void InforUp()
    {
        if (SaveLoadData.Ins.PlayerData.CountUpgrade < 1)
        {
            if (SaveLoadData.Ins.PlayerData.Coin >= currentcoin)
            {
                SaveLoadData.Ins.PlayerData.Coin -= currentcoin;
                currentcoin += 100 * SaveLoadData.Ins.PlayerData.Level;
                coinText.text = currentcoin.ToString();
                SaveLoadData.Ins.PlayerData.Hp += SaveLoadData.Ins.PlayerData.Hp / 100 * 15;
                SaveLoadData.Ins.PlayerData.Hp = Mathf.Ceil(SaveLoadData.Ins.PlayerData.Hp);
                SaveLoadData.Ins.PlayerData.RegenHp += 0.1f;
                SaveLoadData.Ins.PlayerData.Speed += SaveLoadData.Ins.PlayerData.Speed / 100 * 10f;
                SaveLoadData.Ins.PlayerData.Speed = Mathf.Ceil(SaveLoadData.Ins.PlayerData.Speed);
                SaveLoadData.Ins.PlayerData.CountUpgrade ++;
                PlayerPanel.CheckUpgradeCount();
                PlayerPanel.SetInfor(SaveLoadData.Ins.PlayerData.Hp, SaveLoadData.Ins.PlayerData.RegenHp, SaveLoadData.Ins.PlayerData.Speed, SaveLoadData.Ins.PlayerData.CurrentExp, SaveLoadData.Ins.PlayerData.Exp, SaveLoadData.Ins.PlayerData.Level);
            }
        }else if(SaveLoadData.Ins.PlayerData.CountUpgrade < 2){
                //Reward
                UnityEvent e = new UnityEvent();
                e.AddListener(() =>
                {
                    Debug.Log("reward loaded!");
                    SaveLoadData.Ins.PlayerData.Hp += SaveLoadData.Ins.PlayerData.Hp / 100 * 15;
                    SaveLoadData.Ins.PlayerData.Hp = Mathf.Ceil(SaveLoadData.Ins.PlayerData.Hp);
                    SaveLoadData.Ins.PlayerData.RegenHp += 0.1f;
                    SaveLoadData.Ins.PlayerData.Speed += SaveLoadData.Ins.PlayerData.Speed / 100 * 10f;
                    SaveLoadData.Ins.PlayerData.Speed = Mathf.Ceil(SaveLoadData.Ins.PlayerData.Speed);
                    SaveLoadData.Ins.PlayerData.CountUpgrade ++;
                    PlayerPanel.SetInfor(SaveLoadData.Ins.PlayerData.Hp, SaveLoadData.Ins.PlayerData.RegenHp, SaveLoadData.Ins.PlayerData.Speed, SaveLoadData.Ins.PlayerData.CurrentExp, SaveLoadData.Ins.PlayerData.Exp, SaveLoadData.Ins.PlayerData.Level);
                });
                SkygoBridge.instance.ShowRewarded(e,null);
                
        }
    }

    public void ChangeButton(int type)
    {
        Player.Ins.ChangeEquiment((EquimentType)type);
    }
    public void CloseButton()
    {
        AudioManager.Ins.PlaySfx(Constants.SFX_CLOSEUI);
        UIManager.Ins.CloseUI<UIPlayerInfor>();
        if (UIManager.Ins.GetUI<UIGamePlay>().isSpeedUp)
        {
            Time.timeScale = 2;
        }
        else
        {
            Time.timeScale = 1;
        }
        if (GameManager.IsState(GameState.Pause))
        {
            GameManager.ChangeState(GameState.Playing);
        }
    }
}
