using UnityEngine;

public class CharacterMover : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float jumpForce = 5f;
    private CharacterController CController;
    Vector3 MoveDirection;
    private float moveX;
    private float moveY;
    private float moveZ;
    private float jumpImput;
    private float gravity;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CController = GetComponent<CharacterController>();
        gravity = Physics.gravity.y;
    }

    // Update is called once per frame
    void Update()
    {
        moveY = 0f;
        moveX = Input.GetAxisRaw("Horizontal");
        moveZ = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space))
            moveY = 1;
     
        Debug.Log("Move X: " + moveX + " Y: " + moveY + " Z: " + moveZ);
        
        MoveDirection = new Vector3(moveX * moveSpeed, moveY * jumpForce + gravity, moveZ  * moveSpeed);
        Debug.Log(MoveDirection);
    }

    void FixedUpdate()
    {
        CController.Move(MoveDirection);
    }
}
