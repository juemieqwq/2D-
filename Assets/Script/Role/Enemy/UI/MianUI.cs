using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class MianUI : MonoBehaviour
{
    //物体刚体
    private Rigidbody2D rigidbody2D;

    void Start()
    {
        rigidbody2D = transform.parent.GetComponentInChildren<Rigidbody2D>();
        Assert.IsNotNull(rigidbody2D, "主UI获取不到属主位置");
    }


    private void LateUpdate()
    {
        transform.position = rigidbody2D.transform.position;
    }
}
