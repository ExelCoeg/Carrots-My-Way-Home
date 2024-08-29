using UnityEngine;


public enum UI{
    PAUSE,
    GAMEPLAY,
    INTERACT,
    CODEPANEL,
    FIRSTHINT,
    INVENTORY,
    QUESTIONS
}
public class UIManager : SingletonMonoBehaviour<UIManager>
{
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

    [Header("----------------------")]
    public UI currentUI;
    // Start is called before the first frame update
    void Start()
    {
        currentUI = UI.GAMEPLAY;
        uiPause = Instantiate(uiPauseprefab, canvas);
        uiInteract = Instantiate(uiInteractPrefab, canvas);
        uiObjective = Instantiate(uiObjectivePrefab,canvas);
        uiShowMessage = Instantiate(uiShowMessagePrefab, canvas);
        uiInventory = Instantiate(uiInventoryPrefab, canvas);
        uiInventory.Hide();
        uiInteract.Hide();
        uiPause.Hide();        
    }

    // Update is called once per frame
    void Update()
    {
        if(currentUI == UI.GAMEPLAY) {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            if(Input.GetKeyDown(KeyCode.Escape)){
                GameManager.instance.PauseGame();
            }
        }
        else if(currentUI == UI.PAUSE || currentUI == UI.CODEPANEL || currentUI == UI.FIRSTHINT){
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
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
                //show pause ui
                uiPause.Show();
                Cursor.lockState = CursorLockMode.None;
                break;
            case UI.INTERACT:
                //show interact ui
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

        }

        currentUI = ui;
    }

    public void HideUI(UI ui){
        if(currentUI != ui){
            return;
        }
        switch(ui){
            case UI.PAUSE:
                //hide pause ui
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


        }
        currentUI = UI.GAMEPLAY;
    }

    public void ShowMessage(string message){
        uiShowMessage.Show();
        uiShowMessage.SetMessage(message);
    }


}
