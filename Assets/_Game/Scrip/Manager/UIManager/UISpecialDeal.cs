using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISpecialDeal : UICanvas
{
    public void CloseButton()
    {
        Time.timeScale = 1;
        UIManager.Ins.CloseUI<UISpecialDeal>();
    }
}
