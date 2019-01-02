using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlTutorial : MonoBehaviour {
    [SerializeField]
    private float moveSpeed = 2000;

    private PlayerInput playerInput;
    private Shot shot;
    private Rigidbody2D rigid;
    private Vector2 velocity;

    public bool EnableMove { get; set; }
    public bool EnableShot { get; set; }

    public bool HasMove { get; private set; }
    public bool HasShot { get; private set; }
    public bool HasDoge { get; private set; }

    // Use this for initialization
    void Start () {
        playerInput = new PlayerInput();

        shot = GetComponent<Shot>();
        rigid = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!EnableMove)
            return;

        playerInput.Update();

        //if (playerInput.SameTimeTap)
        //    shot.ShotBullet();
    }

    private void FixedUpdate()
    {
        Move();
    }

    //移動メソッド
    private void Move()
    {
        velocity = Vector2.zero;

        if (playerInput.HasTouch)
        {
            velocity.x = playerInput.Direction.x * moveSpeed * Time.fixedDeltaTime;
            velocity.y = playerInput.Direction.y * moveSpeed / 2 * Time.fixedDeltaTime;

            if (!HasMove && velocity.magnitude != 0)
                HasMove = true;
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
