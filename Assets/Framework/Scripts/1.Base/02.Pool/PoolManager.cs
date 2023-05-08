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
            //print("pool manager 初始化成功");
        }
        #region 游戏对象相关方法
        /// <summary>
        /// 获取一个GameObject对象
        /// </summary>
        /// <param name="prefab">源对象</param>
        /// <param name="parent">克隆后go的父物体</param>
        /// <returns>克隆出的目标对象</returns>
        public GameObject GetGameObject(GameObject prefab,Transform parent = null)
        {
            GameObject gameObj;
            if (IsGameObjInPool(prefab))
            {
                gameObj = poolDic[prefab.name].GetObj(parent);

            }
            else
            {
                gameObj = Instantiate(prefab,parent);
                gameObj.name = prefab.name;
            }
            return gameObj;
        }
        /// <summary>
        /// 如果对象池存在则获取一个GameObject对象，否则返回null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        public GameObject GetGameObject(string path, Transform parent = null)
        {
            GameObject go = null;
            string[] strSplit = path.Split('/');
            string prefabName = strSplit[strSplit.Length - 1];
            if (IsGameObjInPool(prefabName))
            {
                go = poolDic[prefabName].GetObj(parent);
            }
            return go;
        }
        /// <summary>
        /// 获取GameObj的组件T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="prefab">源对象</param>
        /// <param name="parent">克隆后go的父物体</param>
        /// <returns>组件T</returns>
        public T GetGameObject<T>(GameObject prefab,Transform parent = null) where T : Component
        {
            GameObject go = GetGameObject(prefab,parent);
            if(go != null)
            {
                return go.GetComponent<T>();
            }
            return null;
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
                //初始化一个空的无限制容量的对象池队列,并将gameObj放入
                PoolGameObjectQueue poolQueue =  CreatePoolList(gameObj,-1,0);
                //PoolGameObjectQueue poolQueue = new PoolGameObjectQueue(gameObj, poolRoot);
                //poolDic.Add(gameObj.name, poolQueue);
                poolQueue.PushObj(gameObj);

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
        /// 创建对象池队列并预放入一些go（若已经存在则为修改已存在对象池队列的设置 如容量）
        /// </summary>
        /// <param name="prefab"></param>
        /// <param name="capacity"></param>
        /// <param name="prePushSize"></param>
        public PoolGameObjectQueue CreatePoolList(GameObject prefab,int capacity= -1,int prePushSize= 0)
        {
            if(prePushSize > capacity && capacity != -1)
            {
                throw new Exception("对象池设置默认容量超出限制");
            }
            PoolGameObjectQueue poolQueue = null;
            //如果不存在prefab的对象池队列
            if (!poolDic.ContainsKey(prefab.name))
            {
                //新建对象池队列
                poolQueue = new PoolGameObjectQueue(prefab, poolRoot,capacity);
                //poolQueue.capacity = capacity;
                poolDic.Add(prefab.name, poolQueue);
                //实例化默认数量的对象，并且加入对象队列
                if (prePushSize > 0)
                {
                    for(int i = 0; i < prePushSize;i++) 
                    {
                        GameObject go = Instantiate(prefab);
                        go.name = prefab.name;
                        poolQueue.PushObj(go);
                    }
                }
            }
            
            else 
            {
                //得到对象池队列并设置容量
                poolQueue = poolDic[prefab.name];
                poolQueue.capacity = capacity;
                //实例化默认数量的对象，并且加入对象队列
                if (prePushSize > 0)
                {
                    for (int i = 0; i < prePushSize - poolQueue.poolValueQueue.Count; i++) 
                    {
                        GameObject go = Instantiate(prefab);
                        go.name = prefab.name;
                        poolQueue.PushObj(go);
                    }
                }
            }
            return poolQueue;
        }
        /// <summary>
        /// 对象列表在池子中且存在该列表存在对象
        /// </summary>
        /// <param name="gameObj"></param>
        /// <returns></returns>
        private bool IsGameObjInPool(GameObject gameObj)
        {
            return IsGameObjInPool(gameObj.name);
        }
        private bool IsGameObjInPool(string name)
        {
            if (poolDic.ContainsKey(name) && poolDic[name].poolValueQueue.Count > 0)
                return true;
            return false;
        }
        #endregion

    }

}
