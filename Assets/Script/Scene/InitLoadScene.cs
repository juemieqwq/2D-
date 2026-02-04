using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

public class InitLoadScene : MonoBehaviour
{

    public AssetReference mainScene;
    void Start()
    {
        mainScene.LoadSceneAsync(LoadSceneMode.Additive, true);
    }


}
