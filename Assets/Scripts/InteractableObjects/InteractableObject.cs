using System;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour, IInteractable
{
    Material outline;
    public event Action OnInteracted;

    public bool is2DObject;
    
    MeshRenderer meshRenderer;
    SpriteRenderer spriteRenderer;

    [Header("Interactable Object 2D")]
    public float detectRadius = 2f;
    public abstract void Interactted(); 
    private void Awake() {
        if(TryGetComponent<MeshRenderer>(out MeshRenderer meshRenderer)){
            this.meshRenderer = meshRenderer;
            outline = meshRenderer.materials[1];
        }
        else if(TryGetComponent<SpriteRenderer>(out SpriteRenderer spriteRenderer)){
            this.spriteRenderer = spriteRenderer;
            
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
            spriteRenderer.material = GameManager.instance.spriteDefaultMaterial2D;
        }
    }
    public void EnableOutline(){
        // print("Enabling outline");
        if(!is2DObject){
            outline.SetFloat("_Scale", 1.125f);
        }
        else{
            spriteRenderer.material = GameManager.instance.outlineMaterial2D;
        }
   }
    public void RaiseOnInteracted(){
        OnInteracted?.Invoke();
    }
    public void _OnInteractted(){
        print("Player interactted with " + gameObject.name);
    }
    public void OnEnable(){
        OnInteracted += _OnInteractted;
    }
    public void OnDisable(){
        OnInteracted -= _OnInteractted;
    }
    private void Update() {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(!is2DObject){
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if(distance >= 2f){
                player.GetComponent<Player>().currentInteractableObject = null; 
                DisableOutline();
                enabled = false;
            }
            else{
                player.GetComponent<Player>().currentInteractableObject = this; 
                EnableOutline();
            }
        }
        else{
            Collider2D obj = Physics2D.OverlapCircle(transform.position, detectRadius, LayerMask.GetMask("Player"));
            if(obj != null){
                player.GetComponent<Player>().currentInteractableObject = this;
                EnableOutline();
            }
            else{
                player.GetComponent<Player>().currentInteractableObject = null;
                DisableOutline();
                enabled = false;
            }
        }
        
    }
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectRadius);
    }
}

