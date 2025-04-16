using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class DonwloadAssetBundlesSimplifWay : MonoBehaviour
{
    [SerializeField]
    Transform _conBrush;
    [SerializeField]
    Transform _conSticker;
    [SerializeField]
    Transform _conColor;

    private void Start()
    {
        StartCoroutine(LoadBundle("/brushbundle", _conBrush));
        StartCoroutine(LoadBundle("/stickerbundle", _conSticker));
    }

    IEnumerator LoadBundle(string bundelName, Transform container)
    {
        string path = Path.Combine(Application.streamingAssetsPath + bundelName);
        AssetBundleCreateRequest BundleLoad = AssetBundle.LoadFromFileAsync(path);
        yield return BundleLoad;
        AssetBundle bundle = BundleLoad.assetBundle;      
        InstatiateFromAssetBudle(bundle, container);
    }

    void InstatiateFromAssetBudle(AssetBundle bundle, Transform container)
    {
        foreach (GameObject go in bundle.LoadAllAssets())
        {
            GameObject clone = Instantiate(go, container);
            clone.name = go.name;
        }  
    }
}
