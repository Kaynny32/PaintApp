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
        StartCoroutine(LoadBundleBrush("/brushbundle"));
    }

    IEnumerator LoadBundleBrush(string bundelName)
    {
        string path = Path.Combine(Application.streamingAssetsPath + bundelName);
        AssetBundleCreateRequest BundleLoad = AssetBundle.LoadFromFileAsync(path);
        yield return BundleLoad;
        AssetBundle bundle = BundleLoad.assetBundle;      
        InstatiateFromAssetBudle(bundle, _conBrush);
    }

    void InstatiateFromAssetBudle(AssetBundle bundle, Transform conteiner)
    {
        foreach (GameObject go in bundle.LoadAllAssets())
        {
            GameObject clone = Instantiate(go, conteiner);
            clone.name = go.name;
        }  
    }
}
