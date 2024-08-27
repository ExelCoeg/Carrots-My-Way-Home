using System.Collections;
using UnityEngine;

public class FindCarrots : Objective
{

    [Header("Carrot Informations")]
    int carrotsToFind = 5;
    public int currentCarrots;
 
    private new void Update() {
        base.Update();
        mainTextString = "Collect Carrots (" + currentCarrots + "/" + carrotsToFind + ")";
    }
    public override void CheckComplete()
    {   
        if(currentCarrots >= carrotsToFind){
            OnComplete();
        }
    }
    public int GetCarrotsToFind(){
        return carrotsToFind;
    }   
}
