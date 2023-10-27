
using System;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class GameManager : Singleton<GameManager>
{
    public static GameState gameState;
    public GameState currenGameState;
    public static void ChangeState(GameState state)
    {
        gameState = state;
    }

    public static bool IsState(GameState state) => gameState == state;
    [SerializeField] private Avoidance avoidance;

    public Avoidance Avoidance { get => avoidance; set => avoidance = value; }

    protected override void Awake()
    {
        base.Awake();
        //tranh viec nguoi choi cham da diem vao man hinh
        //Input.multiTouchEnabled = false;
        //target frame rate ve 60 fps
        Application.targetFrameRate = 60;
        //tranh viec tat man hinh
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        //xu tai tho
        int maxScreenHeight = 1280;
        float ratio = (float)Screen.currentResolution.width / (float)Screen.currentResolution.height;
        if (Screen.currentResolution.height > maxScreenHeight)
        {
            Screen.SetResolution(Mathf.RoundToInt(ratio * (float)maxScreenHeight), maxScreenHeight, true);
        }
        ChangeState(GameState.GamePlay);
        currenGameState = gameState;
        UIManager.Ins.OpenUI<UILoading>();

    }


    [Button]
    public void ChangeEquiment(EquimentType equimentType)
    {
        Player.Ins.ChangeEquiment(equimentType);
    }

}

public enum GameState
{
    GamePlay,
    StartWave,
    EndWave,
    Playing,
    Pause,
    End,
}



