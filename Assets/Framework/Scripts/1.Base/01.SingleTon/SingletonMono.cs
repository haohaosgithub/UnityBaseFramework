using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
    public class SingletonMono<T> : MonoBehaviour where T : SingletonMono<T>
    {
        protected static T instance;
        
        public static T Instance { get { return instance; } }
        protected virtual void Awake()
        {

            //this的类型是基类SingletonMono<T>,即向下转型 基类转子类SingletonMono<T> -> T
            //所以需要调用者保证转型符合语义，而不是仅满足语法
            //A类如果需要作为单例类，则声明时这样声明 class A : SingletonMono<A>

            #region 解释
            //实际我们的目的就是 某个单例作用的类A一定继承于 SingletonMono<该单例作用类A>,
            //但是目前的写法实际上没有约束这一点
            //where 约束实际上只约束了 SingletonMono<T>这个基类的T 必须继承SingletonMono<T>，也就是对基类的T的约束
            //比如已经存在的类 string, GameObject或者任意我们已定义未继承Singleton<T>的类都不能作为泛型T
            //因为没有从已经存在的类到这个泛型类的转化

            //任意新的自定义的类都可以作为泛型T
            //有了一个派生类后，如上例的A，此时确定了一个类 SingletonMono<A>
            //则任意新自定义的类都可以继承这个SingletonMono<A>了，
            //比如有另一个类B ，则它这样声明 class B : SingletonMono<A>是符合语法的，
            //因为A类是SingletonMono<A>的子类，可以作为T
            //但是这样使用后B类实际语焉不详，属于调用者错用

            //总结来说，这个where约束只约束了泛型基类，当泛型确定后，只要合法，任何新增子类都可以继承这个基类，
            //所以需要调用者按照规范去继承该类以实现单例的功能
            #endregion

            instance = this as T;
        }
    
    }
}

