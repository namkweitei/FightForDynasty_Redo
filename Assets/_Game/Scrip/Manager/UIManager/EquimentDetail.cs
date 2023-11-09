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

    public int CoinUpdate { get => coinUpdate; set => coinUpdate = value; }
    private void Awake() {
        coinUpdate = Random.Range(30, 50);
    }
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
}
