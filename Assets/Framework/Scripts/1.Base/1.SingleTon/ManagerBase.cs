using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ManagerBase : MonoBehaviour
{
    public virtual void Init()
    {

    }
}


public class ManagerBase<T> : ManagerBase where T : ManagerBase<T>
{
    private static T instance;
    
    public  override void Init()
    {
        instance = this as T;
    }


}
