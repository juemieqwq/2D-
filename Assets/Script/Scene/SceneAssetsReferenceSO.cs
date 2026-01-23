using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(menuName = "Scene/AssetsReference", fileName = "SceneAssetsReference")]
public class SceneAssetsReferenceSO : ScriptableObject
{
    public List<SceneAsset> assets;

    private Dictionary<string, SceneAsset> DicSceneAssetsReference;

    public AssetReference GetSceneAssetReference(string key)
    {
        InitDic();
        if (DicSceneAssetsReference.TryGetValue(key, out var Scene))
        {
            return Scene.sceneAssetReference;
        }
        else
        {
            Debug.LogError("场景引用获取错误");
            return null;
        }
    }

    private void InitDic()
    {
        if (DicSceneAssetsReference == null)
        {
            DicSceneAssetsReference = new Dictionary<string, SceneAsset>();
            foreach (var asset in assets)
            {
                if (!DicSceneAssetsReference.ContainsKey(asset.key))
                {
                    DicSceneAssetsReference.Add(asset.key, asset);
                }
            }
        }
    }
}

[System.Serializable]
public class SceneAsset
{
    public string key;
    public AssetReference sceneAssetReference;
}
