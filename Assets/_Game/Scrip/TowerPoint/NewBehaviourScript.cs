using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix;
using Sirenix.OdinInspector;
public class NewBehaviourScript : MonoBehaviour
{
    public List<UpTowerSystem> tower;
    [Button]
    public void SetId()
    {
        for (int i = 0; i < tower.Count; i++)
        {
            tower[i].Id = i;
        }
    }

}
