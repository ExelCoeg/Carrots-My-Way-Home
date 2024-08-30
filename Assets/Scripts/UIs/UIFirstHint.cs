
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
    private void Start() {
        closeButton.onClick.AddListener(() => {
            UIManager.instance.HideUI(UI.FIRSTHINT);
        });
    }

}
