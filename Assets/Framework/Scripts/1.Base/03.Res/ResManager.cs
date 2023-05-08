using Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResManager : ManagerBase<ResManager>
{
    private HashSet<Type> wantCacheSet; //需要缓存的类型
    public override void Init()
    {
        base.Init();
        wantCacheSet = GameRoot.Instance.GeneralConfig.hasPoolAtrTypeSet;
        //wantCacheDic = new Dictionary<Type, bool>();
        //wantCacheDic.Add(typeof(Cube), true);
    }

    private bool IsWantCache(Type type)
    {
        return wantCacheSet.Contains(type);
    }

    #region 对外提供的函数
    #region 同步方法
    /// <summary>
    /// 实例化go（根据预制体路径），并返回go组件
    /// </summary>
    /// <typeparam name="T">组件类型</typeparam>
    /// <param name="path">预制体路径</param>
    /// <param name="parent">实例化go后设置父物体</param>
    /// <returns>go组件</returns>
    public T Load<T>(string path, Transform parent = null) where T : Component
    {
        
        if (IsWantCache(typeof(T)))
        {
            return PoolManager.Instance.GetGameObject<T>(GetPrefab(path), parent);
        }
        else
        {
            return InstantiateFromPrefab(GetPrefab(path), parent).GetComponent<T>();
        }
    }
    /// <summary>
    /// 加载资源 如AudioClip Sprite
    /// </summary>
    /// <typeparam name="T">资源类型</typeparam>
    /// <param name="path">资源路径</param>
    /// <returns></returns>
    public T LoadAsset<T>(string path) where T : UnityEngine.Object
    {
        return Resources.Load<T>(path);
    }
    #endregion
    #region 异步方法
    /// <summary>
    /// 异步加载go，并实例化 （如果对象池有直接从对象池中取）
    /// </summary>
    /// <typeparam name="T">组件类型</typeparam>
    /// <param name="path">prefab路径</param>
    /// <param name="cb">加载完成的回调</param>
    /// <param name="parent">实例化节点的父节点</param>
    public void LoadAsync<T>(string path,Action<T> cb = null,Transform parent = null) where T : Component
    {
        //异步获取go时，只有在对象池中有时才从对象池获取，否则异步加载获取
        if(IsWantCache(typeof(T)))
        {
            GameObject go = PoolManager.Instance.GetGameObject(path,parent);
            if(go != null) 
            {
                cb?.Invoke(go.GetComponent<T>());
            }
            else
            {
                StartCoroutine(DoLoadAsync<T>(path, cb, parent));
            }
        }
        else
        {
            StartCoroutine(DoLoadAsync<T>(path, cb, parent));
        }
    }

    private IEnumerator DoLoadAsync<T>(string path, Action<T> cb = null, Transform parent = null) where T : Component
    {
        ResourceRequest rq = Resources.LoadAsync(path);
        yield return rq;
        GameObject prefab = rq.asset as GameObject;
        GameObject go = InstantiateFromPrefab(prefab,parent);
        cb?.Invoke(go.GetComponent<T>());
    }
    /// <summary>
    /// 异步加载资源，如AudioClip,Sprite，Texture等
    /// </summary>
    /// <typeparam name="T">资源类型</typeparam>
    /// <param name="path">资源路径</param>
    /// <param name="cb">加载完成后的回调</param>
    public void LoadAssetAsync<T>(string path, Action<T> cb) where T : UnityEngine.Object
    {
        StartCoroutine(DoLoadAssetAsync<T>(path, cb));
    }
    public IEnumerator DoLoadAssetAsync<T>(string path, Action<T> cb) where T : UnityEngine.Object
    {
        ResourceRequest rq = Resources.LoadAsync(path);
        yield return rq;
        cb?.Invoke(rq.asset as T);
    }

    #endregion
    #endregion
    #region 工具函数
    //根据路径加载预制体资源
    private GameObject GetPrefab(string path)
    {
        GameObject prefab = Resources.Load<GameObject>(path);
        return prefab;
    }
    //根据预制体实例化go
    public GameObject InstantiateFromPrefab(GameObject prefab,Transform parent = null)
    {
        GameObject go= Instantiate(prefab,parent);
        go.name = prefab.name;
        return go;
    }
#endregion
}
