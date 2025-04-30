using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class DonwloadAssetBundlesSimplifWay : MonoBehaviour
{
    public void StartCorutineLoad(string bundelName)
    {
        StartCoroutine(LoadBundle(bundelName));
    }

    IEnumerator LoadBundle(string bundelName)
    {
        string path = Path.Combine(Application.streamingAssetsPath + bundelName);
        AssetBundleCreateRequest BundleLoad = AssetBundle.LoadFromFileAsync(path);
        yield return BundleLoad;
        AssetBundle bundle = BundleLoad.assetBundle;
        foreach (GameObject go in bundle.LoadAllAssets())
        {
            if (bundelName == "/brushbundle")
            {
                GameManagetDate.instance.SetDateBrush(go);
            }
            else
            {
                GameManagetDate.instance.SetDateSticker(go);
            }
        }
        bundle.Unload(false);
    }


    public void ReadJsonFiel()
    {
        if (File.Exists("Color.json"))
        {
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
                GameManagetDate.instance.SetColorDate(colorDate);
            }
        }
        else
        {
            DefaultColor();
        }
    }

    void DefaultColor()
    {
        ColorDate colorDateBlack = new ColorDate("black", 0, 0, 0);
        GameManagetDate.instance.SetColorDate(colorDateBlack);

        ColorDate colorDateWhite = new ColorDate("white", 255, 255, 255);
        GameManagetDate.instance.SetColorDate(colorDateWhite);

        ColorDate colorDateYellow = new ColorDate("yellow", 255, 220, 0);
        GameManagetDate.instance.SetColorDate(colorDateYellow);

        ColorDate colorDateRed = new ColorDate("red", 255, 27, 0);
        GameManagetDate.instance.SetColorDate(colorDateRed);

        ColorDate colorDateGreen = new ColorDate("green", 7, 255, 0);
        GameManagetDate.instance.SetColorDate(colorDateGreen);
    }
}