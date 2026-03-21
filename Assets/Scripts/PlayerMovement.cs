using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Movement
    public float moveSpeed = 5;
    Rigidbody2D rb;

    Vector2 pointerInput, moveDir;

    [SerializeField]
    private InputActionReference moveAction, attack, jump;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        InputManagement();

    }

    private void FixedUpdate()
    {

    }

    void InputManagement()
    {

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
