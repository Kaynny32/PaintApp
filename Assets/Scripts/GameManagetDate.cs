using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class GameManagetDate : MonoBehaviour
{
    public static GameManagetDate instance;

    [SerializeField]
    DonwloadAssetBundlesSimplifWay donwloadAssetBundlesSimplifWay;
    [SerializeField]
    DateGame dateBrushAndSticker;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    private void Start()
    {
        AssignDate("brushbundle");
        AssignDate("stickerbundle");
        AssignDate("color");
    }

    public void SetDateBrush(GameObject goBrush)
    {
        dateBrushAndSticker._listBrush.Add(goBrush);
    }

    public void SetDateSticker(GameObject goSticker)
    {
        dateBrushAndSticker._listSticker.Add(goSticker);
    }

    public void SetColorDate(ColorDate colorDate)
    {
        dateBrushAndSticker._listColor.Add(colorDate);
    }

    public DateGame GetData()
    {
        return dateBrushAndSticker;
    }

    public void AssignDate(string NameDownload)
    {
        switch (NameDownload)
        {
            case "brushbundle":
                donwloadAssetBundlesSimplifWay.StartCorutineLoad("/brushbundle");
                break;
            case "stickerbundle":
                donwloadAssetBundlesSimplifWay.StartCorutineLoad("/stickerbundle");
                break;
            case "color":
                donwloadAssetBundlesSimplifWay.ReadJsonFiel();
                break;
        }
    }

    public void ClearDate(string NameDownload)
    {
        switch (NameDownload)
        {
            case "brushbundle":
                dateBrushAndSticker._listBrush.Clear();
                break;
            case "stickerbundle":
                dateBrushAndSticker._listSticker.Clear();
                break;
            case "color":
                dateBrushAndSticker._listColor.Clear();
                break;
        }
    }
}
