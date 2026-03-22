using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Movement
    public float moveSpeed = 5;
    Rigidbody2D rb;

    [HideInInspector]
    public Vector2 pointerInput, moveDir;
    public float lastHorizontalVector, lastVerticalVector;

    [SerializeField]
    private InputActionReference moveAction, attack, jump;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {



    }

    void FixedUpdate()
    {
        InputManagement();
    }

    void InputManagement()
    {
        if(moveDir.x != 0)
        {
            lastHorizontalVector = moveDir.x;
        }
        if (moveDir.y != 0)
        {
            lastVerticalVector = moveDir.y;
        }
    }
    public void OnMove(InputValue value)
    {
        moveDir = value.Get<Vector2>();
        Debug.Log("onMove");
        rb.linearVelocity = new Vector2(moveDir.x * moveSpeed, moveDir.y * moveSpeed);
    }
    public void OnAttack(InputValue value)
    {

        Debug.Log("onAttack");
    }
    public void OnJump(InputValue value)
    {
        Debug.Log("onJump");
        //rb.linearVelocity = new Vector2(moveDir.x * moveSpeed, moveDir.y * moveSpeed);
    }

}
