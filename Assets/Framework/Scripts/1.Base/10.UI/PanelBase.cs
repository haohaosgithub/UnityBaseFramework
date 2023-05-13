using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
    /// <summary>
    /// 面板基类(其子类挂载在UI面板上）
    /// </summary>
    public class PanelBase : MonoBehaviour
    {
        Type PanelType 
        {   get 
            {
                return this.GetType();
            } 
        } //得到自己的类型（子类）

        public virtual void Init()
        {

        }
        public virtual void OnShow()
        {
            RegisterEventListener();
        }

        /// <summary>
        /// 关闭自己（当前面板）
        /// </summary>
        public void Close() 
        {
            UIManager.Instance.Close(PanelType);
        }
        public virtual void OnClose() 
        {
            UnRegisterEventListener();
        }

        public virtual void RegisterEventListener() 
        {

        }

        public virtual void UnRegisterEventListener()
        {

        }
    }
}

