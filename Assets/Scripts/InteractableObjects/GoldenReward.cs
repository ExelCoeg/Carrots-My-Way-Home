using DG.Tweening;
using UnityEngine;

public class GoldenReward :InteractableObject
{
    public bool interacted;
    public GameObject goldenCarrot;
    public override void Interacted()
    {
        interacted = true;
        SoundManager.Instance.StopSound2D();
        GetComponent<Collider2D>().enabled = false;
        UIInventory.instance.AddItem(goldenCarrot);
        enabled = false;
            
        ObjectiveManager.instance.NextObjective();
    }
    public void Animate(Transform endPos){
        transform.DOMoveY(endPos.position.y,1f);
    }
}