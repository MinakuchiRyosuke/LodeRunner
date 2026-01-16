using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float gridSize = 1f; // 1マスの大きさ

    private Vector2 currentInput; // 入力を保持する変数
    private bool isMoving = false;
    private bool isLeftDig = false;
    private bool isRightDig = false;
    private bool isLadder = false;
    private bool isRod = false;

    // Rigidbody2Dは物理移動しないなら不要ですが、当たり判定用に残すならKinematic推奨
    // Rigidbody2D rb; 

    private void Awake()
    {
        // rb = GetComponent<Rigidbody2D>();
    }

    // Input Systemのイベント（ボタンを押した/離した時に呼ばれる）
    private void OnMove(InputValue value)
    {
        currentInput = value.Get<Vector2>();
    }

    private void OnLeftDig(InputValue value)
    {
        isLeftDig = true;
    }

    private void OnRightDig(InputValue value)
    {
        isRightDig = true;
    }

    void Update()
    {
        if (isLeftDig)
        {
            
        }
        else if (isRightDig)
        {

        }

        // 1. 移動中ではなく、かつ入力がある場合に移動を開始する
        if (!isMoving && currentInput != Vector2.zero)
        {
            if(!isLadder)
            {
                currentInput.y = 0;
            }
            // 2. 斜め移動を禁止する（グリッド移動の定石）
            // 横入力があるときは、縦入力を無視する（またはその逆）
            if (Mathf.Abs(currentInput.x) > 0)
            {
                currentInput.y = 0;
            }

            // 3. 次の目的地を計算（グリッドサイズを考慮）
            // 入力は0か1か-1になるように正規化したり、四捨五入すると安全です
            Vector3 direction = new Vector3(Mathf.Round(currentInput.x), Mathf.Round(currentInput.y), 0);
            Vector3 targetPos = transform.position + (direction * gridSize);

            // 【発展】ここで「targetPosに壁がないか」Raycastで確認すると完璧です

            StartCoroutine(Move(targetPos));
        }
    }

    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;

        // 距離の二乗で判定（処理負荷が軽い）
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            // MoveTowardsで滑らかに移動
            transform.position = Vector3.MoveTowards(transform.position, targetPos, walkSpeed * Time.deltaTime);
            yield return null;
        }

        // 最後に位置をピタッと合わせる
        transform.position = targetPos;

        isMoving = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Ladder"))
        {
            isLadder = true;
        }
        else if(collision.CompareTag("Gold"))
        {

        }
        else if(collision.CompareTag("Rod"))
        {
            isRod = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = false;
        }
        else if(collision.CompareTag("Rod"))
        {
            isRod = false;
        }
    }

}