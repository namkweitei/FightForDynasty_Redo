using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class UIWinn : UICanvas
{
    [SerializeField] Button buttonReward;
    [SerializeField] Button buttonGet;
    [SerializeField] TextMeshProUGUI coinText;
    [SerializeField] int coinReward;
    [SerializeField] TextMeshProUGUI getCoinText;
    [SerializeField] int getCoin;
    [SerializeField] Transform pobUp;
    [SerializeField] Image background;
    public void StartPobUp()
    {
        Time.timeScale = 0;
        background.DOFade(0.7f, 0.5f);
        pobUp.localScale = Vector3.zero;
        pobUp.DOScale(1, 0.5f).SetUpdate(true);

    }
    private void Start()
    {
        coinReward = Random.Range(20, 30) * SaveLoadData.Ins.PlayerData.Level;
        coinText.text = coinReward.ToString();
        getCoin = Random.Range(30, 40) * SaveLoadData.Ins.PlayerData.Level;
        getCoinText.text = getCoin.ToString();
        buttonReward.onClick.AddListener(() =>
            {
                RewardButton();
            });
        buttonGet.onClick.AddListener(() =>
            {
                GetButton();
            });
    }

    public void RewardButton()
    {
        MapManager.Ins.NextMap();
        AddCoin(coinReward);
        UIManager.Ins.CloseUI<UIWinn>();
        GameManager.ChangeState(GameState.GamePlay);
        GameManager.Ins.currenGameState = GameState.GamePlay;
        if (UIManager.Ins.GetUI<UIGamePlay>().isSpeedUp)
        {
            Time.timeScale = 2;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
    public void GetButton()
    {
        //Reward
        MapManager.Ins.NextMap();
        AddCoin(getCoin);
        UIManager.Ins.CloseUI<UIWinn>();
        GameManager.ChangeState(GameState.GamePlay);
        GameManager.Ins.currenGameState = GameState.GamePlay;
                if (UIManager.Ins.GetUI<UIGamePlay>().isSpeedUp)
        {
            Time.timeScale = 2;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
    private void AddCoin(int value)
    {
        SaveLoadData.Ins.PlayerData.Coin += value;
    }
    private void OnApplicationQuit()
    {
        AddCoin(Random.Range(20, 30) * SaveLoadData.Ins.PlayerData.Level);
    }
}
