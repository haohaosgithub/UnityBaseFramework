using UnityEngine;
using Framework;
using Unity.VisualScripting.FullSerializer;

public class TestSingle : Singleton<TestSingle>
{
    public void Func()
    {
        Debug.Log("测试单例");
    }
}

public class TestPoolMgr : MonoBehaviour
{
    public GameObject cubePrefab;
    public GameObject effectAudioSourcePrefab;
    // Start is called before the first frame update
    void Start()
    {
        //PoolManager.Instance.CreatePoolList(cubePrefab, 4,1);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            //TestSingle.Instance.Func();
            //PoolManager.Instance.GetGameObject(cubePrefab);
            CubeController controller =PoolManager.Instance.GetGameObject<CubeController>(cubePrefab);

        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            //PoolManager.Instance.CreatePoolList(cubePrefab, 3, 2);
            AudioSource audioSource = PoolManager.Instance.GetGameObject<AudioSource>(effectAudioSourcePrefab);
        }   

    }


}
