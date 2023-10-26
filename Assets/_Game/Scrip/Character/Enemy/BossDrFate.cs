using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDrFate : Enemy
{
    [SerializeField] private Transform shootPos;
    [SerializeField] private float skill1Chance;
    [SerializeField] private float skill2Chance;

    protected override void DealDmg()
    {
        float random = Random.Range(0f, 1f);

        float skillChance = this.skill1Chance / (this.skill1Chance + this.skill2Chance);
        Debug.Log(random);
        if (random <= skillChance)
        {
            Debug.Log("nomal");
            SetSkill(Skill.NomalSkill);
        }
        else
        {
            Debug.Log("special");
            SetSkill(Skill.SpecialSkill);
        }
    }
    public void SetSkill(Skill skill)
    {
        switch (skill)
        {
            case Skill.NomalSkill:
                Player.Ins.OnHit(damage);
                break;
            case Skill.SpecialSkill:
                Lighting bullet = SmartPool.Ins.Spawn<Lighting>(PoolType.Lighting, shootPos.position, shootPos.rotation);
                bullet.OnInit(Player.Ins, damage * 1.5f);
                break;
            default:
                break;
        }

    }
}
