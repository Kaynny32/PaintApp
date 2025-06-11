using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DateToolsAndUiElements
{
    public DateToolsAndUiElements(string name, bool isActive, GameObject tools, GameObject uiElements)
    { 
        this.name = name;
        this.isActive = isActive;
        this.tools = tools;
        this.uiElements = uiElements;
    }
    public string name;
    public bool isActive;
    public GameObject tools;
    public GameObject uiElements;
}
