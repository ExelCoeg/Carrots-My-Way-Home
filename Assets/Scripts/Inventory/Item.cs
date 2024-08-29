using UnityEngine;
using System.Collections.Generic;
public class Item: MonoBehaviour{

    public string itemName;
    private void Update() {
        Collider2D[] objs = Physics2D.OverlapCircleAll(transform.position, 0.25f);
        if(objs.Length > 0){
            foreach(Collider2D obj in objs){
                if(obj.gameObject.CompareTag("Player")){
                    obj.GetComponent<Inventory>().AddItem(gameObject);
                    gameObject.SetActive(false);
                    Destroy(gameObject);
                }
            }
        }
    }
}