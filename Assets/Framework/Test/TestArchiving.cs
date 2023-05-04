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
        //TestSave testSave = new TestSave { name = "james",
        //                                   list = new List<int>{ 1,2,3},
        //    dic = new Dictionary<int, string>() {
        //                                       {1,"one" },
        //                                       {2,"two" }
        //                                   }
        //};
        //ArchivingManager.Instance.SaveFile(testSave,Application.persistentDataPath + "/存储测试文件");
        //TestSave testSave2 = ArchivingManager.Instance.LoadFile<TestSave>(Application.persistentDataPath + "/存储测试文件");
        //print("over");
        //ArchivingManager.Instance.SaveArchiving(testSave);
        //ArchivingManager.Instance.SaveArchiving(testSave,3,"newTestSave");
        //TestSave testSave2 = ArchivingManager.Instance.LoadArchiving<TestSave>();
        //ArchivingItem item = ArchivingManager.Instance.CreateArchivingItem();
        //ArchivingManager.Instance.SaveArchiving(testSave,item);
        //print("save over");
        //ArchivingManager.Instance.DeleteArchivingItem(item);
        for(int i = 0;i < 10;i++)
        {
            TestSave testSave = new TestSave
            {
                name = "james",
                list = new List<int> { 1, 2, 3 },
                dic = new Dictionary<int, string>() {
                                               {1,"one" },
                                               {2,"two" }
                                           }
            };
            ArchivingItem item = ArchivingManager.Instance.CreateArchivingItem();
            testSave.name = testSave.name + i;
            ArchivingManager.Instance.SaveArchiving(testSave,item);
        }
        //List<ArchivingItem> list = ArchivingManager.Instance.GetArchivingListByUpdateTime();
        ArchivingItem item2 = ArchivingManager.Instance.CreateArchivingItem();
        TestSave testSave2 = new TestSave
        {
            name = "zzzzsdfasfasdasfsfasd",
            list = new List<int> { 1, 2, 3 },
            dic = new Dictionary<int, string>() {
                                               {1,"one" },
                                               {2,"two" }
                                           }
        };
        ArchivingManager.Instance.SaveArchiving(testSave2, item2);
        TestSave testSave3 = new TestSave
        {
            name = "",
            list = new List<int> { 1, 2, 3 },
            dic = new Dictionary<int, string>() {
                                               {1,"one" },
                                               {2,"two" }
                                           }
        };
        item2 = ArchivingManager.Instance.CreateArchivingItem();
        
        ArchivingManager.Instance.SaveArchiving(testSave3, item2);

        List<ArchivingItem> list2 = ArchivingManager.Instance.GetArchivingListByCondition<string>(Condition);
        
        foreach(var saveitem in list2)
        {
            print(saveitem.archivingId);
            TestSave test = ArchivingManager.Instance.LoadArchiving<TestSave>(saveitem);
            print(test.name);
        }

        print("查看list2");


    }

    public string Condition(ArchivingItem tempitem)
    {
        TestSave test =  ArchivingManager.Instance.LoadArchiving<TestSave>(tempitem);
        
        //print(test.name);
        return test.name;

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
