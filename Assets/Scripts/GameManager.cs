using CW.Common;
using DG.Tweening;
using PaintIn3D;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField]
    ScaleSticker scaleSticker;

    [SerializeField]
    ButtonRotate buttonRotate;

    [SerializeField]
    GameObject MainCamera;
    [SerializeField]
    GameObject _paricalBg;
    [SerializeField]
    RawImage _rawBg;

    [SerializeField]
    List<DateToolsAndUiElements> _dateTools_Uielements = new List<DateToolsAndUiElements>();

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

    [Header("Models")]
    [SerializeField]
    GameObject _prefabModels;
    [SerializeField]
    Transform parentModels;
    [SerializeField]
    GameObject _clonePrefab;
    [SerializeField]
    Transform _posModel;    


    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void CloseApp()
    {
        Application.Quit();
    }

    public void GameStart()
    {
        SpawnModels();
        Instatiate_Tools_UI(_conToolsBrush, _prefToolsBrush, GameManagetDate.instance.GetData(), "brush");
        Instatiate_Tools_UI(_conToolsColor, _prefToolsColor, GameManagetDate.instance.GetData(), "color");
        Instatiate_Tools_UI(_conToolsSticker, _prefToolsSticker, GameManagetDate.instance.GetData(), "sticker");
        _rawBg.gameObject.SetActive(false);
       // _paricalBg.SetActive(false);
       // MainCamera.SetActive(true);
    }

    public void CloseGame()
    {
        //MainCamera.SetActive(false);
        //_paricalBg.SetActive(true);
        _rawBg.gameObject.SetActive(true);
        ClearToolsAndUI();
        UI_Manager.instance.ClosePopapScale();
    }

    void Instatiate_Tools_UI(Transform container, GameObject prefab, DateGame dateBrushAndSticker, string nameTools)
    {
        switch (nameTools)
        {
            case "brush":
                foreach (GameObject go in dateBrushAndSticker._listBrush)
                {
                    GameObject cloneTools = Instantiate(prefab, container);
                    cloneTools.name = $"{go.name}";
                    Texture2D texture2D = go.GetComponent<Image>().sprite.texture;
                    cloneTools.GetComponent<P3dPaintDecal>().Texture = texture2D;

                    GameObject cloneUI = Instantiate(_prefBrush, _conBrush);
                    cloneUI.name = $"{go.name}";
                    cloneUI.AddComponent<Button>();

                    cloneUI.GetComponent<Button>().onClick.AddListener(() =>
                    {
                        CloseSticker();
                        UI_Manager.instance.ClosePopapScale();
                    });

                    cloneUI.GetComponent<Image>().sprite = go.GetComponent<Image>().sprite;
                    cloneUI.GetComponent<CwDemoButton>().IsolateTarget = cloneTools.transform;

                    DateToolsAndUiElements buf = new DateToolsAndUiElements("Brush", false, cloneTools, cloneUI);
                    _dateTools_Uielements.Add(buf);
                }
                break;
            case "sticker":
                foreach (GameObject go in dateBrushAndSticker._listSticker)
                {
                    GameObject clone = Instantiate(prefab, container);
                    clone.name = $"{go.name}";
                    Texture2D texture2D = go.GetComponent<Image>().sprite.texture;
                    clone.GetComponent<P3dPaintDecal>().Texture = texture2D;
                    clone.GetComponent<P3dPaintDecal>().Scale = new Vector3(3, 3, 4);
                    scaleSticker.SetSize(3);
                    GameObject cloneUI = Instantiate(go, _conSticker);
                    cloneUI.name = $"{go.name}";
                    cloneUI.AddComponent<Button>();

                    cloneUI.GetComponent<Button>().onClick.AddListener(() =>
                    {
                        ColoseColorAndBrush();
                        UI_Manager.instance.OpenPopapScale();
                    });

                    cloneUI.AddComponent<Outline>();
                    cloneUI.AddComponent<CanvasGroup>();
                    cloneUI.AddComponent<CwDemoButton>();
                    cloneUI.GetComponent<CwDemoButton>().Link = CwDemoButton.LinkType.Isolate;
                    cloneUI.GetComponent<CwDemoButton>().IsolateTarget = clone.transform;

                    DateToolsAndUiElements buf = new DateToolsAndUiElements("Sticker", false, clone, cloneUI);
                    _dateTools_Uielements.Add(buf);
                }
                break;
            case "color":
                int i = 0;
                foreach (ColorDate go in dateBrushAndSticker._listColor)
                {
                    GameObject clone = Instantiate(prefab, container);
                    clone.GetComponent<P3dPaintSphere>().Color = DonwloadAssetBundlesSimplifWay.FromHex(go.hex);

                    Color bufColor = DonwloadAssetBundlesSimplifWay.FromHex(go.hex);
                    GameObject cloneUI = Instantiate(_prefColor, _conColor);
                    cloneUI.GetComponent<Button>().interactable = true;
                    cloneUI.GetComponent<ItemColor>().Set_IndexAndColor(i, bufColor);
                    cloneUI.GetComponent<Image>().color = bufColor;
                    i++;

                    DateToolsAndUiElements buf = new DateToolsAndUiElements("Color", false, clone, cloneUI);
                    _dateTools_Uielements.Add(buf);
                }
                break;
        }
    }

    public void SwitchColor(Color col)
    {
        for (int i = 0; i < _dateTools_Uielements.Count; i++)
        {
            if (_dateTools_Uielements[i].name == "Brush")
            {
                _dateTools_Uielements[i].tools.GetComponent<P3dPaintDecal>().Color = col;
            }
        }
    }

    void ColoseColorAndBrush()
    {
        for (int i = 0; i < _dateTools_Uielements.Count; i++)
        {
            if (_dateTools_Uielements[i].name == "Brush")
            {
                _dateTools_Uielements[i].tools.gameObject.SetActive(false);
                _dateTools_Uielements[i].uiElements.GetComponent<CanvasGroup>().alpha = 0.5f;
            }
            if (_dateTools_Uielements[i].name == "Color")
            {
                _dateTools_Uielements[i].tools.gameObject.SetActive(false);
                _dateTools_Uielements[i].uiElements.GetComponent<CanvasGroup>().alpha = 0.5f;
            }
        }
    }

    public void CloseSticker()
    {
        for (int i = 0; i < _dateTools_Uielements.Count; i++)
        {
            if (_dateTools_Uielements[i].name == "Sticker")
            {
                _dateTools_Uielements[i].tools.gameObject.SetActive(false);
                _dateTools_Uielements[i].uiElements.GetComponent<CanvasGroup>().alpha = 0.5f;
            }
        }
    }

    public void ClearToolsAndUI()
    {
        for (int i = 0; i < _dateTools_Uielements.Count; i++)
        {
            Destroy(_dateTools_Uielements[i].uiElements);
            Destroy(_dateTools_Uielements[i].tools);
        }
        _dateTools_Uielements.Clear();
    }

    public void SpawnModels()
    {
        GameObject buf = _clonePrefab;
        _clonePrefab = Instantiate(_prefabModels, parentModels);
        _clonePrefab.transform.position = _posModel.position;
        buttonRotate.ResetY();
        parentModels.DORotate(new Vector3(-90, 0, 0), 0.1f);
        Destroy(buf);
    }

    public void HideCollorButton(GameObject item, int itemIndex)
    {
        for (int i = 0; i < _dateTools_Uielements.Count; i++)
        {
            if (_dateTools_Uielements[i].name == "Color")
            {
                if (_dateTools_Uielements[i].uiElements.GetComponent<ItemColor>().index != itemIndex)
                {
                    _dateTools_Uielements[i].uiElements.GetComponent<CanvasGroup>().alpha = 0.5f;
                }
                else
                {
                    _dateTools_Uielements[i].uiElements.GetComponent<CanvasGroup>().alpha = 1;
                }
            }
        }
    }

    public void ScaleSticker()
    {
        foreach (DateToolsAndUiElements date in _dateTools_Uielements)
        {
            if (date.name == "Sticker")
            {
                date.tools.GetComponent<P3dPaintDecal>().Scale = new Vector3(scaleSticker.GetSize(), scaleSticker.GetSize(), scaleSticker.GetSize() + 1);
            }
        }
    }
}