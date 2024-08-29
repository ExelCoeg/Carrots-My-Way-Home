using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondPuzzle : Objective
{
    // public List<Carrots2D> carrots2Ds;
    public List<GameObject> carrots;
    public List<FruitBasket> fruitBaskets;
    
    public override void CheckComplete()
    {
        if(complete){
            OnComplete();
        }
    }
    protected override void Update()
    {
        if(Input.GetKeyDown(KeyCode.K)){
            UIInventory.instance.AddItem(carrots[0]);
            UIInventory.instance.AddItem(carrots[1]);
            UIInventory.instance.AddItem(carrots[2]);
            UIInventory.instance.AddItem(carrots[3]);
        }
        complete = fruitBaskets[0].correct && fruitBaskets[1].correct && //
        fruitBaskets[2].correct && fruitBaskets[3].correct;
    }
}
