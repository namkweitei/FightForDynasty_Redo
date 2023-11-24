using System.Collections;
using System.Collections.Generic;
using Unity.XR.OpenVR;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine.Events;


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
        AudioManager.Ins.PlaySfx(Constants.SFX_CLOSEUI);
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
                UnityEvent e = new UnityEvent();
                e.AddListener(() =>
                {
                    Debug.Log("reward loaded!");
                    AddBuck(value);
                    //logevent
                    SkygoBridge.instance.LogEvent("reward_"+value);
                });
                //SkygoBridge.instance.ShowRewarded(e,null);
                //reward
                ApplovinBridge.instance.ShowRewarAdsApplovin(e,null);
            }else if(typeEventButton == TypeEventButton.Purchase){
                PurchaseBuck(price, value);
            }
        }
        else if(typeButton == TypeButton.Coin){
            if(typeEventButton == TypeEventButton.Reward){
                UnityEvent e = new UnityEvent();
                e.AddListener(() =>
                {
                    Debug.Log("reward loaded!");
                    AddCoin(value);
                    //logevent
                    SkygoBridge.instance.LogEvent("reward_"+value);
                });
                //SkygoBridge.instance.ShowRewarded(e,null);
                //reward
                ApplovinBridge.instance.ShowRewarAdsApplovin(e,null);
            }else if(typeEventButton == TypeEventButton.Purchase){
                PurchaseCoin(price, value);
            }
        }
    }
    public void RemoveAds(int price)
    {
        string sku = "";

        sku = "fight_dynasty_noads_199";
        UnityEvent e = new UnityEvent();
        e.AddListener(() =>
        {
            SkygoBridge.instance.CanShowAd = 0;

     
            //noAdsBtn.SetActive(false);
            //logevent
            SkygoBridge.instance.LogEvent("purchase_no_Ads");
        });
        //purchase
        SkygoBridge.instance.PurchaseIAP(sku, e);
    }
    public void SpecialDeal(int price){
        //Buy in game, price is money
        string sku = "";

        sku = "fight_dynasty_special_499";
        UnityEvent e = new UnityEvent();
        e.AddListener(() =>
        {
            SkygoBridge.instance.CanShowAd = 0;

            SaveLoadData.Ins.PlayerData.Coin += 10000;
            for (int i = 0; i < SaveLoadData.Ins.PlayerData.EquimentDatas.Count; i++)
            {
                if (SaveLoadData.Ins.PlayerData.EquimentDatas[i].equimentType == EquimentType.Spear)
                {
                    SaveLoadData.Ins.PlayerData.EquimentDatas[i].IsUnlock = true;
                }
            }
            SpecialDealObject.SetActive(false);
            //noAdsBtn.SetActive(false);
            //logevent
            SkygoBridge.instance.LogEvent("purchase_specialdeal");
        });
        //purchase
        SkygoBridge.instance.PurchaseIAP(sku, e);
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
        string sku = "";
        Debug.Log(price + " : " + claimValue);
        sku = "fight_dynasty_cash_" + price.ToString();
        Debug.Log(sku);
        UnityEvent e = new UnityEvent();
        e.AddListener(() =>
        {
            AddBuck(claimValue);
        
            //noAdsBtn.SetActive(false);
        });

        SkygoBridge.instance.PurchaseIAP(sku, e);
      
    }

    public void AddBuck(int value){
        SaveLoadData.Ins.PlayerData.Buck += value;
    }

}
