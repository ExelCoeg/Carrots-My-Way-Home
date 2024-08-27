using TMPro;
public class UIInteract : UIBase
{
    public static UIInteract instance;
    public TextMeshProUGUI interactText;
    private void Awake() {
        if(instance == null){
            instance = this;
        }
        else{
            Destroy(this);
        }
    }
}
