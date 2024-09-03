
using System.Collections;
using UnityEngine.UI;

public class UIFirstHint : UIBase
{
    public static UIFirstHint instance;
    public Button closeButton;
    public override void Awake() {
        if(instance == null){
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    public override void Show()
    {
        base.Show();
        StartCoroutine(AnimateShow());
    }

    private void Start() {
        closeButton.onClick.AddListener(() => {
            SoundManager.Instance.PlaySound2D("clickUI");
            StartCoroutine(Close());
        });
    }

    public IEnumerator Close(){
        yield return StartCoroutine(AnimateHide());

        UIManager.instance.HideUI(UI.FIRSTHINT);
    }

}
