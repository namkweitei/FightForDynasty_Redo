using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Lighting : GameUnit
{
    [SerializeField] private Enemy enemy;
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    [SerializeField] private TrailRenderer trailRenderer;
    [SerializeField] private List<Enemy> enemyList;
    public int currentEnemyIndex;
    public int maxEnemyIndex;
    public void OnInit(Enemy enemy, float damage, int enemyCount)
    {
        enemyList.Clear();
        trailRenderer.Clear();
        this.enemy = enemy;
        this.damage = damage;
        currentEnemyIndex = 0;
        enemyList.Add(enemy);
        maxEnemyIndex = enemyCount;
    }
    private void Update()
    {
        if (Vector3.Distance(transform.position, enemy.transform.position) < 0.1f)
        {
            enemy.OnHit(damage);
            AudioManager.Ins.PlaySfx(Constants.SFX_LIGHTINGBULLET);
            if (currentEnemyIndex < maxEnemyIndex - 1)
            {
                CheckNearEnemy();
                currentEnemyIndex++;
            }
            else
            {
                SmartPool.Ins.Despawn(gameObject);
            }
        }
        else
        {
            MoveToEnemy();
        }


    }
    private void MoveToEnemy()
    {
        transform.position = Vector3.MoveTowards(transform.position, enemy.transform.position, speed * Time.deltaTime);
    }
    private void CheckNearEnemy()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 3f, 1 << 14);
        foreach (var item in colliders)
        {
            if (!enemyList.Contains(item.GetComponent<Enemy>()))
            {
                enemy = item.GetComponent<Enemy>();
                enemyList.Add(item.GetComponent<Enemy>());
                break;
            }
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 3f);
    }

}
