using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanelAttribute : Attribute
{
    public string prefabPath;
    public int layerNum;
    public bool isCache;

    public UIPanelAttribute(string path,int num,bool isCache)
    {
        prefabPath = path;
        layerNum = num;
        this.isCache = isCache;
    }
}
