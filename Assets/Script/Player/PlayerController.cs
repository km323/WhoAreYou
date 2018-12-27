using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerController : MonoBehaviour {
    [SerializeField]
    private float moveSpeed = 2000;

    private static PlayerInput playerInput;

    private RecordController recordController;
    private SpriteRenderer playerSprite;
    private Dodge dodge;
    private Shot shot;
    private Rigidbody2D rigid;
    private Vector2 velocity;

    public static PlayerInput GetPlayerInput()
    {
        return playerInput;
    }

    void Awake ()
    {
        playerInput = new PlayerInput();
    }

    void Start()
    {
        recordController = GetComponent<RecordController>();
        rigid = GetComponent<Rigidbody2D>();
        shot = GetComponent<Shot>();
        dodge = GetComponent<Dodge>();
        playerSprite = GetComponentInChildren<SpriteRenderer>();
        playerInput.onFirstTap += () => recordController.StartRecord();//記録しはじめる
    }

    void Update () {
        playerInput.Update();
        
        //弾を撃つ
        //if (playerInput.SameTimeTap)
        //    shot.ShotBullet();
        if (playerInput.TouchTime > 0)
            shot.ChargeShotBullet(playerInput.TouchTime);

        Slide();
    }

    void FixedUpdate()
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
        }

        //早いスワイプがあったら、すぐに方向を切り替え
        if (playerInput.QuickSwipe)
            rigid.AddForce(new Vector2(playerInput.Direction.x, 0), ForceMode2D.Impulse);

        //慣性をなくす
        if (!playerInput.HasTouch || playerInput.TouchCount <= 0)
            rigid.velocity = Vector2.zero;

        rigid.AddForce(velocity, ForceMode2D.Force);
    }

    private void Slide()
    {
        if (playerInput.QuickSwipe)
        {
            dodge.DodgeAttack();
            velocity.x = playerInput.Direction.x * 10f;
            velocity.y = playerInput.Direction.y * 10f;
            rigid.velocity = velocity;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.tag == "Bullet")
        //    Time.timeScale = 0.2f;
        //SceneController.Instance.Change(Scene.Game);
    }

    private void OnDestroy()
    {
        recordController.StopRecord();//記録を止める
        playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 0.4f);
    }
}
