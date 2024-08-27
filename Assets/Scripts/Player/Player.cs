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
    public float interactRadius;
    public float groundRadius;
    public Transform groundChecker;
    public Transform interactChecker;
    public LayerMask interactLayer;
    public LayerMask groundLayer;
    public InteractableObject currentInteractableObject;
    public Collider[] interactables;

    [Header("-------ANIMATIONS-------")]
    public bool isWalking;
    public bool isJumping;

    private string isWalkingKey = "isWalking";
    private string isJumpingKey = "isJumping";

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
                if(Input.GetKeyDown(KeyCode.F) && obj.TryGetComponent(out InteractableObject interactable)){
                    Interact(interactable);
                }
            }
            rb2D.MovePosition(transform.position  + move * Time.deltaTime * speed);
            
        }
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
            foreach(Collider obj in interactables){
                obj.GetComponent<InteractableObject>().enabled= true;
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
        interactable.Interactted();
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



}
