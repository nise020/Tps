using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class Bundle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ILoad());
    }

    IEnumerator ILoad()
    {
        AssetBundleCreateRequest asy =
            AssetBundle.LoadFromFileAsync(Path.Combine
            (Application.streamingAssetsPath, "fab_granaid"));//"" <- Path(name)

        yield return asy;

        AssetBundle local = asy.assetBundle;

        if(local == null)
            yield break;

        AssetBundleRequest asset = local.LoadAssetAsync<GameObject>("fab_granaid");

        yield return asset;

        var prefab = asset.asset as GameObject;

        local.Unload(true);

    }
}
