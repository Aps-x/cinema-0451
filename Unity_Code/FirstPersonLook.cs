using UnityEngine;

public class FirstPersonLook : MonoBehaviour
{
    [SerializeField] float sensitivity;
    [SerializeField] Transform orientation;

    float xRotation;
    float yRotation;

    const float MIN_X_ROTATION = -90f;
    const float MAX_X_ROTATION = 90f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * sensitivity;
        float mouseY = Input.GetAxisRaw("Mouse Y") * sensitivity;

        yRotation += mouseX;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, MIN_X_ROTATION, MAX_X_ROTATION);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}