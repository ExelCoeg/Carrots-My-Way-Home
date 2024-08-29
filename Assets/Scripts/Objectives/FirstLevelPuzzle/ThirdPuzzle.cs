using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPuzzle : Objective
{
    [SerializeField] private List<Question> questionsList;
    public override void CheckComplete()
    {
        if(complete){
            OnComplete();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        complete = questionsList[0].correct && questionsList[1].correct && questionsList[2].correct;
    }
}
