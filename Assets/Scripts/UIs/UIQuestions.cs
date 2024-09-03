using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;
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
    private void Start() {
        transform.localScale = Vector3.zero;
        closeButton.onClick.AddListener(() => {
            SoundManager.Instance.PlaySound2D("clickUI");
            StartCoroutine(CloseAnimation());

        });
    }
    private void Update() {
        if(Input.GetKeyDown(KeyCode.F)){
            currentQuestion.CheckAnswer();
        }
    }
    public override void Show(){
        base.Show();
        StartCoroutine(OpenAnimation());
    }
    public override void Hide(){
        base.Hide();
    }

    public IEnumerator OpenAnimation(){
        UIInventory.instance.AnimateInventoryToCenter();
        yield return StartCoroutine(AnimateShow());
    }
    public IEnumerator CloseAnimation(){
        UIInventory.instance.AnimateInventoryToUpperLeft();
        yield return StartCoroutine(AnimateHide());
        UIManager.instance.HideUI(UI.QUESTIONS);
    }

}
