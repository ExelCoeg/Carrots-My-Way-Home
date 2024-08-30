using UnityEngine;
using UnityEngine.UI;
public class UIQuestions : UIBase
{
    public static UIQuestions instance;
    public Button closeButton;
    public Question currentQuestion;
    public override void Awake() {
        if(instance == null){
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.F)){
            currentQuestion.CheckAnswer();
        }
    }
    private void Start() {
        closeButton.onClick.AddListener(() => {
            UIManager.instance.HideUI(UI.QUESTIONS);
        });
    }


}
