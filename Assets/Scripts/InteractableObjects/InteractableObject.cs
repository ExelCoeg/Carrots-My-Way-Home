using UnityEngine;

public abstract class InteractableObject : MonoBehaviour, IInteractable
{
    Material outline;

    public bool is2DObject;
    
    MeshRenderer meshRenderer;
    SpriteRenderer spriteRenderer;

    [Header("Interactable Object 2D")]
    public float detectRadius = 2f;
    public abstract void Interacted(); 
    private void Awake() {
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
        // print("Enabling outline");
        if(!is2DObject){
            outline.SetFloat("_Scale", 1.125f);
        }
        else{
            print("Enabling outline");
            spriteRenderer.material = GameManager.instance.outlineMaterial2D;
        }
   }
    protected virtual void Update() {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        print("test: " + gameObject.name );
        if(!is2DObject){
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if(distance >= 2f){
                DisableOutline();
                enabled = false;
            }
            else{
                EnableOutline();
            }
        }
        else{
            Collider2D obj = Physics2D.OverlapCircle(transform.position, detectRadius, LayerMask.GetMask("Player"));
            if(obj != null){
                EnableOutline();
            }
            else{
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


