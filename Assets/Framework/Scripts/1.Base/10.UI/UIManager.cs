using Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Framework
{
    public class UIManager : ManagerBase<UIManager>
    {

        #region 层级管理类(管理遮罩）
        [Serializable]
        public class UILayer
        {
            public Transform root; //当前层根节点
            public Image imageMask; //当前层遮罩
            public int count;   //当前层面板数量

            public void OnShow()
            {
                ++count;
                Update();
            }
            public void OnClose()
            {
                --count;
                Update();
            }

            public void Update()
            {
                int posIndex = root.childCount - 2 < 0 ? 0 : root.childCount -2; //mask永远是倒数第2个
                imageMask.transform.SetSiblingIndex(posIndex);
                imageMask.raycastTarget = count != 0; //如果当前层只要有面板就设置该层遮罩，遮住前面的层级及当前层其他面板
            }
        }
        #endregion
        public Dictionary<Type, UIPanel> panelDic;
        public List<UILayer> layerList;
        Tips tips;

        public override void Init()
        {
            base.Init();
            panelDic = GameRoot.Instance.GeneralConfig.panelDic;
            tips = transform.Find("UIRoot/Tips").GetComponent<Tips>();
            
        }

        public T Show<T>(int layer = -1) where T : PanelBase
        {
            return Show(typeof(T), layer) as T;
        }
        public PanelBase Show(Type type,int layer = -1)
        {
            if (panelDic.ContainsKey(type))
            {
                UIPanel panel = panelDic[type];
                int layerNum = layer == -1 ? panel.layerNum : layer;
                if (panel.panelBase == null)
                {
                    //创建并设置面板到对应层级的对应位置
                    panel.panelBase = ResManager.Instance.InstantiateFromPrefab(panel.prefab, layerList[layerNum].root).GetComponent<PanelBase>();
                    panel.panelBase.Init();
                }
                else
                {
                    //显示并设置面板到对应层级的对应位置
                    panel.panelBase.gameObject.SetActive(true);
                    panel.panelBase.transform.SetParent(layerList[layerNum].root);
                    panel.panelBase.transform.SetAsLastSibling();
                }
                panel.panelBase.OnShow();
                panel.layerNum = layerNum;
                layerList[layerNum].OnShow();
                return panel.panelBase;

            }
            return null;
        }
        public void Close<T>()
        {
            Close(typeof(T));
        }
        public void Close(Type type)
        {
            if (!panelDic.ContainsKey(type)) return;
            UIPanel panel = panelDic[type];
            if (panel.panelBase == null) return;
            panel.panelBase.OnClose();
            if (panel.isCache)
            {
                panel.panelBase.gameObject.SetActive(false);
                panel.panelBase.transform.SetAsFirstSibling();
            }
            else
            {
                Destroy(panel.panelBase.gameObject);
                panel.panelBase = null;
            }
            layerList[panel.layerNum].OnClose();
        }
        public void CloseAll()
        {
            foreach(var panel in panelDic)
            {
                panel.Value.panelBase.Close();
            }
        }

        public void AddTips(string info)
        {
            tips.AddTips(info);
        }
    }
}

