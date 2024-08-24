using System;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour, IInteractable
{
    public abstract void Interactted();
}
