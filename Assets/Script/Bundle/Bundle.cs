using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class Bundle : MonoBehaviour
{
    [SerializeField] List<string> bundleName;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ILoad());//Load
    }

    IEnumerator ILoad()
    {
        foreach (var bundle in bundleName) 
        {
            AssetBundleCreateRequest async =
            AssetBundle.LoadFromFileAsync(Path.Combine
            (Application.streamingAssetsPath, $"{bundle}"));//"" <- Path(name)

            yield return async;

            AssetBundle local = async.assetBundle;

            if (local == null)
                yield break;

            AssetBundleRequest asset = local.LoadAssetAsync<GameObject>($"{bundle}");

            yield return asset;

            var prefab = asset.asset as GameObject;

            if (prefab != null) 
            {
                local.Unload(true);
            }
        }
       
    }
}
