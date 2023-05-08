using Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTips : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            UIManager.Instance.AddTips("A被按下");
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            UIManager.Instance.AddTips("B被按下");
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            UIManager.Instance.AddTips("C被按下");
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            UIManager.Instance.AddTips("D被按下");
        }
    }
}
