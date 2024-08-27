using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum UI{
    PAUSE,
    GAMEPLAY,
    INTERACT
}
public class UIManager : SingletonMonoBehaviour<UIManager>
{
    [Header("UI Prefabs")]
    public UIPause uiPauseprefab;
    public UIInteract uiInteractPrefab;
    public UIObjective  uiObjectivePrefab;
    public UIShowMessage uiShowMessagePrefab;
    [Header("UI References")]
    public UIPause uiPause;
    public UIInteract uiInteract;
    public UIObjective uiObjective;
    public UIShowMessage uiShowMessage;
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
        uiInteract.Hide();
        uiPause.Hide();        
    }

    // Update is called once per frame
    void Update()
    {
        if(currentUI == UI.GAMEPLAY) {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = true;
        }
        if(Input.GetKeyDown(KeyCode.Escape)){
            GameManager.instance.PauseGame();
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
        }
        currentUI = UI.GAMEPLAY;
        

    }

    public void ShowMessage(string message){
        uiShowMessage.Show();
        uiShowMessage.SetMessage(message);
    }

}
