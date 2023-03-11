using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> where T : Singleton<T>, new()
{
    private static T instance;
    //������������������������������
    //����ÿ����Ҫ��Ϊ�������඼д�ظ����룬����԰��߼�д˽��ctor
    //������Ϊ�˴�ʹ�û��࣬ÿ����Ϊ��������̳и��࣬����ctor����Ϊ˽��
    //����ʹ�ñ༭��Ĭ�����ɵ�ctor(public)��
    //��Ҫʹ�������ⲿ��Ϊ������new�ķ������쵥������
    //private Singleton() 
    //{

    //}
    public static T Instance { get { return instance; } }

    static Singleton()
    {
        instance = new T();
    }

    //�����Ƕ��̵߳�д��
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
