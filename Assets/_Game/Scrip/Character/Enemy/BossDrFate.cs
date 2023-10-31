using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDrFate : Enemy
{
    [SerializeField] private Transform shootPos;
   
    protected override void DealDmg()
    {
        Lighting bullet = SmartPool.Ins.Spawn<Lighting>(PoolType.Lighting, shootPos.position, shootPos.rotation);
        bullet.OnInit(Player.Ins, damage * 1.5f);
    }
   
}
