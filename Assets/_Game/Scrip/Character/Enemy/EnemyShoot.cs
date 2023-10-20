using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : Enemy
{
    [SerializeField] private Transform shootPos;

    protected override void DealDmg()
    {
        Bullet bullet = SmartPool.Ins.Spawn<Bullet>(PoolType.Arrow, shootPos.position, shootPos.rotation);
        bullet.OnInit(Player.Ins, damage);
        // bullet.targetObject = player.GetComponent<Character>();
        // bullet.Damage = damage;
    }
}
