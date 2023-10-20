using System;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class TowerPopUp : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI currentDamage;
    [SerializeField] TextMeshProUGUI nextLevelDamage;
    [SerializeField] TextMeshProUGUI currentFireRate;
    [SerializeField] TextMeshProUGUI nextLevelFireRate;
    [SerializeField] CanvasGroup canvasGroup;
    private void Update() {
        transform.rotation = Camera.main.transform.rotation;
    }
    public void OnInit(float damage, float fireRate, int lv)
    {
        currentDamage.text = damage.ToString();
        nextLevelDamage.text = Math.Round((double)damage * 0.3f, 1).ToString();
        currentFireRate.text = (1 / fireRate).ToString();
        nextLevelFireRate.text = Math.Round((double)(1 / fireRate * 0.1f), 1).ToString();
        levelText.text = "Level " + lv.ToString();
    }
    public void PopUp(float alpha){
        canvasGroup.DOFade(alpha, 0.5f);
    }
}
