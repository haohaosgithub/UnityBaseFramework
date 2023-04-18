using Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigManager : ManagerBase<ConfigManager>
{
    public ConfigSetting configSetting;

    public T GetConfig<T>(string typeName, int id) where T : ConfigBase
    {
        return configSetting.GetConfig<T>(typeName, id);
    }
}
