using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDrFate : Enemy
{
    [SerializeField] private Transform shootPos;
   
    protected override void DealDmg()
    {
        SkillLighting bullet = SmartPool.Ins.Spawn<SkillLighting>(PoolType.SkillDrFate, shootPos.position, shootPos.rotation);
        bullet.OnInit(Player.Ins, damage * 1.5f, 1);
    }
   
}
