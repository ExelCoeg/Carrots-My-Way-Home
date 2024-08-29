using UnityEngine;

public class FirstPuzzle : Objective
{
    public string correctAnswer;
    [SerializeField] private UICodePanel uiCodePanel;

    private void Awake() {
        uiCodePanel = GetComponentInParent<FirstLevelPuzzle>().uiCodePanel;    
    }
    public override void CheckComplete()
    {
        uiCodePanel.SetCorrectAnswer(correctAnswer);
        if(complete){
            OnComplete();
        }
    }


    // Update is called once per frame
    protected override void Update()
    {
        
    }
}
