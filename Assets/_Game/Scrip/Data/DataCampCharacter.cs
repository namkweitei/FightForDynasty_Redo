using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


[CreateAssetMenu(fileName = "DataCampCharacter", menuName = "Fight_For_Dynasty/DataCampCharacter", order = 0)]
public class DataCampCharacter : ScriptableObject
{
    [SerializeField] public List<CampCharacterData> campCharacterDatas;
    public void SetInforLevel(int Level)
    {
        for (int i = 0; i < campCharacterDatas.Count; i++)
        {
            if (Level == campCharacterDatas[i].Level)
            {
                SaveLoadData.Ins.CampCharacterData.MaxHp = campCharacterDatas[i].MaxHp;
                SaveLoadData.Ins.CampCharacterData.Coin = campCharacterDatas[i].Coin;
            }
        }
    }
    // [Button]
    // public void SetInfor(){
    //     for(int i = 0; i < campCharacterDatas.Count; i++){
    //         SetLevel(i);
    //         campCharacterDatas[i].Level = i;
    //     }
    // }
    // public void SetLevel(int i){
    //     switch(i){
    //         case 1:
    //             campCharacterDatas[i].MaxHp = 80;
    //             campCharacterDatas[i].Coin = 20;

    //             break;
    //         case 2:
    //             campCharacterDatas[i].MaxHp = 120;
    //             campCharacterDatas[i].Coin = 40;

    //             break;
    //         case 3:
    //             campCharacterDatas[i].MaxHp = 160;
    //             campCharacterDatas[i].Coin = 60;

    //             break;
    //         case 4:
    //             campCharacterDatas[i].MaxHp = 200;
    //             campCharacterDatas[i].Coin = 100;

    //             break;
    //         case 5:
    //             campCharacterDatas[i].MaxHp = 250;
    //             campCharacterDatas[i].Coin = 150;

    //             break;
    //         case 6:
    //             campCharacterDatas[i].MaxHp = 310;
    //             campCharacterDatas[i].Coin = 210;

    //             break;
    //         case 7:
    //             campCharacterDatas[i].MaxHp = 380;
    //             campCharacterDatas[i].Coin = 280;             

    //             break;
    //         case 8:
    //             campCharacterDatas[i].MaxHp = 460;
    //             campCharacterDatas[i].Coin = 360;    

    //             break;
    //         case 9:
    //             campCharacterDatas[i].MaxHp = 550;
    //             campCharacterDatas[i].Coin = 440;  

    //             break;
    //         case 10:
    //             campCharacterDatas[i].MaxHp = 650;
    //             campCharacterDatas[i].Coin = 530;  

    //             break;
    //         case 11:
    //             campCharacterDatas[i].MaxHp = 760;
    //             campCharacterDatas[i].Coin = 630;

    //             break;
    //         case 12:
    //             campCharacterDatas[i].MaxHp = 870;
    //             campCharacterDatas[i].Coin = 740;

    //             break;
    //         case 13:
    //             campCharacterDatas[i].MaxHp = 980;
    //             campCharacterDatas[i].Coin = 850;

    //             break;
    //         case 14:
    //             campCharacterDatas[i].MaxHp = 1090;
    //             campCharacterDatas[i].Coin = 900;

    //             break;
    //         case 15:
    //             campCharacterDatas[i].MaxHp = 1200;
    //             campCharacterDatas[i].Coin = 960;  

    //             break;
    //         case 16:
    //             campCharacterDatas[i].MaxHp = 1320;
    //             campCharacterDatas[i].Coin = 1070; 

    //             break;
    //         case 17:
    //             campCharacterDatas[i].MaxHp = 1450;
    //             campCharacterDatas[i].Coin = 1200;

    //             break;
    //     }
    // }
}
