using System.Collections;
using System.Collections.Generic;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{
    private SceneLoadManager()
    {

    }

    public static SceneLoadManager instance;


    [SerializeField]
    [Header("场景资源的SO")]
    private SceneAssetsReferenceSO sceneReferenceSO;
    private AssetReference currentScene;
    private AssetReference goToScene;

    private Vector3 playerGoToPosition;
    private bool isFade;
    [Header("淡出淡入的UI对象")]
    [SerializeField]
    private Fade fadeClass;
    [Header("淡出淡入的时间")]
    [SerializeField]
    private float fadeTime;
    //场景加载的协程引用
    private Coroutine loadSceneCoroutine;
    private Player player;

    // Start is called before the first frame update
    void Awake()
    {

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

        if (sceneReferenceSO == null)
        {
            sceneReferenceSO = (Resources.LoadAsync("Scense/SceneAssetsReference").asset) as SceneAssetsReferenceSO;
            if (sceneReferenceSO == null)
                Debug.LogError("场景引用赋值失败");

        }
        currentScene = sceneReferenceSO.GetSceneAssetReference("Cave");
        currentScene.LoadSceneAsync(LoadSceneMode.Additive, true);
    }

    private void Start()
    {
        player = PlayerManager.instance.player;
    }


    public void LoadNewScene(AssetReference newScene, Vector3 playerGoToPosition, bool isFade = true)
    {
        goToScene = newScene;
        this.playerGoToPosition = playerGoToPosition;
        this.isFade = isFade;
        if (loadSceneCoroutine == null && goToScene != null)
            loadSceneCoroutine = StartCoroutine(IELoadNewScene());
    }

    private IEnumerator IELoadNewScene()
    {
        var waitFadeTime = new WaitForSeconds(fadeTime);
        if (isFade)
        {

            player.SetIsInput(false);
            player.SetInputX(0);
            fadeClass.IsFadeIn(fadeTime, true);
            yield return waitFadeTime;
        }

        //等待新场景加载
        yield return goToScene.LoadSceneAsync(LoadSceneMode.Additive, true);

        //等待上一个场景卸载
        yield return currentScene.UnLoadScene();
        //将已卸载的空场景引用改为跳转的场景
        currentScene = goToScene;
        goToScene = null;
        PlayerManager.instance.player.transform.position = playerGoToPosition;
        loadSceneCoroutine = null;
        waitFadeTime = new WaitForSeconds(fadeTime * .5f);
        yield return waitFadeTime;
        if (isFade)
        {
            fadeClass.IsFadeIn(fadeTime, false);
            yield return waitFadeTime;
            player.SetIsInput(true);
        }

    }
}
