using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shootting : MonoBehaviour
{

    [SerializeField] public bool isShooting = false;
    [SerializeField] protected float shootDelay = 1f;
    [SerializeField] protected float shootTimer = 0f;
    [SerializeField] protected Transform shootPoint;
    [SerializeField] protected ParticleSystem shootFx;

    private void Update()
    {

        this.IsShooting();


    }
    protected virtual void Shooting()
    {
    }
    protected virtual bool IsShooting()
    {
        return this.isShooting;
    }
}
