using UnityEngine;
using Framework;
[Pool]
public class Cube : MonoBehaviour
{
    private void OnEnable()
    {
        //Invoke(nameof(Removeself), 1);
    }
    

    // Update is called once per frame
    void Removeself()
    {
        //PoolManager.Instance.PushGameObj(gameObject);
    }
}
