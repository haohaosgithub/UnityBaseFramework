using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Framework;


public class Test : MonoBehaviour
{
    public GameObject cube;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            PoolManager.Instance.GetGameObject(cube);
        }
        //if (Input.GetKeyDown(KeyCode.D))
        //{
        //    PoolManager.Instance.PushGameObj(cube);
        //}

    }


}
