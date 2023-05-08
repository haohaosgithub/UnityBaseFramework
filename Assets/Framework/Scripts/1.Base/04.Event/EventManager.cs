using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Framework
{
    /// <summary>
    /// 事件管理器 
    /// 主要提供注册事件监听（关心的事件，事件发生时的callback） 注销事件监听 以及触发事件
    /// </summary>
    public class EventManager : ManagerBase<EventManager>
    {
        #region 内部类，内部接口
        public interface IEventInfo
        {
            public void Destroy();
        }

        public class EventInfo : IEventInfo
        {
            public Action action;
            public void Destroy()
            {
                action = null;
            }
        }
        public class EventInfo<T> : IEventInfo
        {
            public Action<T> action;
            public void Destroy()
            {
                action = null;
            }
        }
        public class EventInfo<T,U> : IEventInfo
        {
            public Action<T,U> action;
            public void Destroy()
            {
                action = null;
            }
        }
        #endregion

        Dictionary<string,IEventInfo> eventDic;
        public override void Init()
        {
            base.Init();
            eventDic = new Dictionary<string,IEventInfo>();
        }
        
        #region 注册事件监听
        /// <summary>
        /// 注册事件监听 （无参） 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="action"></param>
        public void Register(string name, Action action)
        {
            if(eventDic.ContainsKey(name)) 
            {
                (eventDic[name] as EventInfo).action += action;
            }
            else
            {
                EventInfo eventInfo = new EventInfo();
                eventInfo.action = action;
                eventDic.Add(name, eventInfo);
            }
        }
        /// <summary>
        /// 注册事件监听 （一个参数）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="action"></param>
        public void Register<T>(string name, Action<T> action)
        {
            if (eventDic.ContainsKey(name))
            {
                (eventDic[name] as EventInfo<T>).action   += action;
            }
            else
            {
                EventInfo<T> eventInfo = new EventInfo<T>();
                eventInfo.action = action;
                eventDic.Add(name, eventInfo);
            }
        }
        /// <summary>
        /// 注册事件监听 （两个参数）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="name"></param>
        /// <param name="action"></param>
        public void Register<T,U>(string name, Action<T, U> action)
        {
            if (eventDic.ContainsKey(name))
            {
                (eventDic[name] as EventInfo<T, U>).action += action;
            }
            else
            {
                EventInfo<T, U> eventInfo = new EventInfo<T, U>();
                eventInfo.action = action;
                eventDic.Add(name, eventInfo);
            }
        }
        #endregion
        
        #region 注销事件监听
        public void Unregister(string name,Action action) 
        {
            if(eventDic.ContainsKey(name))
            {
                (eventDic[name] as EventInfo).action -= action;
            }
        }
        public void Unregister<T>(string name, Action<T> action)
        {
            if (eventDic.ContainsKey(name))
            {
                (eventDic[name] as EventInfo<T>).action -= action;
            }
        }
        public void Unregister<T,U>(string name, Action<T, U> action)
        {
            if (eventDic.ContainsKey(name))
            {
                (eventDic[name] as EventInfo<T, U>).action -= action;
            }
        }
        #endregion
        
        #region 触发事件
        public void EventTrigger(string name)
        {
            if (eventDic.ContainsKey(name))
            {
                (eventDic[name] as EventInfo).action?.Invoke();
            }
        }
        public void EventTrigger<T>(string name,T targs)
        {
            if (eventDic.ContainsKey(name))
            {
                (eventDic[name] as EventInfo<T>).action?.Invoke(targs);
            }
        }
        public void EventTrigger<T,U>(string name, T targs,U uargs)
        {
            if (eventDic.ContainsKey(name))
            {
                (eventDic[name] as EventInfo<T,U>).action?.Invoke(targs,uargs);
            }
        }
        #endregion

        #region 删除事件
        /// <summary>
        /// 删除事件名为name的事件
        /// </summary>
        /// <param name="name">事件名</param>
        public void RemoveEvent(string name)
        {
            if(eventDic.ContainsKey(name))
            {
                eventDic[name].Destroy();
                eventDic.Remove(name);
            }
        }
        /// <summary>
        /// 清空事件
        /// </summary>
        public void Clear()
        {
            foreach(var kvp in eventDic)
            {
               kvp.Value.Destroy();
            }
            eventDic.Clear();
        }
        #endregion
    }
}