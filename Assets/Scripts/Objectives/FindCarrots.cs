using System.Collections;
using UnityEngine;

public class FindCarrots : Objective
{

    [Header("Carrot Informations")]
    int carrotsToFind = 5;
    public BasketFindCarrots basket;
    public int currentCarrots;
    private void Start() {
        mainTextString = "Collect Carrots (" + currentCarrots + "/" + carrotsToFind + ")";
        description = "Find all the carrots in the garden";
    }
    private new void Update() {
        base.Update();
        UIObjective.instance.descriptionText.text = description;
        UIObjective.instance.objectiveMainText.text = mainTextString;
        mainTextString = "Collect Carrots (" + currentCarrots + "/" + carrotsToFind + ")";
    }

    public override void CheckComplete()
    {   
        if(complete){
            ObjectiveManager.instance.NextObjective();
            OnComplete();

        }
    }
    public override void OnEnable(){
        base.OnEnable();
        onComplete += Destroy;
    }
    public override void OnDisable()
    {
        base.OnDisable();
        onComplete -= Destroy;
    }
    public void Destroy(){
        Destroy(gameObject);
    }
    public int GetCarrotsToFind(){
        return carrotsToFind;
    }   
}
