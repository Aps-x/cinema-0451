using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;      
    [SerializeField] Transform orientation;      
    [SerializeField] Rigidbody rb;               

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    void Start()
    {
        rb.freezeRotation = true; 
    }

    void Update()
    {
        GetInput();
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void GetInput()
    {
        // Get player input
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    void MovePlayer()
    {
        // Calculate movement direction based on input and orientation
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        moveDirection.Normalize();

        // Set the player's velocity directly, maintaining gravity
        Vector3 velocity = moveDirection * moveSpeed;
        velocity.y = rb.velocity.y;  // Preserve current y velocity to maintain gravity

        // Apply the velocity to the Rigidbody
        rb.velocity = velocity;
    }
}
