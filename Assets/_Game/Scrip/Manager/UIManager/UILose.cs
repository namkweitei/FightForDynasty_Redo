using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
public class UILose : UICanvas
{
    [SerializeField] Button buttonRevice;
    [SerializeField] Button buttonNoThank;
        [SerializeField] Transform pobUp;
    [SerializeField] Image background;
    public void StartPobUp(){
        Time.timeScale = 0;
        background.DOFade(0.7f, 0.5f);
        pobUp.localScale = Vector3.zero;
        pobUp.DOScale(1, 0.5f).SetUpdate(true);

    }
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
        if(SaveLoadData.Ins.CampCharacterData.Hp <= 0){
            SaveLoadData.Ins.MapData.Wave = 0;
            SaveLoadData.Ins.CampCharacterData.Hp = SaveLoadData.Ins.CampCharacterData.MaxHp;
        }
        SaveLoadData.Ins.SaveALL();
        SceneManager.LoadScene(0);
        UIManager.Ins.CloseUI<UILose>();
        GameManager.ChangeState(GameState.GamePlay);
        GameManager.Ins.currenGameState = GameState.GamePlay;
        MapManager.Ins.OnInit();
        Time.timeScale = 1;
        
    }
    public void ReviceButton()
    {
        //Reward
        UIManager.Ins.CloseUI<UILose>();
        if (GameManager.IsState(GameState.Pause))
        {
            GameManager.ChangeState(GameState.Playing);
        }
        Player.Ins.OnInit();
        if(SaveLoadData.Ins.CampCharacterData.Hp <= 0){
            SaveLoadData.Ins.CampCharacterData.Hp = SaveLoadData.Ins.CampCharacterData.MaxHp;
        }
        if (UIManager.Ins.GetUI<UIGamePlay>().isSpeedUp)
        {
            Time.timeScale = 2;
        }
        else
        {
            Time.timeScale = 1;
        }


    }
}
