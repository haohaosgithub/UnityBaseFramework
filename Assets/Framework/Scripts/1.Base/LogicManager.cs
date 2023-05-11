using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Framework;

/// <summary>
/// 逻辑管理器，每个场景通过它的子类来驱动
/// 实际只是对mono单例多封装了两个方法用来注册和注销事件监听，并在OnEnable和OnDisable中调用
/// </summary>
/// <typeparam name="T"></typeparam>
[DefaultExecutionOrder(101)]
public abstract class LogicManager<T> : SingletonMono<T> where T : LogicManager<T>
{
    protected void OnEnable()
    {
        
        RegisterListener();
    }

    protected void OnDisable()
    {
        UnRegisterListener();
    }

    protected abstract void RegisterListener();

    protected abstract void UnRegisterListener();

}
