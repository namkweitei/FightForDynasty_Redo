using System;
using System.Collections;
using System.Collections.Generic;
using NavMeshAvoidance;
using UnityEngine;
using DG.Tweening;
[Serializable]
public class Barrack : MonoBehaviour
{
    public Transform spawnPoint;
    public List<EnemyCate> enemyCates;
    public bool isSpawnEnemy = true;
    public float spawnTimer = 0f;
    public int currentEnemyCate = 0;
    public Map map;

    // private void Update() {
    //     if(isSpawnEnemy){
    //         SpawnEnemy(action,campoint);
    //     }
    // }
    public void SpawnEnemy()
    {
        if (isSpawnEnemy)
        {
            spawnTimer -= Time.fixedDeltaTime;
            if (currentEnemyCate < enemyCates.Count)
            {
                if (enemyCates[currentEnemyCate].enemyCount > 0)
                {
                    if (spawnTimer <= 0f)
                    {
                        Enemy enemy = map.SpawnEnemy(enemyCates[currentEnemyCate].enemyName, this.spawnPoint.position, this.spawnPoint.rotation);
                        enemy.SetTarget(CampCharacter.Ins.transform);
                        enemy.StateMachine.ChangeState(enemy.PatrolState);
                        enemyCates[currentEnemyCate].enemyCount--;
                        spawnTimer = enemyCates[currentEnemyCate].enemyTimeSpawn;
                    }
                }
                else
                {
                    currentEnemyCate++;
                }
            }
            else
            {
                isSpawnEnemy = false;
                transform.DOScale(0f, 0.5f).OnComplete(() => { this.gameObject.SetActive(false); });

            }
        }
    }
}
