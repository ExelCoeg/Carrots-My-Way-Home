using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutGoldenCarrot : Objective
{
    public Basket basket;
    public override void CheckComplete()
    {
        if(complete){
            OnComplete();
            UIManager.instance.ShowUI(UI.MORAL);
            enabled = false;
        }
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        UIObjective.instance.descriptionText.text = description;
        UIObjective.instance.objectiveMainText.text = mainTextString;
        complete = basket.correct;    
    }
    
}
