using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
    public string name { get; set; }
    public float r { get; set; }
    public float g { get; set; }
    public float b { get; set; }
}
