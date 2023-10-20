using System.Collections;
using System.Collections.Generic;
using Unity.XR.OpenVR;
using UnityEngine;
using UnityEngine.UI;



public enum TypeButton{
        Buck,
        Coin,
    }
    public enum TypeEventButton{
        Reward,
        Purchase,
    }
public class UIShop : UICanvas
{
    public ButtonClaim[] buttonClaims;
    public Button RemoveAds;
    public DailyRewardDisplay DailyRewardBuckDisplay;
    public DailyRewardDisplay DailyRewardCoinDisplay;
    private void Start() {
        foreach(ButtonClaim _button in buttonClaims){
            _button.Button.onClick.AddListener(() =>
            {
                ProcessEventButton(_button.TypeButton, _button.TypeEventButton, _button.RewardValue, _button.PriceValue);
            });
        }

    }
    public void CloseButton()
    {
        UIManager.Ins.CloseUI<UIShop>();
        GameManager.ChangeState(GameManager.Ins.currenGameState);
    }

    private void ProcessEventButton(TypeButton typeButton, TypeEventButton typeEventButton, int value, int price){
        if(typeButton == TypeButton.Buck){
            if(typeEventButton == TypeEventButton.Reward){
                AddBuck(value);
            }else if(typeEventButton == TypeEventButton.Purchase){
                PurchaseBuck(price, value);
            }
        }
        else if(typeButton == TypeButton.Coin){
            if(typeEventButton == TypeEventButton.Reward){
                AddCoin(value);
            }else if(typeEventButton == TypeEventButton.Purchase){
                PurchaseCoin(price, value);
            }
        }
    }

    private void PurchaseCoin(int buckPrice, int claimValue){
        if(SaveLoadData.Ins.PlayerData.Buck >= buckPrice)
        {
            Debug.Log("Purchase " + buckPrice + " buck and add " + claimValue + " coin to player");
            SaveLoadData.Ins.PlayerData.Buck -= buckPrice;
            AddCoin(claimValue);
        }
    }

    private void AddCoin(int value){
        SaveLoadData.Ins.PlayerData.Coin += value;
    }

    private void PurchaseBuck(int price, int claimValue){
        //Buy in game, price is money
        AddBuck(claimValue);
    }

    private void AddBuck(int value){
        SaveLoadData.Ins.PlayerData.Buck += value;
    }

}
