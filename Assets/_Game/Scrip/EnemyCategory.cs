using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Category", menuName = "Enemy Category")]
public class EnemyCategory : ScriptableObject
{
    public List<EnemyCate> enemyCates = new List<EnemyCate>();
    public List<EnemyCate> enemybarack2 = new List<EnemyCate>();
}

[Serializable]
public class EnemyCate
{
    public PoolType enemyName;
    public int enemyCount;
    public float enemyTimeSpawn;
}
