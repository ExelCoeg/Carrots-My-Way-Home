using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class UIInventory : UIBase{
    public static UIInventory instance;
    public GameObject ItemSlot;
    public List<GameObject> itemSlots;
    public override void Awake() {
        base.Awake();
        if(instance == null){
            instance = this;
        } else {
            Destroy(gameObject);
        }
    
    }
    // private void Start() {
    //     transform.localScale = Vector3.zero;
    // }
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

    [ContextMenu("Animate Center")]
    public void AnimateInventoryToCenter(){
        StartCoroutine(AnimateCenter());
    }
    [ContextMenu("Animate UpperLeft")]
    public void AnimateInventoryToUpperLeft(){
        StartCoroutine(AnimateUpperLeft());
    }
    
    public IEnumerator AnimateCenter(){
        print("Animating to center");
        GetComponent<GridLayoutGroup>().childAlignment = TextAnchor.MiddleCenter; 
        ChangeAnchorPreset(new Vector2(0.5f,1), new Vector2(0.5f,1), new Vector2(0.5f,0.5f));
        rectTransform.anchoredPosition = new Vector2(0,75f);
        yield return rectTransform.DOAnchorPos(new Vector3(0,-75f),0.25f).WaitForCompletion();
        yield return transform.DOScale(1.5f,0.5f).WaitForCompletion();
    }
    public IEnumerator AnimateUpperLeft(){
        print("Animating to UpperLeft");
        yield return transform.DOScale(1f,0.25f).WaitForCompletion();
        yield return rectTransform.DOAnchorPos(new Vector2(0,75f),0.5f).WaitForCompletion();
        GetComponent<GridLayoutGroup>().childAlignment = TextAnchor.UpperLeft;
        ChangeAnchorPreset(new Vector2(0,1), new Vector2(0,1),new Vector2(0,1));
        rectTransform.anchoredPosition = new Vector2(-400f,0f);
        yield return rectTransform.DOAnchorPos(Vector2.zero,0.5f).WaitForCompletion();
    }

}