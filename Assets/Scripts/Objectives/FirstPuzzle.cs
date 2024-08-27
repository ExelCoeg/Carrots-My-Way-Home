using System.Collections.Generic;
using UnityEngine;

public class FirstPuzzle : Objective
{
    public List<InteractableObject> interactableObjects;
    
    public UIFirstHint uiFirstHintPrefab;
    public UIFirstHint uiFirstHint;

    public bool puzzleComplete;
    private void Start() {
        uiFirstHint = Instantiate(uiFirstHintPrefab,UIManager.instance.canvas);
        uiFirstHint.Hide();
    }
    public override void CheckComplete()
    {
        if(puzzleComplete){
            OnComplete();
        }
    }
}
