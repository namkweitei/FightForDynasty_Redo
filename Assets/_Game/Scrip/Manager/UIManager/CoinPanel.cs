using TMPro;
using UnityEngine;

public class CoinPanel : MonoBehaviour, IObserver
{
    [SerializeField] TextMeshProUGUI coinText;
    [SerializeField] int coin;
    [SerializeField] TextMeshProUGUI buckText;
    [SerializeField] int buck;
    private void Awake()
    {
        SaveLoadData.Ins.PlayerData.RegisterObserver(this);
    }

    private void Start(){
        SetCoin();
        SetBuck();
    }

    // Start is called before the first frame update
    private void SetCoin()
    {
        coin = SaveLoadData.Ins.PlayerData.Coin;
        coinText.text = coin.ToString();
    }

    private void SetBuck(){
        buck = SaveLoadData.Ins.PlayerData.Buck;
        buckText.text = buck.ToString();
    }

    public void OnNotifyAddCurrency()
    {
        SetCoin();
        SetBuck();
    }
}
