using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour,IObserver
{
    [SerializeField] private Image imageFill;
    [SerializeField] private TextMeshProUGUI expText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private float exp;
    [SerializeField] private float currentExp;
    [SerializeField] private float level;
    private void Start() {
        SaveLoadData.Ins.PlayerData.RegisterObserver(this);
        OnInit();
    }
    public void OnInit()
    {
        exp = SaveLoadData.Ins.PlayerData.Exp;
        currentExp = SaveLoadData.Ins.PlayerData.CurrentExp;
        level = SaveLoadData.Ins.PlayerData.Level;
        imageFill.fillAmount = currentExp / exp;
        expText.text = currentExp.ToString() + "/" + exp.ToString();
        levelText.text = level.ToString();
    }

    public void OnNotifyAddCurrency()
    {
        OnInit();
    }

}
