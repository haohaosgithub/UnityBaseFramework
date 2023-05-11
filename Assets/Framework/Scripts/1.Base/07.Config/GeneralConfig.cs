using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting.FullSerializer;
using UnityEditor;
using UnityEngine;

namespace Framework
{
    /// <summary>
    /// 通用设置
    /// 1.处理一些与游戏内容无关的设置，如资源管理中的是否使用对象池；
    /// 2.处理UI面板
    /// 当前实例由GameRoot而不是ConfigManager管理 
    /// </summary>
    [CreateAssetMenu(fileName ="GeneralConfig",menuName ="Config/GeneralConfig")]
    public class GeneralConfig : ConfigBase
    {
        public HashSet<Type> hasPoolAtrTypeSet = new HashSet<Type>();  //如果type类型的对象被资源管理器管理，是否需要对象池，需要则在set中
        public Dictionary<Type,UIPanel> panelDic = new Dictionary<Type, UIPanel>();  //所有的面板对象
        
#if UNITY_EDITOR
        [Button("初始化通用配置")]
        //初始化通用配置（需要缓存类型字典等）
        public void Init()
        {
            PoolAttributeTravels();
            UIPanelAttributeTravels();
        }

        //找到所有有pool特性的类,加入hasPoolAtrTypeDic
        public void PoolAttributeTravels() 
        {
            if(hasPoolAtrTypeSet == null) { Debug.Log("null"); return; }
            hasPoolAtrTypeSet.Clear();
            Assembly[] asms = AppDomain.CurrentDomain.GetAssemblies();
            foreach (Assembly assembly in asms) 
            {
                foreach (Type type in assembly.GetTypes()) 
                {
                    PoolAttribute pool = type.GetCustomAttribute<PoolAttribute>();
                    if(pool != null)
                    {
                        hasPoolAtrTypeSet.Add(type);
                    }
                }
            }
        }
        //找到所有带面板特性的类，加入字典
        public void UIPanelAttributeTravels()
        {
            panelDic.Clear();
            var asm =  AppDomain.CurrentDomain.GetAssemblies();
            foreach (Assembly assembly in asm)
            {
                foreach(Type type in assembly.GetTypes())
                {
                    var attribute = type.GetCustomAttribute<UIPanelAttribute>();
                    if (attribute != null)
                    {
                        panelDic.Add(type,new UIPanel()
                        {
                            prefab = Resources.Load<GameObject>(attribute.prefabPath),
                            isCache = attribute.isCache,
                            layerNum = attribute.layerNum,

                        }
                        );
                    }
                }
            }

        }
        //Unity编辑器每次加载时执行的函数（如改动代码后）
        [InitializeOnLoadMethod]
        public static void Loading()
        {
            //直接使用Find方法获取对象及其脚本配置信息
            if(GameObject.Find("GameRoot") != null)
            {
                GameObject.Find("GameRoot").GetComponent<GameRoot>().GeneralConfig.Init();

            }

            //这里不要用单例实例，因为没有运行所以还未初始化,我们需要loading时执行函数
            //Debug.Log(GameRoot.Instance); Null
            //if(GameRoot.Instance == null || GameRoot.Instance.GeneralConfig == null)
            //{
            //    return;
            //}
            //GameRoot.Instance.GeneralConfig.Init();
        }
#endif


    }
}


