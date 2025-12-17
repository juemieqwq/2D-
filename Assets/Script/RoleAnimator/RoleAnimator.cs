using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleAnimator : MonoBehaviour
{
    public List<GameObject> Containers;

    public Dictionary<string, List<GameObject>> DicPlayImagesGameObjects;

    public void Init()
    {
        foreach (var Contanier in Containers)
        {
            var gameObjects = Contanier.GetComponentsInChildren<GameObject>();
            List<GameObject> PlayImagesGameObjects = new List<GameObject>();
            foreach (var gameObject in gameObjects)
            {
                gameObject.SetActive(false);
                PlayImagesGameObjects.Add(gameObject);
            }
            if (!DicPlayImagesGameObjects.ContainsKey(Contanier.name))
            {
                DicPlayImagesGameObjects.Add(Contanier.name, PlayImagesGameObjects);
            }
        }
    }
}
