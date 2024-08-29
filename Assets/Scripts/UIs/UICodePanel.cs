using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine;
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
    
    private void Awake() {
        
        if(Instance == null){
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        closeButton.onClick.AddListener(() => UIManager.instance.HideUI(UI.CODEPANEL));
        clearButton.onClick.AddListener(() => ClearInput());
        enterButton.onClick.AddListener(() => CheckAnswer());
    }
    public void CheckAnswer()
    {
        if(currentInput == correctAnswer){//ineffective
            FirstLevelPuzzle firstLevelPuzzle = FindObjectOfType<FirstLevelPuzzle>();
            firstLevelPuzzle.puzzles[0].complete = true;
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
        numberPadMainText.placeholder.GetComponent<TextMeshProUGUI>().text = "Correct!";
        yield return new WaitForSeconds(1);
        Hide();
    }
    public void ClearInput()
    {
        currentInput = "";
        numberPadMainText.text = currentInput;
        numberPadMainText.placeholder.GetComponent<TextMeshProUGUI>().text = "Enter Code";
    }
    public void SetCorrectAnswer(string correctAnswer)
    {
        this.correctAnswer = correctAnswer;
    }
}
