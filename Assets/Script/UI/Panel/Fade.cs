using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    private Image image;
    private bool isFadeComplete;
    private bool isLoadSceneComplete;

    private void Awake()
    {
        image = GetComponentInChildren<Image>();
        image.color = Color.clear;
    }

    public void IsFadeIn(float duration, bool isFadeIn)
    {

        if (isFadeIn)
        {
            isLoadSceneComplete = false;
            isFadeComplete = false;
            image.raycastTarget = true;
            image.DOColor(Color.black, duration).OnComplete(() =>
            {
                isFadeComplete = true;
                if (isLoadSceneComplete)
                    image.DOBlendableColor(Color.clear, duration).OnComplete(() => { image.raycastTarget = false; });

            });
        }
        else
        {
            if (isFadeComplete)
                image.DOBlendableColor(Color.clear, duration).OnComplete(() => { image.raycastTarget = false; });
            else
                isLoadSceneComplete = true;
        }

    }
}
