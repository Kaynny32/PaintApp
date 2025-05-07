using DG.Tweening;
using PaintIn3D;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ListToolsAndUIElements
{
    public List<GameObject> _listSticker = new List<GameObject>();
    public List<GameObject> _listBrush = new List<GameObject>();
    public List<GameObject> _listColor = new List<GameObject>();
    [Header("Tools")]
    public List<GameObject> _listStickerTools = new List<GameObject>();
    public List<GameObject> _listBrushTools = new List<GameObject>();
    public List<GameObject> _listColorTools = new List<GameObject>();
}
public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject MainCamera;
    [SerializeField]
    GameObject _paricalBg;

    [Header("Date Color Brush Sticker")]
    [SerializeField]
    ListToolsAndUIElements _listToolsAndUIElements;
    [Header("Tools")]
    [SerializeField]
    Transform _conToolsBrush;
    [SerializeField]
    Transform _conToolsSticker;
    [SerializeField]
    Transform _conToolsColor;
    [SerializeField]
    GameObject _prefToolsSticker;
    [SerializeField]
    GameObject _prefToolsBrush;
    [SerializeField]
    GameObject _prefToolsColor;
    [Header("UI Elements")]
    [SerializeField]
    GameObject _prefColor;
    [SerializeField]
    Transform _conColor;
    [SerializeField]
    Transform _conSticker;
    [SerializeField]
    GameObject _prefBrush;
    [SerializeField]
    Transform _conBrush;


    public void GameStart()
    {
        Instatiate_Tools(_conToolsBrush, _prefToolsBrush, _listToolsAndUIElements, GameManagetDate.instance.GetData(), "brush");
        Instatiate_Tools(_conToolsColor, _prefToolsColor, _listToolsAndUIElements, GameManagetDate.instance.GetData(), "color");
        Instatiate_Tools(_conToolsSticker, _prefToolsSticker, _listToolsAndUIElements, GameManagetDate.instance.GetData(), "sticker");
        _paricalBg.SetActive(false);
        MainCamera.SetActive(true);
    }

    void Instatiate_Tools(Transform container, GameObject prefab, ListToolsAndUIElements listToolsAndUIElements, DateGame dateBrushAndSticker, string nameTools)
    {
        switch (nameTools)
        {
            case "brush":
                foreach (GameObject go in dateBrushAndSticker._listBrush)
                {
                    GameObject clone = Instantiate(prefab, container);
                    clone.name = $"Tools_{go.name}";
                    Texture2D texture2D = go.GetComponent<Image>().sprite.texture;
                    clone.GetComponent<P3dPaintDecal>().Texture = texture2D;
                    listToolsAndUIElements._listBrushTools.Add(clone);
                }
                UI_Manager.instance.Add_Ui_Game("brushUI", _prefBrush, _conBrush, GameManagetDate.instance.GetData(), _listToolsAndUIElements);
                break;
            case "sticker":
                foreach (GameObject go in dateBrushAndSticker._listSticker)
                {
                    GameObject clone = Instantiate(prefab, container);
                    clone.name = $"Tools_{go.name}";
                    Texture2D texture2D = go.GetComponent<Image>().sprite.texture;
                    clone.GetComponent<P3dPaintDecal>().Texture = texture2D;
                    listToolsAndUIElements._listStickerTools.Add(clone);
                }
                UI_Manager.instance.Add_Ui_Game("stickerUI", null, _conSticker,GameManagetDate.instance.GetData(), _listToolsAndUIElements);
                break;
            case "color":
                foreach (ColorDate go in dateBrushAndSticker._listColor)
                {
                    GameObject clone = Instantiate(prefab, container);
                    clone.name = $"Tools_{go.name}";
                    clone.GetComponent<P3dPaintSphere>().Color = new Color32((byte)go.r, (byte)go.g, (byte)go.b, 255);
                    listToolsAndUIElements._listColorTools.Add(clone);
                }
                UI_Manager.instance.Add_Ui_Game("colorUI",_prefColor, _conColor, GameManagetDate.instance.GetData(), _listToolsAndUIElements);
                break;
        }
    }
}