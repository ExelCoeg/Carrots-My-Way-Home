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

    public UIFirstHint uiFirstHint;
    public UICodePanel uiCodePanel;
    public UIQuestions uiQuestions;

    [Header("Puzzles")]
    public List<Objective> puzzles;


    public int currentPuzzleIndex = 0;
    private void Start() {
        uiFirstHint = Instantiate(uiFirstHintPrefab,UIManager.instance.canvas);
        uiCodePanel = Instantiate(uiCodePanelPrefab,UIManager.instance.canvas);
        uiQuestions = Instantiate(uiQuestionsPrefab,UIManager.instance.canvas);
        uiQuestions.Hide();
        uiFirstHint.Hide();
        uiCodePanel.Hide();
}
    protected override void Update() {
        base.Update();
        UIObjective.instance.descriptionText.text = puzzles[currentPuzzleIndex].description;
            UIObjective.instance.objectiveMainText.text = puzzles[currentPuzzleIndex].mainTextString;
        if(currentPuzzleIndex >= 2){
            foreach(var questions in puzzles[currentPuzzleIndex].GetComponent<ThirdPuzzle>().questionsList){
                if(questions.available == false){
                    questions.available = true;
                }
            }
        }
        else{
            if(puzzles[currentPuzzleIndex].complete){
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


    
}
