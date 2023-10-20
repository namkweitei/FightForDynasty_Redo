using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISpecialDeal : UICanvas
{
    public void CloseButton()
    {
        UIManager.Ins.CloseUI<UISpecialDeal>();
    }
}
