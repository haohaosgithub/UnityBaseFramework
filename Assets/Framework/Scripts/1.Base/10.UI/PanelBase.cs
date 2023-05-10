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

        public virtual void Init()
        {

        }
        public virtual void OnShow()
        {
            RegisterEventListener();
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

