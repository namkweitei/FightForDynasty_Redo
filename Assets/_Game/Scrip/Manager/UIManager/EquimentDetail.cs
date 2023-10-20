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
}
