using UnityEngine;
using System;
public abstract class Objective : MonoBehaviour {
    public bool complete;
    [Header("Objective Type")]
    public ObjectiveType objectiveType;
    [Header("Objective Texts")]
    public string mainTextString;
    public string description;
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
    public virtual void OnEnable(){
        onComplete += Complete;
    }
    public virtual void OnDisable(){
        onComplete -= Complete;
    }
}