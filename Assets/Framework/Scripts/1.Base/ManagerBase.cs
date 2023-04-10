using UnityEngine;

namespace Framework
{
    //提取出这个抽象基类是为了在GameRoot中初始化不同类型的管理器
    public abstract class ManagerBase : MonoBehaviour
    {
        public virtual void Init()
        {

        }
    }


    public class ManagerBase<T> : ManagerBase where T : ManagerBase<T>
    {
        private static T instance;
        
        //只读属性提供获取管理器的方法
        public static T Instance
        {
            get
            {
                return instance;
            }
        }

        //提供初始化管理器的方法
        public override void Init()
        {
            instance = this as T;
        }
    }
}


