
using UnityEngine;
using UnityEngine.UI;

public class UIFirstHint : UIBase
{
    public Button closeButton;
    public static UIFirstHint instance;
    private void Awake() {
        if(instance == null){
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }
    private void Start() {
        closeButton.onClick.AddListener(() => {
            UIManager.instance.HideUI(UI.FIRSTHINT);
        });
    }

}
