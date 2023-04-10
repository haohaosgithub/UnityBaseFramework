using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
    /// <summary>
    /// 对象池管理器，给外部提供对对象池的对象获取和放入的功能 
    /// 目的是为了减少频繁实例化和销毁造成的消耗
    /// </summary>
    public class PoolManager : ManagerBase<PoolManager>
    {
        Dictionary<string, PoolGameObjectQueue> poolDic;
        GameObject poolRoot;
        public override void Init()
        {
            base.Init();
            poolDic = new Dictionary<string, PoolGameObjectQueue>();
            poolRoot = new GameObject("poolRoot");
            //GameObject GameRoot = GameObject.Find("GameRoot");
            MonoBehaviour gameRoot = GameRoot.Instance;
            poolRoot.transform.SetParent(gameRoot.transform);
            print("pool manager 初始化成功");
        }

        /// <summary>
        /// 获取一个GameObject对象
        /// </summary>
        /// <param name="prefab">预制体:当对象池没有对应对象时用该预制体实例化对象，当对象池存在对应对象时用预制体名称从对象池中取</param>
        /// <returns></returns>
        public GameObject GetGameObject(GameObject prefab)
        {
            GameObject gameObj;
            if (IsGameObjInPool(prefab))
            {
                gameObj = poolDic[prefab.name].GetObj();

            }
            else
            {
                gameObj = Instantiate(prefab);
                gameObj.SetActive(true);
                gameObj.name = prefab.name;
            }
            return gameObj;
        }

        /// <summary>
        /// 将一个GameObject对象放入到对象池中
        /// </summary>
        /// <param name="gameObj"></param>
        public bool PushGameObj(GameObject gameObj)
        {
            if (poolDic.ContainsKey(gameObj.name))
            {
                return poolDic[gameObj.name].PushObj(gameObj);
            }
            else
            {
                CreatePoolList(gameObj);
                //初始化一个对象池队列
                //PoolGameObjectQueue poolQueue = new PoolGameObjectQueue(gameObj, poolRoot);
                //poolQueue.PushObj(gameObj);
                //poolDic.Add(gameObj.name,poolQueue);

                return true;
            }
        }

        /// <summary>
        /// 清空gameObj所在的缓存池队列
        /// </summary>
        /// <param name="gameObj 要销毁的gameObj队列"></param>
        public void Clear(GameObject gameObj) 
        {
            if(poolDic.ContainsKey(gameObj.name))
            {
                poolDic[gameObj.name].Clear();
            }
        }

        /// <summary>
        /// 创建对象池队列（若已经存在则为修改已存在对象池队列的设置 如容量）
        /// </summary>
        /// <param name="prefab"></param>
        /// <param name="capacity"></param>
        /// <param name="prePushSize"></param>
        public void CreatePoolList(GameObject prefab,int capacity= -1,int prePushSize= 0)
        {
            if(prePushSize > capacity && capacity != -1)
            {
                throw new Exception("对象池设置默认容量超出限制");
            }
            
            //如果不存在prefab的对象池队列
            if (!poolDic.ContainsKey(prefab.name))
            {
                PoolGameObjectQueue poolQueue = new PoolGameObjectQueue(prefab, poolRoot,capacity);
                poolQueue.capacity = capacity;
                poolDic.Add(prefab.name, poolQueue);
                if(prePushSize > 0)
                {
                    for(int i = 0; i < prePushSize;i++) //实例化默认数量的对象，并且加入对象队列
                    {
                        GameObject go = Instantiate(prefab);
                        go.name = prefab.name;
                        poolQueue.PushObj(go);
                    }
                }
            }
            else 
            {
                PoolGameObjectQueue poolQueue = poolDic[prefab.name];
                poolQueue.capacity = capacity;
                if(prePushSize > 0)
                {
                    for (int i = 0; i < prePushSize - poolQueue.poolValueQueue.Count; i++) //实例化默认数量的对象，并且加入对象队列
                    {
                        GameObject go = Instantiate(prefab);
                        go.name = prefab.name;
                        poolQueue.PushObj(go);
                    }
                }
            }

        }
        /// <summary>
        /// 对象列表在池子中且存在该列表存在对象
        /// </summary>
        /// <param name="gameObj"></param>
        /// <returns></returns>
        private bool IsGameObjInPool(GameObject gameObj)
        {
            if (poolDic.ContainsKey(gameObj.name) && poolDic[gameObj.name].poolValueQueue.Count > 0)
                return true;
            return false;
        }
        
         
    }

}
