using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerInfor : UICanvas
{

    [SerializeField] private PlayerPanel PlayerPanel;
    [SerializeField] private EquimentPanel equimentPanel;
    [SerializeField] private int currentcoin;
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] PlayerTv playerTv;
    private void Awake()
    {
    }
    void Start()
    {
        currentcoin = Random.Range(20, 30);
        coinText.text = currentcoin.ToString();
        PlayerPanel.SetInfor(SaveLoadData.Ins.PlayerData.Hp, SaveLoadData.Ins.PlayerData.RegenHp, SaveLoadData.Ins.PlayerData.Speed, SaveLoadData.Ins.PlayerData.CurrentExp, SaveLoadData.Ins.PlayerData.Exp, SaveLoadData.Ins.PlayerData.Level);
        PlayerTv.Ins.ChangeEquip((int)SaveLoadData.Ins.PlayerData.EquiType);
    }

    public void InforUp()
    {
        if (SaveLoadData.Ins.PlayerData.Coin >= currentcoin)
        {
            SaveLoadData.Ins.PlayerData.Coin -= currentcoin;
            currentcoin += Random.Range(10, 15);
            coinText.text = currentcoin.ToString();
            SaveLoadData.Ins.PlayerData.Hp += SaveLoadData.Ins.PlayerData.Hp / 100 * 15;
            SaveLoadData.Ins.PlayerData.Hp = Mathf.Ceil(SaveLoadData.Ins.PlayerData.Hp);
            SaveLoadData.Ins.PlayerData.RegenHp += 0.1f;
            SaveLoadData.Ins.PlayerData.Speed += SaveLoadData.Ins.PlayerData.Speed / 100 * 10f;
            SaveLoadData.Ins.PlayerData.Speed = Mathf.Ceil(SaveLoadData.Ins.PlayerData.Speed);
            PlayerPanel.SetInfor(SaveLoadData.Ins.PlayerData.Hp, SaveLoadData.Ins.PlayerData.RegenHp, SaveLoadData.Ins.PlayerData.Speed, SaveLoadData.Ins.PlayerData.CurrentExp, SaveLoadData.Ins.PlayerData.Exp, SaveLoadData.Ins.PlayerData.Level);
        }
    }
    public void ChangeButton(int type)
    {
        Player.Ins.ChangeEquiment((EquimentType)type);
        playerTv.ChangeEquip(type);
    }
    public void CloseButton()
    {
        UIManager.Ins.CloseUI<UIPlayerInfor>();
        if (GameManager.IsState(GameState.Pause))
        {
            GameManager.ChangeState(GameState.Playing);
        }
    }
}
