using System.Collections;
using System.Collections.Generic;
using Unity.XR.OpenVR;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using JetBrains.Annotations;


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
    public DailyRewardDisplay DailyRewardBuckDisplay;
    public DailyRewardDisplay DailyRewardCoinDisplay;
    public GameObject SpecialDealObject;
    public Image background;
    public Transform pobUp;
    private void Start() {
        foreach(ButtonClaim _button in buttonClaims){
            _button.Button.onClick.AddListener(() =>
            {
                ProcessEventButton(_button.TypeButton, _button.TypeEventButton, _button.RewardValue, _button.PriceValue);
            });
        }
        if(SaveLoadData.Ins.IsGetSpecialDeal){
            SpecialDealObject.SetActive(false);
        }
    }
    public void CloseButton()
    {
        UIManager.Ins.CloseUI<UIShop>();
        if (UIManager.Ins.GetUI<UIGamePlay>().isSpeedUp)
        {
            Time.timeScale = 2;
        }
        else
        {
            Time.timeScale = 1;
        }
        GameManager.ChangeState(GameManager.Ins.currenGameState);
    }
    public void StartPobUp(){
        Time.timeScale = 0;
        background.DOFade(0.7f, 0.5f);
        pobUp.transform.localScale = Vector3.zero;
        pobUp.transform.DOScale(1, 0.5f).SetUpdate(true);
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
    public void SpecialDeal(){
        SaveLoadData.Ins.PlayerData.Coin += 10000;
        for(int i = 0; i < SaveLoadData.Ins.PlayerData.EquimentDatas.Count; i++){
                    if(SaveLoadData.Ins.PlayerData.EquimentDatas[i].equimentType == EquimentType.Spear){
                        SaveLoadData.Ins.PlayerData.EquimentDatas[i].IsUnlock = true;
                    }
        }
        SpecialDealObject.SetActive(false);
    }
    private void PurchaseCoin(int buckPrice, int claimValue){
        if(SaveLoadData.Ins.PlayerData.Buck >= buckPrice)
        {
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
