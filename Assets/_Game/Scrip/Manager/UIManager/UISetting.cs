using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class UISetting : UICanvas
{
    [SerializeField] Image soundClose;
    [SerializeField] Image musicClose;
    public void CloseButton()
    {
        UIManager.Ins.CloseUI<UISetting>();
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
}
