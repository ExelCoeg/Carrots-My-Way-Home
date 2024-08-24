using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    Vector3 move;
    Rigidbody rb;
    public float speed = 5f;
    public float jumpForce = 5;
    public Transform groundChecker;
    public Transform interactChecker;
    public LayerMask interactLayer;
    public LayerMask groundLayer;

    public bool isDebug;
    private void Awake() {
        rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        if(isDebug){
            Debug.Log("isGrounded: " + isGrounded());
        }
        
        move.x = Input.GetAxis("Horizontal");
        if(GameManager.instance.is2D){
            move.z = Input.GetAxis("Vertical");
            move.z = 0;

            Collider2D obj = Physics2D.OverlapCircle(interactChecker.position, 0.1f, interactLayer); 
            if(obj != null){
                if(Input.GetKeyDown(KeyCode.F) && obj.TryGetComponent(out InteractableObject interactable)){
                    Interact(interactable);
                }
            }
        }
        else{
            move.z = Input.GetAxis("Vertical");
            Collider[] obj = Physics.OverlapSphere(interactChecker.position, 0.1f, interactLayer); 
            if(obj.Length > 0){
                if(Input.GetKeyDown(KeyCode.F) && obj[0].TryGetComponent(out InteractableObject interactable)){
                    Interact(interactable);
                }
            }
        }


        if(Input.GetKeyDown(KeyCode.Space) && isGrounded()){
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        rb.MovePosition(transform.position  + move * Time.deltaTime * speed);


    }
    public void Interact(InteractableObject interactable){
        interactable.Interactted();
    }
    public bool isGrounded(){
        return Physics.OverlapSphere(groundChecker.position, 0.1f,groundLayer) != null;
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(interactChecker.position, 0.1f);
        Gizmos.color = Color.red;
    }
}
