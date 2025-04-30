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
    public ColorDate(string name, float r, float g, float b)
    {
        this.name = name;
        this.r = r;
        this.g = g;
        this.b = b;
    }
    public string name;
    public float r;
    public float g;
    public float b;
}