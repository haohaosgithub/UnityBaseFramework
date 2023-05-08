using System;
using System.Collections.Generic;

namespace Framework
{
    public interface IStateMachineOwner
    {
        
    }
    public class StateMachine
    {
        public IStateMachineOwner owner;
        StateBase curStateObj;
        Type CurStateType { get => curStateObj.GetType(); }
        Dictionary<Type, StateBase> stateDic;
        public void Init(IStateMachineOwner owner)
        {
            this.owner = owner;
            stateDic = new Dictionary<Type, StateBase>();
        }

        /// <summary>
        /// 切换到状态T
        /// </summary>
        /// <typeparam name="T">目标状态类型</typeparam>
        /// <param name="isHandleChangeCur">当切换的目标状态和当前状态相同时是否处理</param>
        public void ChangeState<T>(bool isHandleChangeCur = false) where T : StateBase,new() 
        {
            //切换的目标状态和当前状态相同且不处理
            if (curStateObj != null && CurStateType == typeof(T) && !isHandleChangeCur) return;
            //退出当前状态
            if(curStateObj != null)
            {
                curStateObj.Exit();
                MonoManager.Instance.RemoveUpdateListener(curStateObj.Update);
                MonoManager.Instance.RemoveLateUpdateListener(curStateObj.LateUpdate);
                MonoManager.Instance.RemoveFixedUpdateListener(curStateObj.FixedUpdate);
            }
            
            //进入新状态
            curStateObj = GetState<T>();
            curStateObj.Enter();
            MonoManager.Instance.AddUpdateListener(curStateObj.Update);
            MonoManager.Instance.AddLateUpdateListener(curStateObj.LateUpdate);
            MonoManager.Instance.AddFixedUpdateListener(curStateObj.FixedUpdate);
        }

        private StateBase GetState<T>() where T : StateBase ,new()
        {
            if(!stateDic.TryGetValue(typeof(T), out StateBase state))
            {
                state = new T();
                state.Init(this);
                stateDic.Add(typeof(T), state);
            }
            return state;
        }
        
        
        public void Stop()
        {
            curStateObj.Exit();
            MonoManager.Instance.RemoveUpdateListener(curStateObj.Update);
            MonoManager.Instance.RemoveLateUpdateListener(curStateObj.LateUpdate);
            MonoManager.Instance.RemoveFixedUpdateListener(curStateObj.FixedUpdate);
            curStateObj = null;
            owner = null;
            foreach (var state in stateDic.Values) 
            {
                state.UnInit();
            }
            stateDic.Clear();
        }
    }
}

