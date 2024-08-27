using UnityEngine;
public class TPSCamera : MonoBehaviour
{

    public Transform Target;
    public float MouseSensitivity = 10f;
    private float verticalRotation;
    private float horizontalRotation;


    public Transform camera2DPosition;
    public Vector3 camera2DOffset;
    public Vector3 camera3DPosition;

    public float minX, maxX;
    
    void Update()
    {
        if(GameManager.instance.isPaused){
            return;
        }
        if(!GameManager.instance.is2D){
            transform.localPosition = camera3DPosition;
            Target = GameManager.instance.player3D.transform;

            transform.SetParent(GameObject.FindGameObjectWithTag("Player").transform);

            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            verticalRotation -= mouseY * MouseSensitivity;
            verticalRotation = Mathf.Clamp(verticalRotation, -5f, 20f);

            horizontalRotation = Target.rotation.eulerAngles.y +  mouseX * MouseSensitivity;

            transform.rotation = Quaternion.Euler(verticalRotation, horizontalRotation, 0);
            Target.rotation = Quaternion.Euler(0, horizontalRotation, 0);
        }
        else{
            Target = GameManager.instance.player2D.transform;
            transform.SetParent(camera2DPosition);
            transform.localPosition = new Vector3(Mathf.Clamp(Target.position.x,minX,maxX), camera2DOffset.y, camera2DOffset.z);
            transform.rotation = Quaternion.Euler(0, 0, 0);

        }


    }
}