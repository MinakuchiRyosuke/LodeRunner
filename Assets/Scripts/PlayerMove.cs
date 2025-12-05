using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float walkSpeed;
    [SerializeField] private float gridSize;
    Rigidbody2D rb;
    Vector2 inputVec;
    Vector3 movVec;
    private bool isMoving = false;

    private void OnMove(InputValue value)
    {
        if (isMoving) return;
        inputVec = value.Get<Vector2>();
        Vector2 targetPos = transform.position;
        targetPos += inputVec;
        StartCoroutine(Move(targetPos));
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        
    }
    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;

        while((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, walkSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPos;
        isMoving = false;
    }
}
