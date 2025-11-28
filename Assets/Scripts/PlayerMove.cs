using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float walkspeed;
    Rigidbody2D rb;
    Vector2 inputVec;
    Vector3 movVec;

    private void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        movVec = new Vector3(inputVec.x * walkspeed, 0, 0);
    }
    private void FixedUpdate()
    {
        rb.linearVelocity = movVec;
    }
}
