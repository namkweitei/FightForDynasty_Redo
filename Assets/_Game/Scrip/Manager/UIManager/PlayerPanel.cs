using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPanel : MonoBehaviour
{
    [SerializeField] private Image imageFill;
    [SerializeField] private TextMeshProUGUI expText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI speedText;
    [SerializeField] private TextMeshProUGUI recoveryText;
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI upSpeedText;
    [SerializeField] private TextMeshProUGUI upRecoveryText;
    [SerializeField] private TextMeshProUGUI upHpText;
    public void SetInfor(float hp, float recovery, float speed, float currentExp, float exp, int level)
    {
        hpText.text = hp.ToString();
        recoveryText.text = String.Format("{0:0.##}", recovery); ;
        speedText.text = speed.ToString();
        upHpText.text = "+ " + Mathf.Ceil(hp / 100 * 15).ToString();
        upRecoveryText.text = "+ 0.1";
        upSpeedText.text = "+ " + Mathf.Ceil(speed / 100 * 10).ToString();
        imageFill.fillAmount = currentExp / exp;
        expText.text = currentExp.ToString() + "/" + exp.ToString();
        levelText.text = level.ToString();
    }
    public void SetLevelBar()
    {

    }
}
