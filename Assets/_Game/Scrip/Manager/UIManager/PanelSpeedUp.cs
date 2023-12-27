using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PanelSpeedUp : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textCoundADS;
    [SerializeField] int countADS;
    private void Awake()
    {
        countADS = PlayerPrefs.GetInt("countADS");
        textCoundADS.text = countADS.ToString() + "/3";
        if(countADS >= 3)
        {
            UIManager.Ins.GetUI<UIGamePlay>().isSpeedX2 = true;
            this.gameObject.SetActive(false);
        }
    }
    public void WatchADSButton()
    {

        //RewardADSSpeed
        countADS++;
        PlayerPrefs.SetInt("countADS", countADS);
        textCoundADS.text = countADS.ToString() + "/3";
        if(countADS == 3)
        {
            UIManager.Ins.GetUI<UIGamePlay>().isSpeedX2 = true;
            this.gameObject.SetActive(false);
        }
    }
    public void CloseButton()
    {
        this.gameObject.SetActive(false);
    }
}
