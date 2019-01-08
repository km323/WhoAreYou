using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlTutorial : MonoBehaviour {
    [SerializeField]
    private float moveSpeed = 2000;
    [SerializeField]
    private PlayerEffectTutorial effect;

    private DodgeTutorial dodge;
    private PlayerInput playerInput;
    private Shot shot;
    private Rigidbody2D rigid;
    private Vector2 velocity;

    public bool EnableMove { get; set; }
    public bool EnableShot { get; set; }
    public bool EnableDodge { get; set; }

    public bool HasStart { get; private set; }
    public bool HasMove { get; private set; }
    public bool HasShot { get; private set; }
    public bool HasDodge { get; private set; }

    // Use this for initialization
    void Start () {
        playerInput = new PlayerInput();

        dodge = GetComponent<DodgeTutorial>();
        shot = GetComponent<Shot>();
        rigid = GetComponent<Rigidbody2D>();

        effect.StartEffect();
    }
	
	// Update is called once per frame
	void Update () {
        if (!HasStart && effect.GetHasStart())
            HasStart = true;

        if (!EnableMove)
            return;

        playerInput.Update();
        Shot();
        Dodge();
    }

    private void Shot()
    {
        if (EnableShot && playerInput.SameTimeTap)
        {
            shot.ShotBullet();
            if (!HasShot)
                HasShot = true;
        }
    }

    private void Dodge()
    {
        if (EnableDodge && (Input.GetKeyDown(KeyCode.A) || playerInput.QuickSwipe))
        {
            dodge.DodgeAttack();
            if (!HasDodge)
                HasDodge = true;
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        effect.DieEffect();
        Destroy(this);
    }
}
