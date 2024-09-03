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
    // public Transform interactChecker;
    // public LayerMask interactLayer;
    public LayerMask groundLayer;
    public InteractableObject currentInteractableObject;
    // public Collider[] interactables;
    // public Collider2D[] interactables2D;

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
        if(!GameManager.instance.isInteracting){
            PlayerMove();
        }
        if(GameManager.instance.is2D) CycleInventorySlot();

        if(currentInteractableObject != null){
            UIInteract.instance.Show();
            if(Input.GetKeyDown(KeyCode.F)){
                Interact(currentInteractableObject);
            }
        }
        else{
            UIInteract.instance.Hide();
        }
        anim.SetBool(isWalkingKey, isWalking);

    }
    public void PlayerMove(){
        float x = Input.GetAxis("Horizontal");
        float z = !GameManager.instance.is2D ? Input.GetAxis("Vertical") : 0;
        move = transform.right * x + transform.forward * z;
        if(GameManager.instance.is2D){
            
            isWalking = x != 0;
            if(x<0){
                transform.localScale = new Vector3(5,5,5);
            }
            else if(x>0){
                transform.localScale = new Vector3(-5,5,5);
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
        
        
            if(Input.GetKeyDown(KeyCode.Space) && isGrounded() && !isJumping){
                jumpChargeTimer = 0.25f;
                anim.SetTrigger(isJumpingKey);
            }
        
            rb.MovePosition(transform.position  + move * Time.deltaTime * speed);
            
        }
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
        print("Interacted with: " + interactable.gameObject.name);
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
        Gizmos.color = Color.red;
    }

    //--------------------------2D FUNCTIONS--------------------
    public void CycleInventorySlot(){
        if(UIInventory.instance.itemSlots.Count > 0){
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            UIInventory.instance.itemSlots[currentSlot].GetComponent<ItemSlot>().EnableShader();
            if(scroll != 0){
                UIInventory.instance.itemSlots[currentSlot].GetComponent<ItemSlot>().DisableShader();
                if(scroll > 0){
                    currentSlot++;
                    if(currentSlot >= UIInventory.instance.itemSlots.Count){
                        currentSlot = 0;
                    }
                }
                else if(scroll < 0){
                    currentSlot--;
                    if(currentSlot < 0){
                        currentSlot = UIInventory.instance.itemSlots.Count - 1;
                    }
                }
            }
        }
    }


}
