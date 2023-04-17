using Framework;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class TestMono
{
    public TestMono()
    {
        MonoManager.Instance.AddUpdateListener(OnUpdate);
        MonoManager.Instance.StartCoroutine(DoEverySecond());
    }

    void OnUpdate()
    {
        Debug.Log("testmono update");
    }

    public IEnumerator DoEverySecond()
    {
        while (true)
        {
            Debug.Log("testmono coroutine");
            yield return new WaitForSeconds(1);
        }
    }
}

public class TestMonoMgr : MonoBehaviour
{
    private void Start()
    {
        TestMono t = new TestMono();
    }

}

