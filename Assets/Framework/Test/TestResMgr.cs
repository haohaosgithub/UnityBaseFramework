using Framework;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class TestResMgr : MonoBehaviour
{
    Cube cube; //cube对应的脚本组件
    void Start()
    {
        //ResManager.Instance.LoadAsync<Cube>("Test/Cube", Cb);
        //print("start");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && cube == null)
        {
            ResManager.Instance.LoadAsync<Cube>("Test/Cube",Cb);
            
        }
        if (Input.GetKeyDown(KeyCode.D) && cube != null)
        {
            PoolManager.Instance.PushGameObj(cube.gameObject);
            cube = null;
        }

    }

    void Cb(Cube cubeController)
    {
        //print("cb");
        cube = cubeController;
    }
}
