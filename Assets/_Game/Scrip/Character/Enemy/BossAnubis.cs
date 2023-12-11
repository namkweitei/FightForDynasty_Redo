using System;
using System.Collections;
using UnityEngine;
public enum Skill
{
    NomalSkill,
    SpecialSkill,
}
public class BossAnubis : Enemy
{
    [SerializeField] private Transform shootPos;
    [SerializeField] private float skill1Chance;
    [SerializeField] private float skill2Chance;

     public override void Attack()
    {
        isAttack = true;
        dir = Player.Ins.transform.position - transform.position;
        dir.y = 0f;
        dir.Normalize();
        Quaternion targetRotation = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 100);
        float timeAttack = anim.GetCurrentAnimatorStateInfo(1).length * 0.5f;
        float random = UnityEngine.Random.Range(0f, 1f);
        float skillChance = this.skill1Chance / (this.skill1Chance + this.skill2Chance);
        Debug.Log(random);
        if (random <= skillChance)
        {
            ChangeAnim(Constants.ANIM_ATTACKMELEE);
            StartCoroutine(GiveDamage(Skill.NomalSkill, timeAttack));
        }
        else
        {
            ChangeAnim(Constants.ANIM_ATTACKRANGER);
            StartCoroutine(GiveDamage(Skill.SpecialSkill, timeAttack));
        }
        agent.updateRotation = false;
        Invoke(nameof(ResetAttack), anim.GetCurrentAnimatorStateInfo(1).length );
    }
    IEnumerator GiveDamage(Skill skill, float time)
    {
        yield return new WaitForSeconds(time);
        SetSkill(skill);
    }
    public void SetSkill(Skill skill)
    {
        switch (skill)
        {
            case Skill.NomalSkill:
                Player.Ins.OnHit(damage);
                break;
            case Skill.SpecialSkill:
                SkillLighting bullet = SmartPool.Ins.Spawn<SkillLighting>(PoolType.SkillDrFate, shootPos.position, shootPos.rotation);
                bullet.OnInit(Player.Ins, damage * 1.5f, 1);
                break;
            default:
                break;
        }

    }
     public override void PatrolState(ref Action onEnter, ref Action onExecute, ref Action onExit)
    {
        onEnter = () =>
        {
            ChangeAnim(Constants.ANIM_WALKFW);
            Moving();
        };

        onExecute = () =>
        {

            if (IsDestionation)
            {
                StateMachine.ChangeState(IdleState);
            }
            if (IsPlayer())
            {
                timer -= Time.deltaTime;
                if (timer < 0)
                {
                    timer = 1f;
                    Attack();
                }
                else
                {
                    ChangeAnim(Constants.ANIM_WALKFW);
                }
            }
            else
            {
                ChangeAnim(Constants.ANIM_WALKFW);
            }
        };

        onExit = () =>
        {

        };
    }
}
