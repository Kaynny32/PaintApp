using Newtonsoft.Json.Linq;
using PaintIn3D;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Manager_Color_Brush : MonoBehaviour
{
    public static Manager_Color_Brush instance;

    List<ColorDate> colorDates;
    List<CanvasGroup> _cloneBtnColor;
    List<GameObject> _cloneToolColor;

    [SerializeField]
    List<GameObject> _listBrush = new List<GameObject>();

    [SerializeField]
    GameObject _prefabColor;
    [SerializeField]
    Transform _conColor;
    [SerializeField]
    GameObject _prefabColorTool;
    [SerializeField]
    Transform _conColorToolBrush;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        ReadJsonColor();
    }

    public void Add_Ui_Elements()
    {
        _cloneBtnColor = new List<CanvasGroup>();
        _cloneToolColor = new List<GameObject>();
        for (int i = 0; i< colorDates.Count; i++)
        {
            GameObject clone = Instantiate(_prefabColor, _conColor);
            GameObject cloneTool = Instantiate(_prefabColorTool, _conColorToolBrush);
            cloneTool.name = $"Tool_{colorDates[i].name}";
            cloneTool.GetComponent<P3dPaintSphere>().Color = new Color32(((byte)colorDates[i].r), ((byte)colorDates[i].g), ((byte)colorDates[i].b), 255);
            _cloneToolColor.Add(cloneTool);
            clone.name = colorDates[i].name;
            Color buf = new Color32(((byte)colorDates[i].r), ((byte)colorDates[i].g), ((byte)colorDates[i].b), 255);
            clone.GetComponent<Image>().color = buf;
            clone.GetComponent<ItemColor>().Set_IndexAndColor(i, buf);
            _cloneBtnColor.Add(clone.GetComponent<CanvasGroup>());
        }
    }

    void ReadJsonColor()
    {
        if (File.Exists("Color.json"))
        {
            colorDates = new List<ColorDate>();
            StreamReader streamReader = new StreamReader("Color.json");
            string str = streamReader.ReadToEnd();
            JObject jobj = JObject.Parse(str);
            JArray _jArray = jobj["color"].Value<JArray>();
            foreach (JObject _col in _jArray)
            {
                string _name = _col["name"].Value<string>();
                float _r = _col["r"].Value<float>();
                float _g = _col["g"].Value<float>();
                float _b = _col["b"].Value<float>();

                ColorDate colorDate = new ColorDate(_name, _r, _g, _b);
                colorDates.Add(colorDate);
            }
        }
        else 
        {
            DefaultColor();
        }
        Add_Ui_Elements();
    }

    void DefaultColor()
    {
        colorDates = new List<ColorDate>();
        ColorDate colorDateBlack = new ColorDate("black", 0, 0, 0);
        colorDates.Add(colorDateBlack);

        ColorDate colorDateWhite = new ColorDate("white", 255, 255, 255);
        colorDates.Add(colorDateWhite);

        ColorDate colorDateYellow = new ColorDate("yellow", 255, 220, 0);
        colorDates.Add(colorDateYellow);

        ColorDate colorDateRed = new ColorDate("red", 255, 27, 0);
        colorDates.Add(colorDateRed);

        ColorDate colorDateGreen = new ColorDate("green", 7, 255, 0);
        colorDates.Add(colorDateGreen);
    }

    public void ShowColorUi(int index, Color color)
    {
        for (int i = 0; i < _cloneBtnColor.Count; i++)
        {
            _cloneBtnColor[i].alpha = 0.5f;            
        }
        _cloneBtnColor[index].alpha = 1f;
        AssignColorToBrush(color);
    }

    public void AssignColorToBrush(Color color)
    {
        for (int i = 0; i < _listBrush.Count; i++)
        {
            _listBrush[i].GetComponent<P3dPaintDecal>().Color = color;
        }    
    }
}
