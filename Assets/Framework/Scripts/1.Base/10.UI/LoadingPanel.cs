using Framework;
using UnityEngine.UI;

[UIPanel("UI/loadingPanel",4,true)]
public class LoadingPanel : PanelBase
{
    public Text loadingText;
    public Image loadingFill;

    public override void Init()
    {
        base.Init();
    }

    public override void OnShow()
    {
        base.OnShow();
        UpdatePanel(0);
    }
    public override void OnClose()
    {
        base.OnClose();
    }

    public override void RegisterEventListener()
    {
        base.RegisterEventListener();
        EventManager.Instance.Register<float>("LoadingSceneProgress", UpdatePanel);
    }

    public override void UnRegisterEventListener()
    {
        base.UnRegisterEventListener();
        EventManager.Instance.Unregister<float>("LoadingSceneProgress", UpdatePanel);
    }

    public void UpdatePanel(float progress)
    {
        loadingText.text = (int)(progress * 100) +"%";
        loadingFill.fillAmount = progress;
    }
}
