using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Equiment
{
    public EquimentType equimentType;
    public float damage;
    public float attackSpeed;
    public float range;
    public RuntimeAnimatorController anim;
    public float offset;
}