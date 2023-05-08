using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Tips : MonoBehaviour
{
    [SerializeField]
    private Text info;
    private Animator animator;
    private Queue<string> strQueue = new Queue<string>();
    bool canShow = true;

    private void Awake()
    {
        //由于transform.find不能找未激活的物体，所以暂时用拖拽方式
        //info = GameObject.Find("tipsText").GetComponent<Text>();
        //info = transform.Find("tipsBG/Text").GetComponent<Text>();
        animator = GetComponent<Animator>();

        
    }
    public void AddTips(string info)
    {
        strQueue.Enqueue(info);
        ShowTips();
    }
    public void ShowTips()
    {
        if(strQueue.Count > 0 && canShow == true)
        {
            string str = strQueue.Dequeue();
            info.text = str;
            animator.Play("Tips",0,0);
        }
    }

    #region 动画事件
    private void StartTips()
    {
        canShow = false;
    }

    private void EndTips()
    {
        canShow = true;
        ShowTips(); //达到循环的目的
    }
    #endregion

}
