using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class UILoading : UICanvas
{
    public Image progress;
    [SerializeField] float loadingDuration;

    Action OnLoaded;
    private void Start()
    {
        Init();
    }
    public void Init()
    {

        OnLoaded = null;
        LoadingRun();
    }
    public void LoadingRun()
    {
        Time.timeScale = 1f;
        progress.fillAmount = 0;
        progress.DOFillAmount(1f, loadingDuration).SetEase(Ease.Linear).OnComplete(() =>
        {
            OnLoaded?.Invoke();
            OnLoaded = null;
            UIManager.Ins.OpenUI<UIGamePlay>();
            UIManager.Ins.CloseUI<UILoading>();
        });
    }
}
