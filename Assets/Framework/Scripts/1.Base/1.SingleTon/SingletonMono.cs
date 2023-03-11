using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonMono<T> : MonoBehaviour where T : SingletonMono<T>
{
    private static T instance;
    protected virtual void Awake()
    {

        //this�������ǻ���SingletonMono<T>,������ת�� ����ת����SingletonMono<T> -> T
        //������Ҫ�����߱�֤ת�ͷ������壬�����ǽ������﷨
        //A�������Ҫ��Ϊ�����࣬������ʱ�������� class A : SingletonMono<A>

        #region ����
        //ʵ�����ǵ�Ŀ�ľ��� ĳ���������õ���Aһ���̳��� SingletonMono<�õ���������A>,
        //����Ŀǰ��д��ʵ����û��Լ����һ��
        //where Լ��ʵ����ֻԼ���� SingletonMono<T>��������T ����̳�SingletonMono<T>��Ҳ���ǶԻ����T��Լ��
        //�����Ѿ����ڵ��� string, GameObject�������������Ѷ���δ�̳�Singleton<T>���඼������Ϊ����T
        //��Ϊû�д��Ѿ����ڵ��ൽ����������ת��

        //�����µ��Զ�����඼������Ϊ����T
        //����һ�����������������A����ʱȷ����һ���� SingletonMono<A>
        //���������Զ�����඼���Լ̳����SingletonMono<A>�ˣ�
        //��������һ����B �������������� class B : SingletonMono<A>�Ƿ����﷨�ģ�
        //��ΪA����SingletonMono<A>�����࣬������ΪT
        //��������ʹ�ú�B��ʵ�����ɲ��꣬���ڵ����ߴ���

        //�ܽ���˵�����whereԼ��ֻԼ���˷��ͻ��࣬������ȷ����ֻҪ�Ϸ����κ��������඼���Լ̳�������࣬
        //������Ҫ�����߰��չ淶ȥ�̳и�����ʵ�ֵ����Ĺ���
        #endregion

        instance = this as T;
    }
}
