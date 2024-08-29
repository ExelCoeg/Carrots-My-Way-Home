using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    Vector3 move;
    Rigidbody rb;
    Rigidbody2D rb2D;
    Animator anim;
    public bool isDebug;
    [Header("-------MOVEMENT-------")]
    public float speed = 5f;
    public float jumpForce = 5;

    [Header("-------CHECKERS-------")]
    [SerializeField] private float interactRadius;
    [SerializeField] private float groundRadius;
    public Transform groundChecker;
    public Transform interactChecker;
    public LayerMask interactLayer;
    public LayerMask groundLayer;
    public InteractableObject currentInteractableObject;
    public Collider[] interactables;
    public Collider2D[] interactables2D;

    [Header("-------ANIMATIONS-------")]
    public bool isWalking;
    public bool isJumping;

    private string isWalkingKey = "isWalking";
    private string isJumpingKey = "isJumping";
    [Header("-------INVENTORY--------")]
    public int currentSlot;

    public event Action OnJump;

    float jumpChargeTimer = 0;
    private void Awake() {
        anim = GetComponent<Animator>();
    }
    private void Start() {
        currentInteractableObject = null;
        if(GameManager.instance.is2D){
            rb2D = GetComponent<Rigidbody2D>();
        }
        else{
            rb = GetComponent<Rigidbody>();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(isDebug){
            // Debug.Log("isGrounded: " + isGrounded());
            // print("isWalking: " + isWalking);  
        }
        float x = Input.GetAxis("Horizontal");
        float z = !GameManager.instance.is2D ? Input.GetAxis("Vertical") : 0;
        move = transform.right * x + transform.forward * z;
        if(GameManager.instance.is2D){
            CycleInventorySlot();
            
            isWalking = x != 0;
            if(x<0){
                transform.localScale = new Vector3(5,5,5);
                interactChecker.localScale = new Vector3(1,1,1);
            }
            else if(x>0){
                transform.localScale = new Vector3(-5,5,5);
                interactChecker.localScale = new Vector3(-1,1,1);
            }

            
            Collider2D obj = Physics2D.OverlapCircle(interactChecker.position, interactRadius, interactLayer); 
            if(obj != null){
                currentInteractableObject = obj.GetComponent<InteractableObject>();
                currentInteractableObject.enabled = true;
                UIInteract.instance.Show();
                if(Input.GetKeyDown(KeyCode.F) && obj.TryGetComponent(out InteractableObject interactable)){
                    Interact(interactable);
                }
            }
            else{
                UIInteract.instance.Hide();
                currentInteractableObject = null;
            }
            rb2D.MovePosition(transform.position  + move * Time.deltaTime * speed);
            
        }








        //------------------------------------3D------------------------------------
        else{
            if(jumpChargeTimer > 0){
                jumpChargeTimer -= Time.deltaTime;
                if(jumpChargeTimer <= 0){
                    OnJump?.Invoke();
                }
            }

            isWalking = x != 0 || z != 0;
            isJumping = !isGrounded();
            
           
            interactables = Physics.OverlapSphere(interactChecker.position, interactRadius, interactLayer);
            if(interactables.Length > 0){
                if(interactables[0].TryGetComponent(out InteractableObject obj)){
                    currentInteractableObject = obj;
                    currentInteractableObject.enabled = true;
                }
            }
            else{
                currentInteractableObject = null;
            }
            
           if(currentInteractableObject != null){
                UIInteract.instance.Show();
                if(Input.GetKeyDown(KeyCode.F)){
                    Interact(currentInteractableObject);
                }
            }
            else{
                UIInteract.instance.Hide();
            }
            if(Input.GetKeyDown(KeyCode.Space) && isGrounded() && !isJumping){
                jumpChargeTimer = 0.25f;
                anim.SetTrigger(isJumpingKey);
            }
        
            rb.MovePosition(transform.position  + move * Time.deltaTime * speed);
            
        }
        anim.SetBool(isWalkingKey, isWalking);

    }
    public void PlayerJump(){
        if(GameManager.instance.is2D){
            rb2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        else{
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
    public void Interact(InteractableObject interactable){
        interactable.Interacted();
        UIInteract.instance.Hide();
    }
    
    public bool isGrounded(){
        return Physics.OverlapSphere(groundChecker.position, groundRadius,groundLayer).Length > 0;
    }
    private void OnEnable() {
        OnJump += PlayerJump;
    }
    private void OnDisable() {
        OnJump -= PlayerJump;
    }
    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(groundChecker.position, groundRadius);
        Gizmos.DrawWireSphere(interactChecker.position, interactRadius);
        Gizmos.color = Color.red;
    }

    //--------------------------2D FUNCTIONS--------------------
    public void CycleInventorySlot(){
        if(UIInventory.instance.items.Count > 0){
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if(scroll != 0){
                UIInventory.instance.items[currentSlot].GetComponent<ItemSlot>().DisableShader();
                if(scroll > 0){
                    currentSlot++;
                    if(currentSlot >= UIInventory.instance.items.Count){
                        currentSlot = 0;
                    }
                }
                else if(scroll < 0){
                    UIInventory.instance.items[currentSlot].GetComponent<ItemSlot>().DisableShader();
                    currentSlot--;
                    if(currentSlot < 0){
                        currentSlot = UIInventory.instance.items.Count - 1;
                    }
                }
                UIInventory.instance.items[currentSlot].GetComponent<ItemSlot>().EnableShader();
            }
        }
    }


}
