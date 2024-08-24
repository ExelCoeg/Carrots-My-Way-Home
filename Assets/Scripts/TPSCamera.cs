using UnityEngine;
public class TPSCamera : MonoBehaviour
{

    public Transform Target;
    public float MouseSensitivity = 10f;
    private float verticalRotation;
    private float horizontalRotation;

    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        verticalRotation -= mouseY * MouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -20f, 20f);

        horizontalRotation = Target.rotation.eulerAngles.y +  mouseX * MouseSensitivity;

        transform.rotation = Quaternion.Euler(verticalRotation, horizontalRotation, 0);
        Target.rotation = Quaternion.Euler(0, horizontalRotation, 0);
    }
}