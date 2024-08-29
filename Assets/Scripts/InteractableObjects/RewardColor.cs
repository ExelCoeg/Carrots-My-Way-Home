using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Reward : InteractableObject
{
    public List<GameObject> carrots;
    public override void Interacted()
    {
        UIInventory.instance.AddItem(carrots[2]);
        UIInventory.instance.AddItem(carrots[1]);
        UIInventory.instance.AddItem(carrots[3]);
        UIInventory.instance.AddItem(carrots[0]);
    }


    public void Animate(Transform endPos){
        transform.DOMoveY(endPos.position.y,1f);
    }
}
