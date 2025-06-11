using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DateGame
{
   public List<GameObject> _listBrush;
   public List<GameObject> _listSticker;
   public List<ColorDate> _listColor;
}

[Serializable]
public class ColorDate
{
    public ColorDate(string hex)
    {
        this.hex = hex;

    }
    public string hex;
}