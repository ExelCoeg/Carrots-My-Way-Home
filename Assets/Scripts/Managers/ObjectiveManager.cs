using System.Collections.Generic;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour

{
    public static ObjectiveManager instance;
    public Transform objectiveParent;
    public int currentObjectiveIndex;
    public List<Objective> objectives;
    public Objective currentObjective;
    private void Awake() {
        if(instance == null){
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }
    private void Start() {
        Instantiate(objectives[currentObjectiveIndex],new Vector3(0,1.5f,0),Quaternion.identity,objectiveParent);
    }
    private void Update() {
        if(currentObjectiveIndex >= objectives.Count){
            return;
        }
        currentObjective = UIObjective.instance.objective =  objectives[currentObjectiveIndex];
    }
    public void NextObjective(){
        currentObjectiveIndex++;
        if(currentObjectiveIndex >= objectives.Count){
            print("All objectives completed!");
            return;
        }
        Instantiate(objectives[currentObjectiveIndex],objectiveParent.position,Quaternion.identity,objectiveParent);
    }
}

public enum ObjectiveType{
    FIND_CARROTS,
    COMPLETE_PUZZLE,
    FIRST_FIRSTPUZZLE,
    FIRST_SECONDPUZZLE,
    FIRST_THIRDPUZZLE
}