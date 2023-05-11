using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
    [DefaultExecutionOrder(100)]
    public class GameRoot : SingletonMono<GameRoot>
    {
        [SerializeField]
        private GeneralConfig generalConfig;
        public GeneralConfig GeneralConfig { get { return generalConfig; } }
        protected override void Awake()
        {
            //如果实例不为空说明已经有对应的gameRoot脚本了，则删除多余的直接返回
            if (instance != null) 
            {
                Destroy(gameObject);
                return;
            }
            base.Awake();
            DontDestroyOnLoad(gameObject);

            //初始化所有管理器
            InitManagers();

        }
        

        private void InitManagers()
        {
            ManagerBase[] managers = GetComponents<ManagerBase>();
            print("初始化所有管理器，数量为：" + managers.Length);
            foreach (ManagerBase manager in managers)
            {
                //print("初始化管理器：" + manager);
                manager.Init();
            }
        }
         
    }
}


