using Unity.VectorGraphics;
using UnityEngine;


public enum UI{
    PAUSE,
    GAMEPLAY,
    INTERACT,
    CODEPANEL,
    FIRSTHINT,
    INVENTORY,
    QUESTIONS,
    FRUITBASKETS,
    MORAL,
    SHOWMESSAGE,
    OBJECTIVE,
    TRANSITION
}
public class UIManager : MonoBehaviour

{
    public static UIManager instance;
        
    
    [Header("UI Prefabs")]
    public UIPause uiPauseprefab;
    public UIInteract uiInteractPrefab;
    public UIObjective  uiObjectivePrefab;
    public UIShowMessage uiShowMessagePrefab;
    public UIInventory uiInventoryPrefab;
    [Header("UI References")]
    public UIPause uiPause;
    public UIInteract uiInteract;
    public UIObjective uiObjective;
    public UIShowMessage uiShowMessage;
    public UIInventory uiInventory;
    [Header("Canvas Reference")]
    public Transform canvas;
    [Header("Transition Image")]
    public SVGImage uiTransitionImagePrefab;
    public SVGImage uiTransitionImage;

    [Header("----------------------")]
    public UI currentUI;
    private void Awake() {
        if(instance == null){
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        currentUI = UI.GAMEPLAY;
        uiPause = Instantiate(uiPauseprefab, canvas);
        uiInteract = Instantiate(uiInteractPrefab, canvas);
        uiObjective = Instantiate(uiObjectivePrefab,canvas);
        uiShowMessage = Instantiate(uiShowMessagePrefab, canvas);
        uiInventory = Instantiate(uiInventoryPrefab, canvas);
        uiTransitionImage = Instantiate(uiTransitionImagePrefab, canvas.transform);
        uiInventory.Hide();
        uiInteract.Hide();
        uiPause.Hide();        
    }

    // Update is called once per frame
    void Update()
    {

        if(currentUI == UI.GAMEPLAY) {
            GameManager.instance.isInteracting = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            if(Input.GetKeyDown(KeyCode.Escape) && !GameManager.instance.isTransitioning){
                GameManager.instance.PauseGame();
            }
        }
        else if(currentUI == UI.PAUSE || currentUI == UI.CODEPANEL || currentUI == UI.FIRSTHINT || currentUI == UI.QUESTIONS || currentUI == UI.FRUITBASKETS){
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            GameManager.instance.isInteracting = true;
        }
        canvas = GameObject.Find("Canvas").transform;
    }
    
    public void ShowUI(UI ui){
        if(currentUI == ui){
            return;
        }
        HideUI(currentUI);
        switch(ui){
            case UI.PAUSE:
                uiPause.Show();
                Cursor.lockState = CursorLockMode.None;
                break;
            case UI.INTERACT:
                uiInteract.Show();
                break;
            case UI.CODEPANEL:
                UICodePanel.Instance.Show();
                break;
            case UI.FIRSTHINT:
                UIFirstHint.instance.Show();
                break;
            case UI.QUESTIONS:
                UIQuestions.instance.Show();
                break;
            case UI.FRUITBASKETS:
                UIFruitBaskets.instance.Show();
                break;
            case UI.MORAL:
                UIMoral.instance.Show();
                break;
            case UI.OBJECTIVE:
                uiObjective.Show();
                break;
            case UI.SHOWMESSAGE:
                uiShowMessage.Show();
                break;
            case UI.TRANSITION:
                uiTransitionImage.gameObject.SetActive(true);
                break;
        
        }

        currentUI = ui;
    }

    public void HideUI(UI ui){
        if(currentUI != ui){
            return;
        }
        
        switch(ui){
            case UI.PAUSE:
                uiPause.Hide();
                break;
            case UI.INTERACT:
                uiInteract.Hide();
                break;
            case UI.CODEPANEL:
                UICodePanel.Instance.Hide();
                break;
            case UI.FIRSTHINT:
                UIFirstHint.instance.Hide();
                break;
            case UI.QUESTIONS:
                UIQuestions.instance.Hide();
                break;
            case UI.FRUITBASKETS:
                UIFruitBaskets.instance.Hide();
                break;
            case UI.MORAL:
                UIMoral.instance.Hide();
                break;
            case UI.OBJECTIVE:
                uiObjective.Hide();
                break;
            case UI.SHOWMESSAGE:
                uiShowMessage.Hide();
                break;
            case UI.TRANSITION:
                uiTransitionImage.gameObject.SetActive(false);
                break;


        }
        currentUI = UI.GAMEPLAY;
    }

    public void ShowMessage(string message){
        uiShowMessage.Show();
        uiShowMessage.SetMessage(message);
    }
    public void Initialize(){

    }
     

}
