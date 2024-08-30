using TMPro;
public class UIInteract : UIBase
{
    public static UIInteract instance;
    public TextMeshProUGUI interactText;
    public override void Awake() {
        if(instance == null){
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }
}
