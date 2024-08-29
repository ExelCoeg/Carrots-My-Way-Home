using UnityEngine;

public class TPSCamera : MonoBehaviour
{
    public Transform Target;
    public float MouseSensitivity = 10f;
    private float verticalRotation;
    private float horizontalRotation;

    public Transform camera2DPosition;
    public Vector3 camera2DOffset;
    public Vector3 camera3DOffset;

    public float minX, maxX;
    public float minY = -10f, maxY = 80f;  // Clamping vertical angle

    public float distanceFromTarget = 5f;
    public float minDistance = 2f;
    public float maxDistance = 10f;
    public float zoomSpeed = 2f;

    public float rotationSpeed = 5f; // Speed of character rotation
    public LayerMask groundLayer;    // Layer for ground detection
    public float cameraCollisionOffset = 0.2f; // Offset for collision correction

    public float heightOffset = 2f;  // Additional height for the camera

    void Update()
    {
        if (GameManager.instance.isPaused)
        {
            return;
        }

        if (!GameManager.instance.is2D)
        {
            Target = GameManager.instance.player3D.transform;
            transform.SetParent(GameObject.FindGameObjectWithTag("Player").transform);

            float mouseX = Input.GetAxis("Mouse X") * MouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * MouseSensitivity;

            horizontalRotation += mouseX;
            verticalRotation -= mouseY;

            verticalRotation = Mathf.Clamp(verticalRotation, minY, maxY);

            // Handle Zoom
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            distanceFromTarget -= scroll * zoomSpeed;
            distanceFromTarget = Mathf.Clamp(distanceFromTarget, minDistance, maxDistance);

            Quaternion rotation = Quaternion.Euler(verticalRotation, horizontalRotation, 0);
            Vector3 desiredPosition = Target.position - rotation * Vector3.forward * distanceFromTarget + camera3DOffset;

            // Apply the height offset
            desiredPosition.y += heightOffset;

            // Collision detection using raycasting
            RaycastHit hit;
            if (Physics.Raycast(Target.position, desiredPosition - Target.position, out hit, distanceFromTarget + cameraCollisionOffset, groundLayer))
            {
                // If the camera is too close to the ground, zoom it in towards the player
                distanceFromTarget = hit.distance - cameraCollisionOffset;
                desiredPosition = Target.position - rotation * Vector3.forward * distanceFromTarget + camera3DOffset;

                // Apply the height offset again after zooming in
                desiredPosition.y += heightOffset;
            }

            transform.position = desiredPosition;
            transform.LookAt(Target.position + Vector3.up * heightOffset);

            // Check if any movement key is pressed
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) ||
                Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                // Calculate the target rotation based on the camera's forward direction
                Quaternion targetRotation = Quaternion.Euler(0, horizontalRotation, 0);

                // Smoothly rotate the character to face the camera's forward direction
                Target.rotation = Quaternion.Slerp(Target.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
        else
        {
            Target = GameManager.instance.player2D.transform;
            transform.SetParent(camera2DPosition);
            transform.localPosition = new Vector3(Mathf.Clamp(Target.position.x, minX, maxX), camera2DOffset.y, camera2DOffset.z);
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}