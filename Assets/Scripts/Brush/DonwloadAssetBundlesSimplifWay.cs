using Newtonsoft.Json.Linq;
using System.Collections;
using System.Globalization;
using System.IO;
using UnityEngine;

public class DonwloadAssetBundlesSimplifWay : MonoBehaviour
{
    public static DonwloadAssetBundlesSimplifWay instance;

    [SerializeField]
    FlexibleColorPicker fcp;
    [SerializeField]
    string _hex;

    string path = Application.streamingAssetsPath;

    private void Awake()
    {
        if (instance == null)
            instance = this;
       
    }

    private void Start()
    {
        
    }

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
        if (File.Exists(path + "/Json/Color.json"))
        {
            StreamReader streamReader = new StreamReader(path + "/Json/Color.json");
            string str = streamReader.ReadToEnd();
            JObject jobj = JObject.Parse(str);
            JArray _jArray = jobj["color"].Value<JArray>();
            foreach (JObject _col in _jArray)
            {
                string _hex = _col["hex"].Value<string>();

                ColorDate colorDate = new ColorDate(_hex);
                GameManagetDate.instance.SetColorDate(colorDate);
            }
            streamReader.Close();
        }
        else
        {
            DefaultColor();
        }
    }

    void DefaultColor()
    {
        ColorDate colorDateBlack = new ColorDate("black");
        GameManagetDate.instance.SetColorDate(colorDateBlack);

        ColorDate colorDateWhite = new ColorDate("white");
        GameManagetDate.instance.SetColorDate(colorDateWhite);

        ColorDate colorDateYellow = new ColorDate("yellow");
        GameManagetDate.instance.SetColorDate(colorDateYellow);

        ColorDate colorDateRed = new ColorDate("red");
        GameManagetDate.instance.SetColorDate(colorDateRed);

        ColorDate colorDateGreen = new ColorDate("green");
        GameManagetDate.instance.SetColorDate(colorDateGreen);
    }

    public static Color FromHex(string hex)
    {
        
        if (hex.Length < 6)
        {
            throw new System.FormatException("Needs a string with a length of at least 6");
        }
        var r = hex.Substring(0, 2);
        var g = hex.Substring(2, 2);
        var b = hex.Substring(4, 2);
        string alpha;
        if (hex.Length >= 8)
            alpha = hex.Substring(6, 2);
        else
            alpha = "FF";

        return new Color((int.Parse(r, NumberStyles.HexNumber) / 255f),
                        (int.Parse(g, NumberStyles.HexNumber) / 255f),
                        (int.Parse(b, NumberStyles.HexNumber) / 255f),
                        (int.Parse(alpha, NumberStyles.HexNumber) / 255f));
    }

    public void ColorSave()
    {
        _hex = ColorUtility.ToHtmlStringRGB(fcp.color);

        if (File.Exists(path + "/Json/Color.json"))
        {
            StreamReader streamReader = new StreamReader(path + "/Json/Color.json");
            string str = streamReader.ReadToEnd();
            JObject jobj = JObject.Parse(str);
            JArray _jArray = jobj["color"].Value<JArray>();

            JObject jobjBuf = new JObject();
            jobjBuf["hex"] = _hex;
            _jArray.Add(jobjBuf);
            JObject jobjttt = new JObject();
            jobjttt["color"] = _jArray;

            streamReader.Close();
            StreamWriter streamWriter = new StreamWriter(path + "/Json/Color.json");
            streamWriter.Write(jobjttt);
            streamWriter.Close();
        }
        GameManagetDate.instance.ResetColorDate();
        ReadJsonFiel();
        UI_Manager.instance.Instatiate_UI_Elements("color");
    }
}