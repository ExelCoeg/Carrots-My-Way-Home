using UnityEngine;

public class FruitBasket : InteractableObject
{
    public bool correct;
    [SerializeField] private Item correctFruit;
    [SerializeField] private Item fruitInput;
    [SerializeField] private SpriteRenderer placeholder;
   
    public void Update() {
        fruitInput = UIInventory.instance.itemSlots.Count > 0 ? UIInventory.instance.itemSlots[GameManager.instance.player2D.currentSlot]//
        .GetComponent<ItemSlot>().item.GetComponent<Item>() : null;
    }
    public override void Interacted()
    {
        if(fruitInput != null){
            if(fruitInput.itemName == correctFruit.itemName){
                placeholder.sprite = correctFruit.GetComponent<SpriteRenderer>().sprite;
                correct = true;
                GetComponent<Collider2D>().enabled = false;
                UIInventory.instance.RemoveItem(UIInventory.instance.itemSlots[GameManager.instance.player2D.currentSlot]);
            }
            else{
                UIManager.instance.ShowMessage("Something doesn't quite feel right...");
            }
        }
        else{
            UIManager.instance.ShowMessage("I need to pick up a fruit first...");
        }
    }
}
