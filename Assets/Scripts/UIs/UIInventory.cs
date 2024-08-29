using UnityEngine;
using System.Collections.Generic;
public class UIInventory : UIBase{
    public static UIInventory instance;
    public GameObject ItemSlot;
    public List<GameObject> itemSlots;
    private void Awake() {
        if(instance == null){
            instance = this;
        }
        else{
            Destroy(gameObject);
        }
    }
    public void AddItem(GameObject item){
        GameObject itemSlotClone = Instantiate(ItemSlot,transform);
        itemSlotClone.GetComponent<ItemSlot>().item = item;
        itemSlots.Add(itemSlotClone);
    }
    public void RemoveItem(GameObject item){
        Destroy(item);
        itemSlots.Remove(item);
        GameManager.instance.player2D.currentSlot = 0;
    }
    public void ClearInventory(){
        foreach(GameObject itemSlot in itemSlots){
            Destroy(itemSlot);
        }
        itemSlots.Clear();
    }

}