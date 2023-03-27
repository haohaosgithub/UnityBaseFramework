using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
    public abstract class ManagerBase : MonoBehaviour
    {
        public virtual void Init()
        {

        }
    }


    public class ManagerBase<T> : ManagerBase where T : ManagerBase<T>
    {
        private static T instance;
        public static T Instance
        {
            get
            {
                return instance;
            }
        }

        public override void Init()
        {
            instance = this as T;
        }
    }
}


