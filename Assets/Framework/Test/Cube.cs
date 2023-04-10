using UnityEngine;
using Framework;

public class Cube : MonoBehaviour
{
    private void OnEnable()
    {
        
        Invoke(nameof(Removeself), 1);
    }
    

    // Update is called once per frame
    void Removeself()
    {
        print("removeself");
        PoolManager.Instance.PushGameObj(gameObject);
    }
}
