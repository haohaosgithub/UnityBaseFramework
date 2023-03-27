using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
    /// <summary>
    /// 对象池管理器，给外部提供对对象池的对象获取和放入的功能 
    /// 目的时为了减少频繁实例化和销毁造成的消耗
    /// </summary>
    public class PoolManager : ManagerBase<PoolManager>
    {
        Dictionary<string, PoolGameObjectData> poolDic;
        GameObject poolRoot;
        public override void Init()
        {
            base.Init();
            poolDic = new Dictionary<string, PoolGameObjectData>();
            poolRoot = new GameObject("poolRoot");
            GameObject GameRoot = GameObject.Find("GameRoot");
            poolRoot.transform.SetParent(GameRoot.transform);
            print("pool manager 初始化成功");
        }

        /// <summary>
        /// 获取GameObject对象
        /// </summary>
        /// <param name="prefab">参数是预制体，当对象池没有对应对象时用该预制体实例化对象，当对象池存在对应对象时用预制体名称从对象池中取</param>
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
                gameObj.name = prefab.name;
            }
            return gameObj;
        }

        /// <summary>
        /// 将GameObject对象放入到对象池中
        /// </summary>
        /// <param name="gameObj"></param>
        public bool PushGameObj(GameObject gameObj,int capacity = -1)
        {
            if (poolDic.ContainsKey(gameObj.name))
            {
                return poolDic[gameObj.name].PushObj(gameObj);
            }
            else
            {
                poolDic.Add(gameObj.name, new PoolGameObjectData(gameObj, poolRoot,capacity));
                return true;
            }
        }

        private bool IsGameObjInPool(GameObject gameObj)
        {
            if (poolDic.ContainsKey(gameObj.name) && poolDic[gameObj.name].poolValueQueue.Count > 0)
                return true;
            return false;
        }
    }

}
