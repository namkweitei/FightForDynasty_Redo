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
    [SerializeField] private List<GameObject> armor;
    public List<GameObject> Armor{get => armor; set => armor = value;}

private void Start() {
    SetArmor(SaveLoadData.Ins.PlayerData.Level);
}
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
    public void SetArmor(int lv)
    {
        if (lv > 19)
        {
            armor[0].SetActive(true);
            armor[1].SetActive(true);
            armor[2].SetActive(true);
            armor[3].SetActive(true);
            armor[4].SetActive(true);
        }
        else if (lv > 14)
        {
            armor[0].SetActive(true);
            armor[1].SetActive(true);
            armor[2].SetActive(true);
            armor[3].SetActive(true);
        }
        else if (lv > 9)
        {
            armor[0].SetActive(true);
            armor[1].SetActive(true);
            armor[2].SetActive(true);
        }
        else if (lv > 4)
        {
            armor[0].SetActive(true);
            armor[1].SetActive(true);
        }
        else if (lv > 1)
        {
            armor[0].SetActive(true);
        }

    }
}
