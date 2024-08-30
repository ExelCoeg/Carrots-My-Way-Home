using UnityEngine;
using UnityEngine.UI;

public class FruitBasket : InteractableObject
{
    public bool correct;
    [SerializeField] private Item correctFruit;
    [SerializeField] private Item fruitInput;
    [SerializeField] private SpriteRenderer placeholder;
    public Sprite fruitBasketSprite;
   
    public void Update() {
        fruitInput = UIInventory.instance.itemSlots.Count > 0 ? UIInventory.instance.itemSlots[GameManager.instance.player2D.currentSlot]//
        .GetComponent<ItemSlot>().item.GetComponent<Item>() : null;
    }
    public override void Interacted()
    {
        if(UIInventory.instance.itemSlots.Count > 0) {
            UIManager.instance.ShowUI(UI.FRUITBASKETS);
            UIFruitBaskets.instance.GetComponent<Image>().sprite = fruitBasketSprite;
            UIFruitBaskets.instance.currentFruitBasket = this;

        }
        else{
            UIManager.instance.ShowMessage("I need to pick up the carrots first...");
        }
        
    }
    public void CheckAnswer(){
        if(fruitInput.itemName == correctFruit.itemName){
            SoundManager.Instance.PlaySound2D("carrotPutBasket");
            placeholder.sprite = correctFruit.GetComponent<SpriteRenderer>().sprite;
            correct = true;
            GetComponent<Collider2D>().enabled = false;
            UIInventory.instance.RemoveItem(UIInventory.instance.itemSlots[GameManager.instance.player2D.currentSlot]);
            UIManager.instance.ShowMessage("This looks pretty right...");
        }
        else{
            UIManager.instance.ShowMessage("Something doesn't quite feel right...");
        }
        UIManager.instance.HideUI(UI.FRUITBASKETS);
    }
}
