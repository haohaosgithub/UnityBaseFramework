using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FrameworkMenuItem 
{
    [MenuItem("Framework/打开存档路径")]
    public static void OpenArchivingDirPath()
    {
        EditorUtility.RevealInFinder(Application.persistentDataPath);
    }
}
