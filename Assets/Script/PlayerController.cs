using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField]
    private float moveSpeed;

    private PlayerInput playerInput;
    private Rigidbody2D rigid;
    private Vector2 velocity;

    void Awake () {
        playerInput = new PlayerInput();
    }

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update () {
        playerInput.Update();

        Move();

        if (playerInput.SecondTap)
            Debug.Log("SHOT");
    }

    private void Move()
    {
        velocity = Vector2.zero;

        if (playerInput.HasTouch)
        {
            velocity.x = playerInput.Direction.x * moveSpeed * Time.fixedDeltaTime;
            velocity.y = playerInput.Direction.y * moveSpeed / 2 * Time.fixedDeltaTime;
        }

        //早いスワイプがあったら、すぐに方向を切り替え
        if (playerInput.QuickSwipe)
            rigid.AddForce(new Vector2(playerInput.Direction.x, 0), ForceMode2D.Impulse);

        //慣性をなくす
        if (!playerInput.HasTouch || playerInput.TouchCount <= 0)
            rigid.velocity = Vector2.zero;

        rigid.AddForce(velocity, ForceMode2D.Force);
    }
}
