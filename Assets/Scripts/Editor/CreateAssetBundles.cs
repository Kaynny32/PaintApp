using System;
using System.IO;
using UnityEditor;
using UnityEngine;

public class CreateAssetBundles 
{
    [MenuItem("Assets/Create Assets Bundles")]
    private static void BuildAssetBundles()
    {
        string assetBundleDirectoryPath = Path.Combine(Application.streamingAssetsPath);
        Debug.Log(assetBundleDirectoryPath);
        try
        {
            BuildPipeline.BuildAssetBundles(assetBundleDirectoryPath, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);
        }
        catch (Exception e)
        {
            Debug.LogWarning(e);
        }

    }
}
