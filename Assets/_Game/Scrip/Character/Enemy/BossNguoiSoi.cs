using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossNguoiSoi : Enemy
{
    public override void Attack()
    {
        isAttack = true;
        dir = Player.Ins.transform.position - transform.position;
        dir.y = 0f;
        dir.Normalize();
        Quaternion targetRotation = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 50);
        float timeAttack = anim.GetCurrentAnimatorStateInfo(1).length * 0.5f;
        Invoke(nameof(DealDmg), timeAttack);
        agent.updateRotation = false;
        Invoke(nameof(ResetAttack), anim.GetCurrentAnimatorStateInfo(1).length);
    }
    protected override void DealDmg()
    {
        Player.Ins.OnHit(damage);
        int i = Random.Range(0, 2);
        if (i == 0)
        {
            AudioManager.Ins.PlaySfx(Constants.SFX_WEREWOLVESATTACK_1);
        }
        else
        {
            AudioManager.Ins.PlaySfx(Constants.SFX_WEREWOLVESATTACK_2);
        }
    }
    protected override void OnDead()
    {
        OnDeathAction?.Invoke(this);
        agent.isStopped = true;
        agent.updateRotation = false;
        ChangeAnim(Constants.ANIM_DEAD);
        AudioManager.Ins.PlaySfx(Constants.SFX_WEREWOLVESDEAD);
        if(animAnimal != null){
            animAnimal.SetTrigger("dead");
        }
        base.OnDead();
    }
}
