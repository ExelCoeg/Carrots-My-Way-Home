using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class RewardColor : InteractableObject
{
    public List<GameObject> carrots;
    public override void Interacted()
    {
        UIInventory.instance.AddItem(carrots[0]);
        UIInventory.instance.AddItem(carrots[1]);
        UIInventory.instance.AddItem(carrots[2]);
        UIInventory.instance.AddItem(carrots[3]);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Animate(Transform endPos){
        transform.DOMoveY(endPos.position.y,1f);
    }
}
