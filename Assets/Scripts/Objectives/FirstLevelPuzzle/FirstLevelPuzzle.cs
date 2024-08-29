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
    public UIFirstHint uiFirstHint;
    public UICodePanel uiCodePanel;

    [Header("Puzzles")]
    public List<Objective> puzzles;
    public int currentPuzzleIndex = 0;
    private void Start() {
        uiFirstHint = Instantiate(uiFirstHintPrefab,UIManager.instance.canvas);
        uiCodePanel = Instantiate(uiCodePanelPrefab,UIManager.instance.canvas);
        uiFirstHint.Hide();
        uiCodePanel.Hide();
}
    protected override void Update() {
        base.Update();
        if(puzzles[currentPuzzleIndex].complete){
            currentPuzzleIndex++;
            mainTextString = puzzles[currentPuzzleIndex].mainTextString;
            description = puzzles[currentPuzzleIndex].description;
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
