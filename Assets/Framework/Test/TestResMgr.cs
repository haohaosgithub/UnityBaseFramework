using Framework;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class TestResMgr : MonoBehaviour
{
    CubeController cube; //cube对应的脚本组件
    void Start()
    {
        //ResManager.Instance.LoadAsync<Cube>("Test/Cube", Cb);
        //print("start");
    }

    // Update is called once per frame
    void Update()
    {
        //如果按下A且脚本为空
        if (Input.GetKeyDown(KeyCode.A) && cube == null)
        {
            ResManager.Instance.LoadAsync<CubeController>("Test/Cube",Cb);
            
        }
        //如果按下D并且脚本组件为非空
        if (Input.GetKeyDown(KeyCode.D) && cube != null) 
        {
            PoolManager.Instance.PushGameObj(cube.gameObject);
            cube = null;
        }

    }

    void Cb(CubeController cubeController) //异步加载完成后的回调设置脚本组件的值为cubeController
    {
        //print("cb");
        cube = cubeController;
    }
}
