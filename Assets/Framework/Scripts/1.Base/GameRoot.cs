using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
    public class GameRoot : SingletonMono<GameRoot>
    {
        protected override void Awake()
        {
            if (instance != null)
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
            print("初始化所有管理器，数量为：" + managers.Length);
            foreach (ManagerBase manager in managers)
            {
                manager.Init();
            }
        }
    }
}


