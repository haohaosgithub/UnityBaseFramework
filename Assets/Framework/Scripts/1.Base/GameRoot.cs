using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoot : SingletonMono<GameRoot>
{
    protected override void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        base.Awake();
        DontDestroyOnLoad(gameObject);
        
        
        InitManagers();

    }

    private void InitManagers()
    {
        ManagerBase[] managers = GetComponents<ManagerBase>();
        foreach (ManagerBase manager in managers)
        {
            manager.Init();
        }
    }
}
