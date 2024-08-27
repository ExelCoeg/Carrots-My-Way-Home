using UnityEngine;

public class Carrot : InteractableObject
{
    FindCarrots objective;
   
    public override void Interactted()
    {   
        objective = GetComponentInParent<FindCarrots>();
        if(ObjectiveManager.instance.currentObjective.objectiveType == ObjectiveType.FIND_CARROTS){
           if(gameObject.name == "GoldenCarrot"){
            print("Golden Carrot");
            if(objective.currentCarrots < objective.GetCarrotsToFind()-1){
                print("You need to collect more carrots!");
                 UIManager.instance.ShowMessage("You need to collect more carrots!");
                return;
            }
            else{
                GameManager.instance.OnSwitchDimension();
                // UIManager.instance.ShowMessage("Switching to 2D mode");
            }
           }
            RaiseOnInteracted();
            objective.currentCarrots++;
        }
    }
}
