using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine.AI;

public class MapManager : Singleton<MapManager>
{
    [SerializeField] private List<Map> mapListPrefabs;
    [SerializeField] private List<Map> mapListsRuntime;
    private void Start()
    {
        OnInit();
    }
    public void OnInit()
    {
        if (SaveLoadData.Ins.MapData.Level <= mapListPrefabs.Count - 6)
        {
            for (int i = 0; i < SaveLoadData.Ins.MapData.Level + 6; i++)
            {
                Map newMap = Instantiate(mapListPrefabs[i]);
                mapListsRuntime.Add(newMap);
                if (newMap.id == SaveLoadData.Ins.MapData.Level)
                {
                    newMap.SetPosPlayer();
                    newMap.SetPosCampCharacter();
                    newMap.OnInit(MapState.Start);
                }
                else if (newMap.id == SaveLoadData.Ins.MapData.Level - 1)
                {
                    newMap.enabled = false;
                    newMap.OnInit(MapState.During);
                }
                else if (newMap.id <= SaveLoadData.Ins.MapData.Level - 2)
                {
                    newMap.enabled = false;
                    newMap.OnInit(MapState.End);
                }
                else
                {
                    newMap.enabled = false;
                }
            }
        }
        else if (SaveLoadData.Ins.MapData.Level > mapListPrefabs.Count - 6)
        {
            for (int i = 0; i < mapListPrefabs.Count; i++)
            {
                Map newMap = Instantiate(mapListPrefabs[i]);
                mapListsRuntime.Add(newMap);
                if (newMap.id == SaveLoadData.Ins.MapData.Level)
                {
                    
                    newMap.SetPosPlayer();
                    newMap.SetPosCampCharacter();
                    newMap.OnInit(MapState.Start);
                }
                else if (newMap.id == SaveLoadData.Ins.MapData.Level - 1)
                {
                    newMap.enabled = false;
                    newMap.OnInit(MapState.During);
                }
                else if (newMap.id <= SaveLoadData.Ins.MapData.Level - 2)
                {
                    newMap.enabled = false;
                    newMap.OnInit(MapState.End);
                }
                else
                {
                    newMap.enabled = false;
                }
            }
        }

    }
    [Button]
    public void NextMap()
    {
        mapListsRuntime[SaveLoadData.Ins.MapData.Level].enabled = true;
        mapListsRuntime[SaveLoadData.Ins.MapData.Level].OnInit(MapState.Start);
        mapListsRuntime[SaveLoadData.Ins.MapData.Level].MoveCampCharacter();
        mapListsRuntime[SaveLoadData.Ins.MapData.Level - 1].enabled = false;
        mapListsRuntime[SaveLoadData.Ins.MapData.Level - 1].OnInit(MapState.During);
        mapListsRuntime[SaveLoadData.Ins.MapData.Level - 2].enabled = false;
        mapListsRuntime[SaveLoadData.Ins.MapData.Level - 2].OnInit(MapState.End);
        if (SaveLoadData.Ins.MapData.Level + 6 <= mapListPrefabs.Count)
        {
            Map newMap = Instantiate(mapListPrefabs[SaveLoadData.Ins.MapData.Level + 6]);
            mapListsRuntime.Add(newMap);
            newMap.enabled = false;
        }
    }
}
