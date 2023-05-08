using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
    /// <summary>
    /// 存放panel元数据和PanelBase
    /// </summary>
    public class UIPanel
    {
        public GameObject prefab;
        public int layerNum;
        public bool isCache;

        public PanelBase panelBase;
    }
}


