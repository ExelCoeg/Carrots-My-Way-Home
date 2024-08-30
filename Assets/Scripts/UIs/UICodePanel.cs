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
        transform.DOScale(1, 0.5f).SetEase(Ease.OutBack);
    }
    public override void Hide()
    {
        transform.DOScale(0, 0.5f).SetEase(Ease.OutBack);
        
        base.Hide();
    }
    private void Start()
    {
        closeButton.onClick.AddListener(() => {
            SoundManager.Instance.PlaySound2D("clickUI");

            UIManager.instance.HideUI(UI.CODEPANEL);
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
            ClearInput();
            numberPadMainText.placeholder.GetComponent<TextMeshProUGUI>().text = "Incorrect.";
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
    public IEnumerator Correct(){
        ClearInput();
        numberPadMainText.placeholder.GetComponent<TextMeshProUGUI>().text = "Correct.";

        yield return new WaitForSeconds(1);        

        FirstLevelPuzzle firstLevelPuzzle = FindObjectOfType<FirstLevelPuzzle>();
        firstLevelPuzzle.puzzles[0].complete = true;

        UIManager.instance.HideUI(UI.CODEPANEL);


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
}
