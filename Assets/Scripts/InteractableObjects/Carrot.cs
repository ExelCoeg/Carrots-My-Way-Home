using UnityEngine;

public class Carrot : InteractableObject
{
    FindCarrots objective;
   
    public override void Interacted()
    {   
        SoundManager.Instance.PlaySound3D("carrotPickUp",transform.position);
        objective = GetComponentInParent<FindCarrots>();
        if(ObjectiveManager.instance.currentObjective.objectiveType == ObjectiveType.FIND_CARROTS){
            if(gameObject.name == "GoldenCarrot"){
                if(objective.currentCarrots < objective.GetCarrotsToFind()-1){
                    print("You need to collect more carrots!");
                    UIManager.instance.ShowMessage("You need to collect more carrots!");
                    return;
                }
                else{
                    objective.complete = true;
                    GameManager.instance.OnSwitchDimension();

                }
            }
            else{
                objective.currentCarrots++;
                objective.basket.currentIndex++;
            }
            Destroy(gameObject);
        }
    }
}
