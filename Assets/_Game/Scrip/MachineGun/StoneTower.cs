
using System;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Assertions.Must;
public class StoneTower : Tower
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
                direction = enemy[0].transform.position - transform.position;
                direction.y = 0f;
                direction.Normalize();
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 10);

                Attack(enemy[0]);
                //bow.rotation = Quaternion.Lerp(bow.rotation, bowRotate, Time.deltaTime * 100);


            }
        }
    }
    protected override void Attack(Character target)
    {
        this.shootTimer -= Time.fixedDeltaTime;
        if (this.shootTimer < 0)
        {
            bow.DORotateQuaternion(Quaternion.LookRotation(direction), 0.5f).OnComplete(() =>
            {
                foreach (var item in shootPoint)
                {
                    BulletStone bullet = SmartPool.Ins.Spawn<BulletStone>(PoolType.Stone, item.position, item.rotation);
                    Vector3 center = (item.position + target.transform.position) * 0.5f;
                    AudioManager.Ins.PlaySfx(Constants.SFX_STONETOWERATTACK);
                    center.y += 3f;
                    bullet.OnInit(item, target, damage);
                }
                bow.DOLocalRotateQuaternion(originalRotate_2, 0.5f);
                //bow.DORotateQuaternion(originalRotate_2, 0.5f);
            });
            this.shootTimer = shootSpeed;

        }
    }
}
