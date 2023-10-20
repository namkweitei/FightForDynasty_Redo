using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTurret : Tower
{
    [SerializeField] ParticleSystem particle;
    protected override void Rotate()
    {
        if (enemy.Count > 0)
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
            }
        }
    }

    protected override void Attack(Character target)
    {
        particle.Play();
        shootTimer -= Time.fixedDeltaTime;
        if (shootTimer < 0)
        {
            shootTimer = shootSpeed;
            target.OnHit(damage);
        }
    }

}
