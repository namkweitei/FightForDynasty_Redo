using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
[CreateAssetMenu(fileName = "DataWave", menuName = "Fight_For_Dynasty/DataWave", order = 1)]
public class DataWave : ScriptableObject
{
    [SerializeField] List<Map> maps;
    [SerializeField] List<WaveData> waveDatas;

}
