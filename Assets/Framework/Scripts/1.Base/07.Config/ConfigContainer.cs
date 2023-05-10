using System;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Framework
{
    //配置容器类，所有游戏内配置（如装备，NPC，玩家配置等）
    [CreateAssetMenu(fileName = "ConfigContainer", menuName = "Config/ConfigContainer")]
    public class ConfigContainer : SerializedScriptableObject
    {
        public Dictionary<string, Dictionary<int, ConfigBase>> configDic;
        public T GetConfig<T>(string typeName, int id) where T : ConfigBase
        {
            if (!configDic.ContainsKey(typeName))
            {
                throw new Exception($"获取配置错误：没有{typeName}");
            }
            if (!configDic[typeName].ContainsKey(id))
            {
                throw new Exception($"获取配置错误：{typeName}下没有{id}");
            }
            return configDic[typeName][id] as T;
        }

    }

}
