using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class SavebleGameObjectDate
{
    public Dictionary<string, SaveInfo> SaveDateInfoDict = new Dictionary<string, SaveInfo>();
    public string saveSceneKey;

    public void SaveSceneDate(string SceneKey)
    {
        saveSceneKey = SceneKey;
    }

    public string LoadSceneDate()
    {
        return saveSceneKey;
    }
}

[System.Serializable]
public class SaveInfo
{
    public float posX;
    public float posY;
    public float posZ;
    public float health;



    public void SavePosDate(float x, float y, float z)
    {
        posX = x;
        posY = y;
        posZ = z;
    }
    public Vector3 GetSavePosDate()
    {
        return new Vector3(posX, posY, posZ);
    }

}

