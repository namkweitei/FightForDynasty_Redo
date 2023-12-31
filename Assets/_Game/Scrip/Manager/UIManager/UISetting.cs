using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UISetting : UICanvas
{
    [SerializeField] Image soundClose;
    [SerializeField] Image musicClose;
    [SerializeField] Image vibrationClose;
    [SerializeField] Transform pobUp;
    [SerializeField] Image background;
    public void StartPobUp(){
        Time.timeScale = 0;
        background.DOFade(0.7f, 0.5f);
        pobUp.localScale = Vector3.zero;
        pobUp.DOScale(1, 0.5f).SetUpdate(true);

    }
    public void CloseButton()
    {
        AudioManager.Ins.PlaySfx(Constants.SFX_CLOSEUI);
        UIManager.Ins.CloseUI<UISetting>();

        if (UIManager.Ins.GetUI<UIGamePlay>().countSpeed == 1)
        {
            Time.timeScale = 1.25f;
        }
        else if (UIManager.Ins.GetUI<UIGamePlay>().countSpeed == 2)
        {
            Time.timeScale = 1.5f;
        }
        else if (UIManager.Ins.GetUI<UIGamePlay>().countSpeed == 0)
        {
            Time.timeScale = 1;
        }

        if (GameManager.IsState(GameState.Pause))
        {
            GameManager.ChangeState(GameState.Playing);
        }
    }
    public void SoundButton()
    {
        AudioManager.Ins.ToggleSfx();
        if (!soundClose.gameObject.activeSelf)
        {
            soundClose.gameObject.SetActive(true);
        }
        else
        {
            soundClose.gameObject.SetActive(false);
        }
    }
    public void MusicButton()
    {
        AudioManager.Ins.ToggleMusic();
        if (!musicClose.gameObject.activeSelf)
        {
            musicClose.gameObject.SetActive(true);
        }
        else
        {
            musicClose.gameObject.SetActive(false);
        }
    }
    public void VibrationButton(){
        GameManager.IsVibration = !GameManager.IsVibration;
         if (!vibrationClose.gameObject.activeSelf)
        {
            vibrationClose.gameObject.SetActive(true);
        }
        else
        {
            vibrationClose.gameObject.SetActive(false);
        }
    }
}
