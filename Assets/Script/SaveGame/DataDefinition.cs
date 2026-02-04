using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataDefinition : MonoBehaviour
{
    public enum DataOperationType
    {
        OnlyRead,
        ReadWrite,
    }

    public DataOperationType operationType = DataOperationType.ReadWrite;
    public string DateId;

    public void OnValidate()
    {
        if (DateId == string.Empty || operationType == DataOperationType.ReadWrite)
        {
            DateId = System.Guid.NewGuid().ToString();
            operationType = DataOperationType.OnlyRead;
        }


    }
}
