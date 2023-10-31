using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private List<GameObject> armor;
    private void Start() {
        
        armor = Player.Ins.Armor;
    }
    public void LevelUp(int lv)
    {
        switch (lv)
        {
            case 1:
                break;
            case 2:
                armor[0].SetActive(true);
                SaveLoadData.Ins.PlayerData.Hp += SaveLoadData.Ins.PlayerData.Hp * 0.2f;
                Player.Ins.OnInit();
                break;
            case 3:
                SaveLoadData.Ins.PlayerData.Hp += SaveLoadData.Ins.PlayerData.Hp * 0.2f;
                Player.Ins.OnInit();
                break;
            case 4:
                SaveLoadData.Ins.PlayerData.Hp += SaveLoadData.Ins.PlayerData.Hp * 0.2f;
                Player.Ins.OnInit();
                break;
            case 5:
                for(int i = 0; i < SaveLoadData.Ins.PlayerData.EquimentDatas.Count; i++){
                    if(SaveLoadData.Ins.PlayerData.EquimentDatas[i].equimentType == EquimentType.Sword){
                        SaveLoadData.Ins.PlayerData.EquimentDatas[i].IsUnlock = true;
                    }
                }
                SaveLoadData.Ins.PlayerData.Hp += SaveLoadData.Ins.PlayerData.Hp * 0.2f;
                Player.Ins.OnInit();
                break;
            case 6:
                armor[1].SetActive(true);
                SaveLoadData.Ins.PlayerData.Hp += SaveLoadData.Ins.PlayerData.Hp * 0.45f;
                Player.Ins.OnInit();
                break;
            case 7:
                SaveLoadData.Ins.PlayerData.Hp += SaveLoadData.Ins.PlayerData.Hp * 0.45f;
                Player.Ins.OnInit();
                break;
            case 8:
                SaveLoadData.Ins.PlayerData.Hp += SaveLoadData.Ins.PlayerData.Hp * 0.45f;
                Player.Ins.OnInit();
                break;
            case 9:
                SaveLoadData.Ins.PlayerData.Hp += SaveLoadData.Ins.PlayerData.Hp * 0.45f;
                Player.Ins.OnInit();
                break;
            case 10:
            for(int i = 0; i < SaveLoadData.Ins.PlayerData.EquimentDatas.Count; i++){
                    if(SaveLoadData.Ins.PlayerData.EquimentDatas[i].equimentType == EquimentType.Crossbow){
                        SaveLoadData.Ins.PlayerData.EquimentDatas[i].IsUnlock = true;
                    }
                }
                SaveLoadData.Ins.PlayerData.Hp += SaveLoadData.Ins.PlayerData.Hp * 0.45f;
                Player.Ins.OnInit();
                break;
            case 11:
                armor[2].SetActive(true);
                SaveLoadData.Ins.PlayerData.Speed += SaveLoadData.Ins.PlayerData.Speed * 0.12f;
                Player.Ins.OnInit();
                break;
            case 12:
                SaveLoadData.Ins.PlayerData.Speed += SaveLoadData.Ins.PlayerData.Speed * 0.12f;
                Player.Ins.OnInit();
                break;
            case 13:
                SaveLoadData.Ins.PlayerData.Speed += SaveLoadData.Ins.PlayerData.Speed * 0.12f;
                Player.Ins.OnInit();
                break;
            case 14:
                SaveLoadData.Ins.PlayerData.Speed += SaveLoadData.Ins.PlayerData.Speed * 0.12f;
                Player.Ins.OnInit();
                break;
            case 15:
            for(int i = 0; i < SaveLoadData.Ins.PlayerData.EquimentDatas.Count; i++){
                    if(SaveLoadData.Ins.PlayerData.EquimentDatas[i].equimentType == EquimentType.Spear){
                        SaveLoadData.Ins.PlayerData.EquimentDatas[i].IsUnlock = true;
                    }
                }
                SaveLoadData.Ins.PlayerData.Speed += SaveLoadData.Ins.PlayerData.Speed * 0.12f;
                Player.Ins.OnInit();
                break;
            case 16:
                armor[3].SetActive(true);
                SaveLoadData.Ins.PlayerData.RegenHp += 0.1f;
                Player.Ins.OnInit();
                break;
            case 17:
                SaveLoadData.Ins.PlayerData.RegenHp += 0.1f;
                Player.Ins.OnInit();
                break;
            case 18:
                SaveLoadData.Ins.PlayerData.RegenHp += 0.1f;
                Player.Ins.OnInit();
                break;
            case 19:
                SaveLoadData.Ins.PlayerData.RegenHp += 0.1f;
                Player.Ins.OnInit();
                break;
            case 20:
                SaveLoadData.Ins.PlayerData.RegenHp += 0.1f;
                Player.Ins.OnInit();
                break;
            case 21:
                armor[4].SetActive(true);
                SaveLoadData.Ins.PlayerData.RegenHp += 0.1f;
                Player.Ins.OnInit();
                break;
            case 22:
                SaveLoadData.Ins.PlayerData.RegenHp += 0.1f;
                Player.Ins.OnInit();
                break;
            case 23:
                SaveLoadData.Ins.PlayerData.RegenHp += 0.1f;
                Player.Ins.OnInit();
                break;
            case 24:
                SaveLoadData.Ins.PlayerData.RegenHp += 0.1f;
                Player.Ins.OnInit();
                break;
            case 25:
                SaveLoadData.Ins.PlayerData.RegenHp += 0.1f;
                Player.Ins.OnInit();
                break;
            default:
                break;
        }

    }
}
