using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMusic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            
            AudioManager.Instance.PlayBGMusic("Test/geqian");
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            AudioManager.Instance.PlayOneShot("Test/cannon_01",Vector3.zero,true,Callback,1); ;
        }
    }

    private void Callback()
    {
        print(" 音效结束");
    }
}
