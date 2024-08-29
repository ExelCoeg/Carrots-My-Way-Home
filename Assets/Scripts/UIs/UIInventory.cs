using UnityEngine;
using System.Collections.Generic;
public class UIInventory : UIBase{
    public static UIInventory instance;
    public GameObject ItemSlot;
    private void Awake() {
        if(instance == null){
            instance = this;
        }
        else{
            Destroy(gameObject);
        }
    }
    public List<GameObject> items;
    public List<GameObject> itemSlots;

    public void AddItem(GameObject item){
        GameObject itemSlotClone = Instantiate(ItemSlot,transform);
        itemSlotClone.GetComponent<ItemSlot>().item = item;
        items.Add(item);
        itemSlots.Add(itemSlotClone);
    }
    public void ClearInventory(){
        foreach(GameObject item in items){
            Destroy(item);
        }
        items.Clear();
    }

}