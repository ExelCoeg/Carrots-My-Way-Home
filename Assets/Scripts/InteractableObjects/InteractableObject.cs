using UnityEngine;
using System.Collections;
public abstract class InteractableObject : MonoBehaviour, IInteractable
{
    public bool isDebug = false;
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
            if(isDebug) print("Sprite renderer found");
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
            if(isDebug) print("Disabling outline"); 
            spriteRenderer.material = GameManager.instance.spriteDefaultMaterial2D;
        }
    }
    public void EnableOutline(){
        if(isDebug){
            print("Enabling outline: " + gameObject.name);
        }
        if(!is2DObject){
            outline.SetFloat("_Scale", 1.125f);
        }
        else{
            if(isDebug) print("Enabling outline");
            spriteRenderer.material = GameManager.instance.outlineMaterial2D;
        }
   }
 
   
    public IEnumerator WrongAnswerDelay() {
        // Disable interaction
        GetComponent<Collider2D>().enabled = false;

        // Wait for 2 seconds (or any desired delay)
        yield return new WaitForSeconds(2f);

        // Re-enable interaction
        GetComponent<Collider2D>().enabled = true;
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


