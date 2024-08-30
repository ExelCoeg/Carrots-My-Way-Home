
using UnityEngine;
using UnityEngine.UI;


public class UIFruitBaskets : UIBase
{
    public static UIFruitBaskets instance;
    public FruitBasket currentFruitBasket;

    public Button closeButton;
    public override void Awake() {
        if(instance == null){
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }
    private void Update() {
        if(Input.GetKeyDown(KeyCode.F)){
            currentFruitBasket.CheckAnswer();
        }
    }
    private void Start() {
        closeButton.onClick.AddListener(() => {
            SoundManager.Instance.PlaySound2D("clickUI");

            UIManager.instance.HideUI(UI.FRUITBASKETS);
        });
    }
}
