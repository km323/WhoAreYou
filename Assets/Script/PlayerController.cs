using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    [SerializeField]
    private float moveSpeed = 2000;

    private static PlayerInput playerInput;

    private RecordController recordController;
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

        playerInput.onFirstTap += () => recordController.StartRecord();//記録しはじめる
    }

    void Update () {
        playerInput.Update();

        StopGameTime();

        //弾を撃つ
        if (playerInput.SameTimeTap)
            shot.ShotBullet();
    }

    void FixedUpdate()
    {
        Move();
    }

    //タップしてない時はゲーム時間を止める
    private void StopGameTime()
    {
        if (playerInput.HasTouch)
            Time.timeScale = 1;
        else
            Time.timeScale = 0;
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

    private void OnDestroy()
    {
        Time.timeScale = 1;
        recordController.StopRecord();//記録を止める 
    }
}
