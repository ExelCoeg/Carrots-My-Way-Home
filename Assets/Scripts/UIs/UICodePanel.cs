using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine;
using DG.Tweening;
public class UICodePanel : UIBase
{
    public TMP_InputField numberPadMainText;
    public List<Numbers> numberPadButtons;

    public string currentInput = "";
    [SerializeField] private string correctAnswer;
    public Button closeButton;
    public Button clearButton;
    public Button enterButton;
    public static UICodePanel Instance;
    
    public override void Awake() {
        
        if(Instance == null){
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }
    public override void Show()
    {
        base.Show();
        StartCoroutine(AnimateShow());
    }
    private void Start()
    {
        closeButton.onClick.AddListener(() => {
            SoundManager.Instance.PlaySound2D("clickUI");
            ClearInput();
            StartCoroutine(Close());
        });
        clearButton.onClick.AddListener(() => {
            SoundManager.Instance.PlaySound2D("clickButton");
            ClearInput();
        });
        enterButton.onClick.AddListener(() => {
            SoundManager.Instance.PlaySound2D("clickButton");
            CheckAnswer();
        });
    }
    public void CheckAnswer()
    {
        if(currentInput == correctAnswer){//ineffective
            StartCoroutine(Correct());
        }
        else{
            StartCoroutine(Wrong());
            
        }
    }
    public void OnNumberButtonClick(int number)
    {
        if(currentInput.Length >= 4){
            return;
        }
        currentInput += number.ToString();
        numberPadMainText.text = currentInput;
    }
    public void ClearInput()
    {
        currentInput = "";
        numberPadMainText.text = currentInput;
        numberPadMainText.placeholder.GetComponent<TextMeshProUGUI>().text = "Enter Code...";
    }
    public void SetCorrectAnswer(string correctAnswer)
    {
        this.correctAnswer = correctAnswer;
    }

    //--------------------------ANIMATION SEQUENCES---------------------------------
    public IEnumerator Correct(){
        SoundManager.Instance.PlaySound2D("correct");
        ClearInput();
        numberPadMainText.placeholder.GetComponent<TextMeshProUGUI>().text = "Correct.";
        yield return numberPadMainText.placeholder.GetComponent<TextMeshProUGUI>().DOColor(Color.green,0.1f).WaitForCompletion();
        yield return numberPadMainText.placeholder.GetComponent<TextMeshProUGUI>().DOColor(Color.white,0.1f).WaitForCompletion(); 

        yield return new WaitForSeconds(1f);


        FirstLevelPuzzle firstLevelPuzzle = FindObjectOfType<FirstLevelPuzzle>();
        firstLevelPuzzle.puzzles[0].complete = true;

        yield return StartCoroutine(AnimateHide());
        UIManager.instance.HideUI(UI.CODEPANEL);
    }
    public IEnumerator Wrong(){
        ClearInput();
        SoundManager.Instance.PlaySound2D("wrong");
        numberPadMainText.placeholder.GetComponent<TextMeshProUGUI>().text = "Incorrect.";
        yield return numberPadMainText.placeholder.GetComponent<TextMeshProUGUI>().DOColor(Color.red,0.1f).WaitForCompletion();
        yield return numberPadMainText.placeholder.GetComponent<TextMeshProUGUI>().DOColor(Color.white,0.1f).WaitForCompletion();
        
    }
    public IEnumerator Close(){
        yield return StartCoroutine(AnimateHide());
        UIManager.instance.HideUI(UI.CODEPANEL);
    }
}
