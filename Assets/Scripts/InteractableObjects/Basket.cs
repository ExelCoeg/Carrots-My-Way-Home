using System.Collections.Generic;
using UnityEngine;

public class Basket : InteractableObject
{
    public GameObject goldenCarrot;
    public bool correct;

    public override void Interacted()
    {
        goldenCarrot.SetActive(true);
        correct = true;
    }

}
