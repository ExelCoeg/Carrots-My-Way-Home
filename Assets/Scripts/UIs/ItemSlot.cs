using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot: MonoBehaviour{
    [SerializeField] private Image itemImage;
    public GameObject selectedShader;
    public GameObject item;
    private void Start() {
        itemImage.DOFade(1f,1f);
    }
    private void Update() {
        if(item != null){
            itemImage.enabled = true;
            itemImage.sprite = item.GetComponent<SpriteRenderer>().sprite;
        }
        else{
            itemImage.enabled = false;
        }
    }
    public void Remove(){
        UIInventory.instance.itemSlots.Remove(gameObject);
        Destroy(gameObject);
    }
    public void EnableShader(){
        selectedShader.SetActive(true);
    }
    public void DisableShader(){
        selectedShader.SetActive(false);
    }
    
}