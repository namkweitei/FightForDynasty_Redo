using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Equiment
{
    public bool IsUnlock;
    public EquimentType equimentType;
    public float damage;
    public float attackSpeed;
    public float range;
    public RuntimeAnimatorController anim;
    public float offset;
    public int countUpdate;
}
