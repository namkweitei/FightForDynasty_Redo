using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingTower : Tower
{
    [SerializeField] int enemyCount;
    protected override void Start() => enemy.Clear();
    protected override void FixedUpdate()
    {
        Rotate();
    }
    protected override void Rotate()
    {
        if (enemy.Count > 0 && GameManager.IsState(GameState.Playing))
        {
            if (enemy[0].IsDead)
            {
                enemy.Remove(enemy[0]);
            }
            else
            {
                Attack(enemy[0]);
            }
        }
    }
    protected override void Attack(Character enemy)
    {
        this.shootTimer -= Time.fixedDeltaTime;
        if (this.shootTimer < 0)
        {
            Lighting bullet = SmartPool.Ins.Spawn<Lighting>(PoolType.Lighting, shootPoint[0].position, shootPoint[0].rotation);
            AudioManager.Ins.PlaySfx(Constants.SFX_LIGHTINGTOWERATTACK);
            bullet.OnInit(enemy, damage, enemyCount);
            this.shootTimer = shootSpeed;
        }
    }
}
