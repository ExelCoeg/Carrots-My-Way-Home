using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum UI{
    PAUSE,
    GAMEPLAY
}
public class UIManager : SingletonMonoBehaviour<UIManager>
{
    public UIPause uiPauseprefab;
    public UIPause uiPause;
    public Transform canvas;

    public UI currentUI;
    // Start is called before the first frame update
    void Start()
    {
        uiPause = Instantiate(uiPauseprefab, canvas);
        uiPause.Hide();        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            GameManager.instance.PauseGame();
        }

    }

    public void ShowUI(UI ui){
        if(currentUI == ui){
            return;
        }
        switch(ui){
            case UI.PAUSE:
                //show pause ui
                uiPause.Show();
                break;
        
        }

        currentUI = ui;
    }

}
