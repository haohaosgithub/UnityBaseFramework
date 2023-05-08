using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Framework
{
    /// <summary>
    /// Framework的场景管理器，由于Unity有SceneManager，为了避免重名带来的各种问题，将框架中的场景管理器命名为SceneMgr
    /// </summary>
    public class SceneMgr : MonoBehaviour
    {
        public void Load(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
        public void LoadAsync(string sceneName, Action cb = null)
        {
            StartCoroutine(DoLoadAsync(sceneName,cb));
        }

        public IEnumerator DoLoadAsync(string sceneName,Action cb = null)
        {
            AsyncOperation ao =  SceneManager.LoadSceneAsync(sceneName);
            while(!ao.isDone)
            {
                
                EventManager.Instance.EventTrigger<float>("LoadingSceneProgress",ao.progress);
                yield return null;   
            }
            EventManager.Instance.EventTrigger<float>("LoadingSceneProgress", 1f);
            EventManager.Instance.EventTrigger("LoadingSceneComplete");
            cb?.Invoke();
        }
    }
}

