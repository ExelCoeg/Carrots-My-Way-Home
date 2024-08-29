using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIQuestions : UIBase
{
    public static UIQuestions instance;
    public Button closeButton;
    private void Awake() {
        if(instance == null){
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }
    private void Start() {
        closeButton.onClick.AddListener(() => {
            UIManager.instance.HideUI(UI.QUESTIONS);
        });
    }
    


}
