using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampCharacterHealing : MonoBehaviour
{
    float timeHeal = 2f;
    bool isPlayerStay;
    private void Update()
    {
        if (isPlayerStay && SaveLoadData.Ins.PlayerData.Coin > 0 && !Player.Ins.GetMove() && !CampCharacter.Ins.isUpdate)
        {
            timeHeal -= Time.deltaTime;
            if (timeHeal <= 0)
            {
                CampCharacter.Ins.Healling();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        isPlayerStay = true;
    }
    private void OnTriggerExit(Collider other)
    {
        isPlayerStay = false;
        timeHeal = 2f;
    }
}
