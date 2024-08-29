using UnityEngine;

public class FruitBasket : InteractableObject
{
    [SerializeField] private GameObject correctFruit;
    [SerializeField] private GameObject fruitInput;
    [SerializeField] private GameObject currentFruit;
    public bool correct;
    public override void Interacted()
    {
        currentFruit = fruitInput;
        if(fruitInput.GetComponent<Item>().itemName == correctFruit.GetComponent<Item>().itemName){
            correct = true;
        }
    }
}
