using UnityEngine;

public abstract class InteractableObject : MonoBehaviour, IInteractable
{
    Material outline;
    MeshRenderer meshRenderer;
    SpriteRenderer spriteRenderer;
    public bool is2DObject;
    
    public abstract void Interacted(); 
    public void Awake() {
        if(TryGetComponent<MeshRenderer>(out MeshRenderer meshRenderer)){
            this.meshRenderer = meshRenderer;
            outline = meshRenderer.materials[1];
        }
        else if(TryGetComponent<SpriteRenderer>(out SpriteRenderer spriteRenderer)){
            this.spriteRenderer = spriteRenderer;
            print("Sprite renderer found");
        }
       
    }
    private void Start() {
        DisableOutline();        
    }
   public void DisableOutline(){
        if(!is2DObject){
            outline.SetFloat("_Scale", 0f);
        }
        else{
            print("Disabling outline"); 
            spriteRenderer.material = GameManager.instance.spriteDefaultMaterial2D;
        }
    }
    public void EnableOutline(){
        print("Enabling outline: " + gameObject.name);
        if(!is2DObject){
            outline.SetFloat("_Scale", 1.125f);
        }
        else{
            print("Enabling outline");
            spriteRenderer.material = GameManager.instance.outlineMaterial2D;
        }
   }
 
   
   
   
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")){
            EnableOutline();
            other.GetComponent<Player>().currentInteractableObject = this;
        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("Player")){
            DisableOutline();
            other.GetComponent<Player>().currentInteractableObject = null;
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            EnableOutline();
            other.GetComponent<Player>().currentInteractableObject = this;
        }

    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player")){
            DisableOutline();
            other.GetComponent<Player>().currentInteractableObject = null;
        }
    }
}


