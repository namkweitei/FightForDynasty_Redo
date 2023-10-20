using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;

public class MapManager : Singleton<MapManager>
{
    [SerializeField] private List<Map> mapList;
    private void Start()
    {
        OnInit();
    }
    public void OnInit()
    {
        for (int i = 0; i < mapList.Count; i++)
        {
            if (mapList[i].id == SaveLoadData.Ins.MapData.Level)
            {
                mapList[i].SetPosPlayer();
                mapList[i].SetPosCampCharacter();
                mapList[i].OnInit(MapState.Start);
            }
            else if (mapList[i].id == SaveLoadData.Ins.MapData.Level - 1)
            {
                mapList[i].enabled = false;
                mapList[i].OnInit(MapState.During);
            }
            else if (mapList[i].id <= SaveLoadData.Ins.MapData.Level - 2)
            {
                mapList[i].enabled = false;
                mapList[i].OnInit(MapState.End);
            }
            else
            {
                mapList[i].enabled = false;
            }
        }
    }
    [Button]
    public void NextMap()
    {
        mapList[SaveLoadData.Ins.MapData.Level].enabled = true;
        mapList[SaveLoadData.Ins.MapData.Level].OnInit(MapState.Start);
        mapList[SaveLoadData.Ins.MapData.Level].MoveCampCharacter();
        mapList[SaveLoadData.Ins.MapData.Level - 1].enabled = false;
        mapList[SaveLoadData.Ins.MapData.Level - 1].OnInit(MapState.During);
        mapList[SaveLoadData.Ins.MapData.Level - 2].enabled = false;
        mapList[SaveLoadData.Ins.MapData.Level - 2].OnInit(MapState.End);
    }
}
