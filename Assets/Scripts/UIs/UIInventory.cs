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
    private void Start() {
        transform.localScale = Vector3.zero;
    }
    private void Update() {
        if(UIManager.instance.currentUI == UI.QUESTIONS){
            GetComponent<GridLayoutGroup>().childAlignment = TextAnchor.MiddleCenter; 
            ChangeAnchorPreset(new Vector2(0.5f,1), new Vector2(0.5f,1), new Vector2(0.5f,0.5f));
            StartCoroutine(AnimateCenter());
        }
        else{
            ChangeAnchorPreset(new Vector2(0,1), new Vector2(0,1),new Vector2(0,1));
            GetComponent<GridLayoutGroup>().childAlignment = TextAnchor.UpperLeft;
            StartCoroutine(AnimateUpperLeft());

        }
    }
    [ContextMenu("Show Inventory")]
    public override void Show()
    {
        base.Show();
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
    public IEnumerator AnimateCenter(){
        yield return rectTransform.DOAnchorPos(new Vector3(0,-75f),0.25f).WaitForCompletion();
        transform.DOScale(1.5f,0.25f);
    }
    public IEnumerator AnimateUpperLeft(){
        yield return rectTransform.DOAnchorPos(Vector3.zero,0.25f).WaitForCompletion();
        transform.DOScale(1f,0.25f);

    }

}