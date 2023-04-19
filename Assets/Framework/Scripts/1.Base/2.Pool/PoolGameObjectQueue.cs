using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Framework
{
    /// <summary>
    /// GameObject对象池的具体数据队列，仅供poolManager调用而不供外部调用
    /// </summary>
    public class PoolGameObjectQueue
    {
        public Queue<GameObject> poolValueQueue;
        public int capacity; // -1代表无限制

        public GameObject fatherObj;
        public PoolGameObjectQueue(GameObject obj, GameObject poolRoot,int capacity = -1)
        {
            poolValueQueue = new Queue<GameObject>();
            this.capacity = capacity;
            fatherObj = new GameObject();
            fatherObj.name = obj.name;
            fatherObj.transform.SetParent(poolRoot.transform);
            //PushObj(obj);
        }

        public GameObject GetObj(Transform parent = null)
        {

            GameObject gameObj = poolValueQueue.Dequeue();

            gameObj.SetActive(true);
            gameObj.transform.SetParent(parent);
            
            if(parent == null)
            {
                SceneManager.MoveGameObjectToScene(gameObj, SceneManager.GetActiveScene());
            }

            //Debug.Log("parent" + gameObj.transform.parent.name);
            return gameObj;
        }

        public bool PushObj(GameObject obj)
        {
            if(capacity != -1 && poolValueQueue.Count >= capacity)
            {
                Object.Destroy(obj);
                return false;
            }
            
            poolValueQueue.Enqueue(obj);

            obj.SetActive(false);
            obj.transform.SetParent(fatherObj.transform);
            return true;
        }

        public void Clear()
        {
            Object.Destroy(fatherObj);
            fatherObj = null;
            poolValueQueue.Clear();
        }
    }
}

