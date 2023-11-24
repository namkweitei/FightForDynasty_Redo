using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ButtonClaim: MonoBehaviour{
    [SerializeField] private Button button;
    [SerializeField] private TypeButton typeButton;
    [SerializeField] private TypeEventButton typeEventButton;
    [SerializeField] private int rewardValue;
    [SerializeField] private int priceValue;
    [SerializeField] private TextMeshProUGUI rewardValueText;
    [SerializeField] private TextMeshProUGUI priceValueText;
    private void Start() {
       
            rewardValueText.text = "+" + rewardValue.ToString();
            if(typeButton == TypeButton.Buck){
                priceValueText.text = "$ " + priceValue.ToString() ;
            }else{
                priceValueText.text = priceValue.ToString();
            }
       
        
    }
    public Button Button { get => button; set => button = value; }
    public TypeButton TypeButton { get => typeButton; set => typeButton = value; }
    public TypeEventButton TypeEventButton { get => typeEventButton; set => typeEventButton = value; }
    public int RewardValue { get => rewardValue; set => this.rewardValue = value; }
    public int PriceValue { get => priceValue; set => this.priceValue = value; }
}
