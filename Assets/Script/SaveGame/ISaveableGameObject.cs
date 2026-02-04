using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface ISaveableGameObject
{
    public DataDefinition GetDateDefinition();
    public void RegisterSaveDate() => DataManager.instance.RegisterSaveDate(this);
    public void UnRegisterSaveDate() => DataManager.instance.UnRegisterSaveDate(this);
    public void SaveDate(ref SavebleGameObjectDate date);
    public void LoadSaveDate(ref SavebleGameObjectDate date);
}
