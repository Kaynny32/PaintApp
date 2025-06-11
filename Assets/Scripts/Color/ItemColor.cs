using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemColor : MonoBehaviour
{
    public int index;
    public Color color;

    public void Set_IndexAndColor(int i, Color color)
    {
        index = i;
        this.color = color;
    }

    public void ShowColor()
    {
        GameManager.instance.HideCollorButton(gameObject, index);  
        GameManager.instance.SwitchColor(color);
        GameManager.instance.CloseSticker();
        UI_Manager.instance.ClosePopapScale();
    }
}
