using UnityEngine;
using Framework;

public class Cube : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke(nameof(Removeself), 5);
    }
    

    // Update is called once per frame
    void Removeself()
    {
        PoolManager.Instance.PushGameObj(gameObject);
    }
}
