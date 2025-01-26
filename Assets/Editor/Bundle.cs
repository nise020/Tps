using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class Bundle : MonoBehaviour
{
    [MenuItem("Assets/AssetsBundle")]

    static void BuildIdAssetsBundle() 
    {
        string dir = "Assets/StreamingAssets";

        if (!Directory.Exists(Application.streamingAssetsPath)) 
        {
            Directory.CreateDirectory(dir);
        }

        BuildPipeline.BuildAssetBundles(dir,
            BuildAssetBundleOptions.None,
            EditorUserBuildSettings.activeBuildTarget);
    }
}
