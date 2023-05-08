using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
    /// <summary>
    /// 单例模式基类思路：利用继承解决代码重复，利用泛型解决不同子类不同实例
    /// </summary>
    /// <typeparam name="T">实际的单例子类</typeparam>
    public abstract class Singleton<T> where T : Singleton<T>, new()
    {
        private static T instance;
        //如果不是用这个单例基类来处理单例，而是每个需要作为单例的类都写重复代码，则可以按逻辑写私有ctor
        //但是因为此处使用基类，每个作为单例的类继承该类，所以ctor不能为私有
        //这里使用编译器默认生成的ctor(public)，需要使用者在外部人为不能用new的方法构造此基类的单例对象
        //private Singleton() 
        //{

        //}
        public static T Instance { get { return instance; } }

        static Singleton()
        {
            instance = new T();
        }

        //不考虑多线程的写法
        //private T instance;

        //public T Instance { 
        //    get 
        //    {
        //        if(instance == null)
        //        {
        //            instance = new T();
        //        }
        //        return instance;
        //    } 
        //}


    }

}
