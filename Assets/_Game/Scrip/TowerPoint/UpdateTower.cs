using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpdateTower : MonoBehaviour
{
    [SerializeField] private TextMeshPro coin;
    [SerializeField] private Image loadCircle;
    [SerializeField] private float loadTime = 5f;
    public Image LoadCircle { get => loadCircle; set => loadCircle = value; }
    public void OnInit(int currentCoin)
    {
        loadCircle.fillAmount = 0;
        this.coin.text = currentCoin.ToString();

    }
    public void SetCurrentCoin(int currentCoin)
    {
        coin.text = currentCoin.ToString();
    }


    public void LoadTower()
    {
        loadCircle.fillAmount += 1 / loadTime * Time.deltaTime * 5;
    }
}
