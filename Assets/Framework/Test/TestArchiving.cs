using Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
class TestSave
{
    public string name;
    public List<int> list;
    public Dictionary<int, string> dic;
}

public class TestArchiving : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        TestSave testSave = new TestSave { name = "james",
                                           list = new List<int>{ 1,2,3},
            dic = new Dictionary<int, string>() {
                                               {1,"one" },
                                               {2,"two" }
                                           }
        };
        ArchivingManager.Instance.SaveFile(testSave,Application.persistentDataPath + "/存储测试文件");
        TestSave testSave2 = ArchivingManager.Instance.LoadFile<TestSave>(Application.persistentDataPath + "/存储测试文件");
        print("over");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
