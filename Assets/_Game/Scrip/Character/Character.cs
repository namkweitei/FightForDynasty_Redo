using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Character : GameUnit
{
    [SerializeField] protected AttackZone attackZone;
    [SerializeField] protected CharacterHit characterHit;
    [SerializeField] protected Animator anim;
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected float speed;
    [SerializeField] protected float maxHp;
    [SerializeField] protected float hp;
    protected string animName = "idle";
    public bool IsDead;


    public virtual void OnInit()
    {
        characterHit.OnInit(maxHp);
        IsDead = false;
        hp = maxHp;
    }
    protected void ChangeAnim(string animName)
    {
        if (this.animName != animName)
        {
            anim.ResetTrigger(this.animName);
            this.animName = animName;
            anim.SetTrigger(this.animName);
        }
    }

    public virtual void OnHit(float damage)
    {
        if (!IsDead && GameManager.IsState(GameState.Playing))
        {
            hp -= damage;
            characterHit.SetHp(hp);
            if(hp <= 0){
                IsDead = true;
                OnDead();
            }
        }
    }

    protected virtual void OnDead()
    {
        ChangeAnim(Constants.ANIM_DEAD);
        Invoke("OnDespawn", 1.5f);

    }



    protected virtual void OnDespawn()
    {

    }

}
