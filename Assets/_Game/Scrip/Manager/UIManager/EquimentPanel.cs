using System.Globalization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class EquimentPanel : MonoBehaviour
{
    [SerializeField] List<EquimentDetail> equimentDetails;

    void Start()
    {
        for (int i = 0; i < SaveLoadData.Ins.PlayerData.EquimentDatas.Count; i++)
        {
            for (int j = 0; j < equimentDetails.Count; j++)
            {
                if (SaveLoadData.Ins.PlayerData.EquimentDatas[i].equimentType == equimentDetails[j].EquimentType)
                {
                    equimentDetails[j].OnInit(SaveLoadData.Ins.PlayerData.EquimentDatas[i].damage, SaveLoadData.Ins.PlayerData.EquimentDatas[i].attackSpeed);
                    if (SaveLoadData.Ins.PlayerData.EquimentDatas[i].IsUnlock)
                    {
                        equimentDetails[j].Unlock();
                        equimentDetails[j].SetTextCoin(equimentDetails[j].CoinUpdate);
                    }
                }
                if (equimentDetails[j].EquimentType == SaveLoadData.Ins.PlayerData.EquiType)
                {
                    equimentDetails[j].OnImageChange();
                }
                else
                {
                    equimentDetails[j].OffImageChange();
                }
            }
        }
    }
    public void ChangeButton(EquimentDetail equimentDetail)
    {
        Player.Ins.ChangeEquiment(equimentDetail.EquimentType);
        PlayerTv.Ins.ChangeEquip((int)equimentDetail.EquimentType);
        for (int i = 0; i < equimentDetails.Count; i++)
        {
            equimentDetails[i].OffImageChange();
        }
        equimentDetail.OnImageChange();
    }
    public void UpgradeButton(EquimentDetail equimentDetail)
    {
        for (int i = 0; i < SaveLoadData.Ins.PlayerData.EquimentDatas.Count; i++)
        {
            if (SaveLoadData.Ins.PlayerData.EquimentDatas[i].equimentType == equimentDetail.EquimentType)
            {
                if (SaveLoadData.Ins.PlayerData.EquimentDatas[i].countUpdate < 1)
                {
                    if (SaveLoadData.Ins.PlayerData.Coin >= equimentDetail.CoinUpdate)
                    {
                        SaveLoadData.Ins.PlayerData.EquimentDatas[i].damage += (float)Math.Round((double)SaveLoadData.Ins.PlayerData.EquimentDatas[i].damage * UnityEngine.Random.Range(0.08f, 0.12f), 1);
                        SaveLoadData.Ins.PlayerData.EquimentDatas[i].attackSpeed += (float)Math.Round((double)SaveLoadData.Ins.PlayerData.EquimentDatas[i].attackSpeed * UnityEngine.Random.Range(0.01f, 0.03f), 3);
                        equimentDetail.OnInit(SaveLoadData.Ins.PlayerData.EquimentDatas[i].damage, SaveLoadData.Ins.PlayerData.EquimentDatas[i].attackSpeed);
                        SaveLoadData.Ins.PlayerData.EquimentDatas[i].countUpdate++;
                        equimentDetail.SetReward();
                        SaveLoadData.Ins.PlayerData.Coin -= equimentDetail.CoinUpdate;
                    }

                }
                else if (SaveLoadData.Ins.PlayerData.EquimentDatas[i].countUpdate < 30)
                {
                    //Reward

                    Time.timeScale = 0;
                    UnityEvent e = new UnityEvent();
                    e.AddListener(() =>
                    {
                        Debug.Log("reward loaded!");
                        SaveLoadData.Ins.PlayerData.EquimentDatas[i].damage += (float)Math.Round((double)SaveLoadData.Ins.PlayerData.EquimentDatas[i].damage * UnityEngine.Random.Range(0.08f, 0.12f), 1);
                        SaveLoadData.Ins.PlayerData.EquimentDatas[i].attackSpeed += (float)Math.Round((double)SaveLoadData.Ins.PlayerData.EquimentDatas[i].attackSpeed * UnityEngine.Random.Range(0.01f, 0.03f), 3);
                        equimentDetail.OnInit(SaveLoadData.Ins.PlayerData.EquimentDatas[i].damage, SaveLoadData.Ins.PlayerData.EquimentDatas[i].attackSpeed);
                        SaveLoadData.Ins.PlayerData.EquimentDatas[i].countUpdate++;
                        Time.timeScale = 1;
                    });
                    SkygoBridge.instance.ShowRewarded(e, null);
                }
            }


        }
    }

}
