using UnityEngine;
using UnityEngine.UI;

public class Question : InteractableObject
{
    
    public bool correct;
    [SerializeField] private Item correctCarrot;
    [SerializeField] private Item inputCarrot;
    public Sprite questionSprite;
    public bool available = false;
    private void Update() {
        inputCarrot = UIInventory.instance.itemSlots.Count > 0 ? UIInventory.instance.itemSlots[GameManager.instance.player2D.currentSlot]//
        .GetComponent<ItemSlot>().item.GetComponent<Item>() : null;
    }
    public override void Interacted()
    {

        if(available){
            UIManager.instance.ShowUI(UI.QUESTIONS);
            UIQuestions.instance.GetComponent<Image>().sprite = questionSprite;
            UIQuestions.instance.currentQuestion = this;
        }
        else{
            UIManager.instance.ShowMessage("I need to find complete the previous puzzle first...");
        }
    }
    
    public void CheckAnswer(){
        if(correctCarrot == inputCarrot){
            correct = true;
            GetComponent<Collider2D>().enabled = false;
            UIInventory.instance.RemoveItem(UIInventory.instance.itemSlots[GameManager.instance.player2D.currentSlot]);
        }
        else{
            UIManager.instance.ShowMessage("Incorrect answer...");
        }
        UIManager.instance.HideUI(UI.QUESTIONS);
            
        
    }
}
