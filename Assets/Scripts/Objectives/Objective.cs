using UnityEngine;
using System;
public abstract class Objective : MonoBehaviour {
    public ObjectiveType objectiveType;
    public string mainTextString;
    public string description;
    public bool complete;
    public event Action onComplete;
   
    public abstract void CheckComplete();
    protected virtual void Update() {
        CheckComplete();
        UIObjective.instance.descriptionText.text = description;
        UIObjective.instance.objectiveMainText.text = mainTextString;
    }
    public void OnComplete(){
        onComplete?.Invoke();
    }
    public void Complete(){
        UIManager.instance.ShowMessage("Objective Complete!");
        ObjectiveManager.instance.NextObjective();
        Destroy(gameObject);
    }
    public void OnEnable(){
        onComplete += Complete;
    }
    public void OnDisable(){
        onComplete -= Complete;
    }
}