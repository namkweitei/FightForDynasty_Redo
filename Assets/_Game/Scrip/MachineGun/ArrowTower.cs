using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTower : Tower
{
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
                direction = enemy[0].transform.position - transform.position;
                direction.y = 0f;
                direction.Normalize();
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                rotation.rotation = Quaternion.Lerp(rotation.rotation, targetRotation, Time.deltaTime * 10);
                Quaternion bowRotate = Quaternion.LookRotation(direction);
                bow.rotation = Quaternion.Lerp(bow.rotation, bowRotate, Time.deltaTime * 10);
            }
        }
    }

    protected override void Attack(Enemy target)
    {
        this.shootTimer -= Time.fixedDeltaTime;
        if (this.shootTimer < 0)
        {
            if (shootPoint.Count > 1)
            {
                foreach (var item in shootPoint)
                {

                    Bullet bullet = SmartPool.Ins.Spawn<Bullet>(PoolType.Arrow, item.position, item.rotation);
                    bullet.OnInit(null, damage);
                    AudioManager.Ins.PlaySfx(Constants.SFX_ARROWTOWERATTACK);
                    // bullet.targetObject = target;
                    // bullet.Damage = damage;
                }
                this.shootTimer = shootSpeed;
            }else{
                 Bullet bullet = SmartPool.Ins.Spawn<Bullet>(PoolType.Arrow, shootPoint[0].position, shootPoint[0].rotation);
                    bullet.OnInit(target, damage);
                    AudioManager.Ins.PlaySfx(Constants.SFX_ARROWTOWERATTACK);
                    this.shootTimer = shootSpeed;
            }

        }
    }

}
