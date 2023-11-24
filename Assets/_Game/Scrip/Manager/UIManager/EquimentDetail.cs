using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquimentDetail : MonoBehaviour
{
    public EquimentType EquimentType;
    [SerializeField] Image buttonChange;
    [SerializeField] TextMeshProUGUI indexText;
    [SerializeField] TextMeshProUGUI upIndexText;
    [SerializeField] Image lockEquiment;
    [SerializeField] TextMeshProUGUI textCoint;
    [SerializeField] int coinUpdate;
    [SerializeField] GameObject coin;
    [SerializeField] GameObject reward;
    [SerializeField] GameObject upgradeButton;

    public int CoinUpdate { get => coinUpdate; set => coinUpdate = value; }
    // Start is called before the first frame update
    public void OnInit(float damage, float fireRate)
    {
        indexText.text = damage.ToString() + "\n" + fireRate.ToString();
        upIndexText.text = Random.Range(8, 12).ToString() + "%" + "\n" + Random.Range(1, 3).ToString() + "%";
    }
    public void OnImageChange()
    {
        buttonChange.color = Color.white;
    }
    public void OffImageChange()
    {
        buttonChange.color = Color.clear;
    }
    public void Unlock(){
        lockEquiment.gameObject.SetActive(false);
    }
    public void SetTextCoin(int coin){
        textCoint.text = coin.ToString();
    }
    public void SetReward(){
        coin.SetActive(false);
        reward.SetActive(true);
    }
    public void OffUpButton(float damage, float fireRate)
    {
        indexText.text = damage.ToString() + "\n" + fireRate.ToString();
        upIndexText.gameObject.SetActive(false);
        upgradeButton.SetActive(false);
    }
}
