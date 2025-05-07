using CW.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager instance;
    [Header("ToolsPanelViewer")]
    [Header("Sticker")]    
    [SerializeField]
    Transform _conSticker;
    List<GameObject> _prefabStickerClon;

    [Header("Brush")]    
    [SerializeField]
    Transform _conBrush;
    List<GameObject> _prefabBrushClon;

    [Header("Color")]
    [SerializeField]
    GameObject _prefabColor;
    List<GameObject> _prefabColorClon;
    [SerializeField]
    Transform _conColor;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            Instatiate_UI_Elements("brushbundle");
        if (Input.GetKeyDown(KeyCode.O))
            Instatiate_UI_Elements("stickerbundle");
    }

    public void Instatiate_UI_Elements(string NameDownload)
    {
        switch (NameDownload)
        {
            case "brushbundle":
                _prefabBrushClon = new List<GameObject>();
                foreach (GameObject go in GameManagetDate.instance.GetData()._listBrush)
                {
                    GameObject buf = Instantiate(go, _conBrush);
                    buf.name = go.name;
                    _prefabBrushClon.Add(buf);
                }
                break;
            case "stickerbundle":
                _prefabStickerClon = new List<GameObject>();
                foreach (GameObject go in GameManagetDate.instance.GetData()._listSticker)
                { 
                    GameObject buf = Instantiate(go, _conSticker);
                    buf.name = go.name;
                    _prefabStickerClon.Add(buf);
                }
                break;
            case "color":
                _prefabColorClon = new List<GameObject>();
                foreach (ColorDate colorDateBuf in GameManagetDate.instance.GetData()._listColor)
                {
                    GameObject buf = Instantiate(_prefabColor, _conColor);
                    buf.name = colorDateBuf.name;
                    Color bufColor = new Color32((byte)colorDateBuf.r, (byte)colorDateBuf.g, (byte)colorDateBuf.b, 255);
                    buf.GetComponent<Image>().color = bufColor;
                    buf.GetComponent<CanvasGroup>().alpha = 1;
                    _prefabColorClon.Add(buf);
                }
                break;
        }
    }

    public void ClearUIElements()
    {
        for (int i = 0; i < _prefabColorClon.Count; i++)
        {
            Destroy(_prefabColorClon[i]);
        }
        for (int i = 0; i < _prefabStickerClon.Count; i++)
        {
            Destroy(_prefabStickerClon[i]);
        }
        for (int i = 0; i < _prefabBrushClon.Count; i++)
        {
            Destroy(_prefabBrushClon[i]);
        }
        _prefabColorClon.Clear();
        _prefabStickerClon.Clear();
        _prefabBrushClon.Clear();
    }

    public void Add_Ui_Game(string nameUI,GameObject prefab, Transform container, DateGame dateGame, ListToolsAndUIElements listToolsAndUIElements)
    {
        switch (nameUI)
        {
            case "brushUI":
                foreach (GameObject go in dateGame._listBrush)
                {
                    GameObject clone = Instantiate(prefab, container);
                    clone.name = go.name;
                    clone.GetComponent<Image>().sprite = go.GetComponent<Image>().sprite;
                    clone.GetComponent<CwDemoButton>().enabled = false;
                    listToolsAndUIElements._listBrush.Add(clone);
                }
                break;
            case "stickerUI":
                foreach (GameObject go in dateGame._listSticker)
                {
                    GameObject clone = Instantiate(go, container);
                    listToolsAndUIElements._listSticker.Add(clone);
                }
                break;
            case "colorUI":
                int i = 0;
                foreach (ColorDate go in dateGame._listColor)
                {                    
                    Color bufColor = new Color32((byte)go.r, (byte)go.g, (byte)go.b, 255);
                    GameObject clone = Instantiate(prefab, container);
                    clone.name = go.name;
                    clone.GetComponent<Button>().interactable = true;
                    clone.GetComponent<ItemColor>().Set_IndexAndColor(i, bufColor);                    
                    clone.GetComponent<Image>().color = bufColor;
                    listToolsAndUIElements._listColor.Add(clone);
                    i++;
                }
                break;
        }
    }
}