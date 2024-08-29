using UnityEngine;

public class Inventory : MonoBehaviour{
   
    public void AddItem(GameObject item){
        UIInventory.instance.AddItem(item);
    }
    public void ClearInventory(){
        UIInventory.instance.ClearInventory();
    }
}
