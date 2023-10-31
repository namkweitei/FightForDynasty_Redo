using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDragon : Enemy
{
    [SerializeField] ParticleSystem particle;
    public override void Attack()
    {
        isAttack = true;
        dir = Player.Ins.transform.position - transform.position;
        dir.y = 0f;
        dir.Normalize();
        Quaternion targetRotation = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 50);
        particle.Play();
        float timeAttack = anim.GetCurrentAnimatorStateInfo(1).length * 0.5f;
        Invoke(nameof(DealDmg), timeAttack);
        agent.updateRotation = false;
        Invoke(nameof(ResetAttack), 1f);
    }
}
