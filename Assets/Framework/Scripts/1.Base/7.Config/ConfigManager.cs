using Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
    /// <summary>
    /// 配置管理器 ，只管理和游戏内相关
    /// </summary>
    public class ConfigManager : ManagerBase<ConfigManager>
    {
        public ConfigSetting configSetting;

        public T GetConfig<T>(string typeName, int id) where T : ConfigBase
        {
            return configSetting.GetConfig<T>(typeName, id);
        }
    }
}


