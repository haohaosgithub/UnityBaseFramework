using Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEventMgr : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            EventManager.Instance.EventTrigger("A");
            EventManager.Instance.EventTrigger<string>("B","hello");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            EventManager.Instance.Register("A",ACallBack);
            EventManager.Instance.Register<string>("B", BCallBack);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            EventManager.Instance.Unregister("A",ACallBack);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            EventManager.Instance.RemoveEvent("A");
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            EventManager.Instance.Clear();
        }
    }

    public void ACallBack()
    {
        print("A callback");
    }

    public void BCallBack(string s)
    {
        print("B callback: " + s);
    }
}
