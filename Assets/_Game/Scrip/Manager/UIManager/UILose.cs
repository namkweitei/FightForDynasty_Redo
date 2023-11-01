using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UILose : UICanvas
{
    [SerializeField] Button buttonRevice;
    [SerializeField] Button buttonNoThank;
    private void Start()
    {
        buttonRevice.onClick.AddListener(() =>
            {
                ReviceButton();
            });
        buttonNoThank.onClick.AddListener(() =>
            {
                NothankButton();
            });
    }
    public void NothankButton()
    {
        SaveLoadData.Ins.SaveALL();
        SceneManager.LoadScene(0);
        UIManager.Ins.CloseUI<UIWinn>();
        GameManager.ChangeState(GameState.GamePlay);
        GameManager.Ins.currenGameState = GameState.GamePlay;
        MapManager.Ins.OnInit();
    }
    public void ReviceButton()
    {
        //Reward

    }
}
