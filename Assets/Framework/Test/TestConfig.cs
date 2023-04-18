using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestConfig : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        print(ConfigManager.Instance.GetConfig<DemoConfig>("weapon", 0).weaponName);
        print(ConfigManager.Instance.GetConfig<DemoConfig>("weapon", 1).weaponName);
    }

    
}
