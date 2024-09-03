using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FirstLevelPuzzle : Objective
{
    // public List<InteractableObject> interactableObjects;
  
    public TMP_InputField inputField;
    
    [Header("UI Prefabs")]
    public UIFirstHint uiFirstHintPrefab;
    public UICodePanel uiCodePanelPrefab;
    public UIQuestions uiQuestionsPrefab;
    public UIFruitBaskets uIFruitBasketsPrefab;
    public UIMoral uiMoralPrefab;

    public UIFirstHint uiFirstHint;
    public UICodePanel uiCodePanel;
    public UIQuestions uiQuestions;
    public UIFruitBaskets uIFruitBaskets;
    public UIMoral uiMoral;

    [Header("Puzzles")]
    public List<Objective> puzzles;


    public int currentPuzzleIndex = 0;
    private void Start() {
        uiFirstHint = Instantiate(uiFirstHintPrefab,UIManager.instance.canvas);
        uiCodePanel = Instantiate(uiCodePanelPrefab,UIManager.instance.canvas);
        uiQuestions = Instantiate(uiQuestionsPrefab,UIManager.instance.canvas);
        uIFruitBaskets = Instantiate(uIFruitBasketsPrefab,UIManager.instance.canvas);
        uiMoral = Instantiate(uiMoralPrefab,UIManager.instance.canvas);
        uiMoral.Hide();
        uIFruitBaskets.Hide();
        uiQuestions.Hide();
        uiFirstHint.Hide();
        uiCodePanel.Hide();
}
    protected override void Update() {
        base.Update();
        UIObjective.instance.descriptionText.text = puzzles[currentPuzzleIndex].description;
        UIObjective.instance.objectiveMainText.text = puzzles[currentPuzzleIndex].mainTextString;
        int randint = Random.Range(1,1000);
        if(randint == 1){
    
            SoundManager.Instance.PlaySound2D("caveAmbience");
        }
        if(currentPuzzleIndex >= 2){
            foreach(var questions in puzzles[currentPuzzleIndex].GetComponent<ThirdPuzzle>().questionsList){
                if(questions.available == false){
                    questions.available = true;
                }
            }
        }
        else{
            if(puzzles[currentPuzzleIndex].complete){
                SoundManager.Instance.PlaySound2D("correct");
                currentPuzzleIndex++;
            }
        }

        complete  = puzzles[0].complete && puzzles[1].complete && puzzles[2].complete;
    }
    public override void CheckComplete()
    {
        if(complete){
            OnComplete();
        }
    }

    public void CompleteFirstLevel(){
        GameManager.instance.SwitchDimension();
        // UIManager.instance.ShowUI()
        Destroy(gameObject);
    }
    public override void OnEnable()
    {
        base.OnEnable();
        onComplete += CompleteFirstLevel;
    }
    public override void OnDisable()
    {
        base.OnDisable();
        onComplete += CompleteFirstLevel;
    }
}
