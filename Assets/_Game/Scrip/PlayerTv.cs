using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerTv : Singleton<PlayerTv>
{
    [SerializeField] List<RuntimeAnimatorController> animations;
    [SerializeField] Animator anim;
    [SerializeField] List<Transform> transforms;

    [Button]
    public void ChangeEquip(int count)
    {
        for (int i = 0; i < transforms.Count; i++)
        {
            transforms[i].gameObject.SetActive(false);
            if (i == count)
            {
                transforms[i].gameObject.SetActive(true);
            }
        }
        anim.runtimeAnimatorController = animations[count];
    }
}
