using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static BehaviorContainer;

public class RoleAnimator : MonoBehaviour
{
    [Header("一秒播放多少帧，应用与本角色的所有动画播放")]
    public int playFrame;
    public List<GameObject> containers = new List<GameObject>();
    public Dictionary<string, List<GameObject>> DicPlayImagesGameObjects;
    public List<GameObject> currentPlayBehavior;
    private bool isInit = false;
    private Coroutine currentCoroutine;
    private bool isFinshedPlay = false;
    public void Init()
    {
        DicPlayImagesGameObjects = new Dictionary<string, List<GameObject>>();
        if (containers == null || containers.Count == 0)
        {
            isInit = false;
            Debug.LogError("容器存储列表为赋值,初始化失败");
            return;
        }
        foreach (var Contanier in containers)
        {
            var ContanierClass = Contanier.GetComponent<BehaviorContainer>();
            List<GameObject> PlayImagesGameObjects = new List<GameObject>();
            PlayImagesGameObjects = ContanierClass.GetPlayGameObjects();
            Contanier.gameObject.SetActive(true);
            var KeyName = ContanierClass.roleBehaviorName.ToString();
            if (!DicPlayImagesGameObjects.ContainsKey(KeyName))
            {
                DicPlayImagesGameObjects.Add(KeyName, PlayImagesGameObjects);
                Debug.Log(PlayImagesGameObjects + "已存入");
            }
            else
                Debug.Log("字典已存在Key：" + KeyName);
        }
        isInit = true;
    }

    public void Start()
    {
        if (containers != null)
        {
            Init();
            PlayRoleBehavior(RoleBehavior.Idle, true);
            Debug.Log("初始化状态为：" + currentPlayBehavior);
        }
        else
            Debug.Log("获取的容器为空");
    }

    public void PlayRoleBehavior(RoleBehavior BehaviorName, bool isLoop = true)
    {
        //如果未成功初始化，在播放时再次尝试初始化
        if (!isInit)
            Init();

        isFinshedPlay = true;
        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine);
        SetCurrentBehaviorAtive();
        string Key = BehaviorName.ToString();
        if (DicPlayImagesGameObjects.TryGetValue(Key, out currentPlayBehavior))
        {
            isFinshedPlay = false;
            currentCoroutine = StartCoroutine(Play(isLoop));
        }
        else
        {
            Debug.Log("CurrentPlayBehavior:" + currentPlayBehavior);
        }


    }

    private IEnumerator Play(bool isLoop = true)
    {
        float time = 0;
        float everyFrame = 0;
        everyFrame = 1f / (float)this.playFrame;
        int Index = 0;
        // 先激活第一帧
        if (currentPlayBehavior != null && currentPlayBehavior.Count > 0)
        {
            SetCurrentBehaviorAtive();
            currentPlayBehavior[0].SetActive(true);
        }
        else
        {
            Debug.LogError("CurrentPlayBehavior is null or empty!");
            yield break;
        }
        while (!isFinshedPlay)
        {
            time += Time.fixedDeltaTime;
            while (time >= everyFrame && !isFinshedPlay)
            {
                time -= everyFrame;
                Index++;
                // 检查是否到达最后一帧
                if (Index >= currentPlayBehavior.Count)
                {
                    if (isLoop)
                    {
                        Index = 0; // 循环回到第一帧
                    }
                    else
                    {
                        // 非循环播放，停留在最后一帧
                        Index = currentPlayBehavior.Count - 1;
                        isFinshedPlay = true;
                        yield break;
                    }
                }
                if (!isFinshedPlay)
                {
                    for (int i = 0; i < currentPlayBehavior.Count; i++)
                    {
                        currentPlayBehavior[i].SetActive(i == Index);
                    }
                }

            }
            yield return new WaitForFixedUpdate();
        }
    }

    public void SetCurrentBehaviorAtive(bool isAtive = false)
    {
        foreach (var frame in currentPlayBehavior)
        {
            frame.SetActive(isAtive);
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown("p"))
        {

            PlayRoleBehavior(RoleBehavior.Idle, true);
        }
        else if (Input.GetKeyDown("o"))
        {

            PlayRoleBehavior(RoleBehavior.Run, true);
        }
        if (Input.GetKeyDown("s"))
        {
            isFinshedPlay = true;
            StopCoroutine(currentCoroutine);
        }
    }
}
