using DG.Tweening;
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

    [Header("Callibration")]
    [SerializeField]
    CanvasGroup _callibrationPanel;

    [Header("Scale Sticker Popap")]
    [SerializeField]
    RectTransform _conScaleStickerPopap;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            OpenPopapScale();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            ClosePopapScale();
        }
    }

    private void Start()
    {
       // _callibrationPanel.GetComponent<AnimUI>().ShowUI();
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
                    Color bufColor = DonwloadAssetBundlesSimplifWay.FromHex(colorDateBuf.hex);
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

    public void ResetUIColor()
    {
        for (int i = 0; i< _prefabColorClon.Count; i++)
        { 
            Destroy( _prefabColorClon[i]);
        }
    }

    public void OpenPopapScale()
    {
        _conScaleStickerPopap.DOKill();
        _conScaleStickerPopap.GetComponent<CanvasGroup>().DOFade(1, 0.5f).SetEase(Ease.InQuad);
        _conScaleStickerPopap.GetComponent<CanvasGroup>().interactable = true;
        _conScaleStickerPopap.GetComponent<CanvasGroup>().blocksRaycasts = true;
        _conScaleStickerPopap.DOAnchorPosY(-120, 0.5f).SetEase(Ease.InQuad);
    }

    public void ClosePopapScale()
    {
        _conScaleStickerPopap.DOKill();
        _conScaleStickerPopap.DOAnchorPosY(0, 0.5f).SetEase(Ease.InQuad);
        _conScaleStickerPopap.GetComponent<CanvasGroup>().DOFade(0, 0.5f).SetEase(Ease.InQuad);
        _conScaleStickerPopap.GetComponent<CanvasGroup>().interactable = false;
        _conScaleStickerPopap.GetComponent<CanvasGroup>().blocksRaycasts = false;        
    }
}