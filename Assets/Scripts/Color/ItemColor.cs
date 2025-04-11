using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemColor : MonoBehaviour
{
    public int index;
    Color color;

    public void Set_IndexAndColor(int i, Color color)
    {
        index = i;
        this.color = color;
    }

    public void ShowColor()
    {
        Manager_Color_Brush.instance.ShowColorUi(index, color);
    }
}
