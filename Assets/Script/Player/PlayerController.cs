using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerController : MonoBehaviour {
    [SerializeField]
    private float moveSpeed = 2000;

    private static PlayerInput playerInput;
    public static PlayerInput GetPlayerInput()
    {
        return playerInput;
    }

    private StageManager stageManager;
    private RecordController recordController;
    private Dodge dodge;
    private SpriteRenderer playerSprite;
    private Shot shot;
    private Rigidbody2D rigid;
    private Vector2 velocity;
    private bool enableDodge;

    private Item item;
    private bool hasMissileItem = false;
    private GameObject itemAssociated = null;

    private bool canDodgeGaugeMaxSe = true;

    void Awake ()
    {
        playerInput = new PlayerInput();
    }

    void Start()
    {
        stageManager = GameObject.Find("StageManager").GetComponent<StageManager>();
        recordController = GetComponent<RecordController>();
        dodge = GetComponent<Dodge>();
        rigid = GetComponent<Rigidbody2D>();
        shot = GetComponent<Shot>();
        playerSprite = GetComponentInChildren<SpriteRenderer>();
        playerInput.onFirstTap += () => recordController.StartRecord();//記録しはじめる

        enableDodge = true;
    }

    void Update () {
#if UNITY_EDITOR
        //if (GetComponent<PolygonCollider2D>().enabled)
        //    GetComponent<PolygonCollider2D>().enabled = false;

        if (Input.GetKeyDown(KeyCode.A))
            dodge.DodgeAttack();
#endif
        playerInput.Update();

        //PlayDodgegGaugeMaxSe();

        if (Input.GetKeyDown(KeyCode.P))
        {
            Time.timeScale = 0;
            playerInput.DisableInput();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            Time.timeScale = 1;
            playerInput.EnableInput();
        }

        //回避
        if (enableDodge && playerInput.TouchTime >= stageManager.GetPressTimeNeed() && playerInput.HasReleased)
        {
            dodge.DodgeAttack();
            SoundManager.Instance.PlaySe(SE.DodgeAttack);
        }
        if (hasMissileItem)
            return;

        //弾を撃つ
        if (playerInput.SameTimeTapBegin)
            SoundManager.Instance.PlaySe(SE.ShotBegin);
        if (playerInput.SameTimeTap)
            shot.ShotBullet();
    }

    private void PlayDodgegGaugeMaxSe()
    {
        if (canDodgeGaugeMaxSe && playerInput.TouchTime >= stageManager.GetPressTimeNeed())
        {
            SoundManager.Instance.PlaySe(SE.DodgeGaugeMax);
            canDodgeGaugeMaxSe = false;
        }
        if (playerInput.HasReleased)
            canDodgeGaugeMaxSe = true;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "SlowMotion")
            return;

        if (collision.tag == "Item")
        {
            UseItem();

            collision.gameObject.GetComponent<Collider2D>().enabled = false;
            collision.gameObject.GetComponentInChildren<SpriteRenderer>().DOFade(0f, 1f);
            collision.transform.DOScale(new Vector3(2f, 2f, 1f), 1f);
            Destroy(collision.gameObject,1f);

            SoundManager.Instance.PlaySe(SE.GetItem);
            return;
        }

        enableDodge = false;

        GameMain gameMain = FindObjectOfType<GameMain>();
        gameMain.DisableWhenActiveDie();

        SoundManager.Instance.PlaySe(SE.PlayerDamage);

        SceneController.Instance.Additive(Scene.Result);
    }

    private void UseItem()
    {
        Item item = GameObject.Find("ItemManager").GetComponent<ItemManager>().GetItem();
        switch (item.GetKindOfItem())
        {
            case Item.KindOfItem.Attack:
                shot.SetBulletPrefab(item.GetItemEffect());
                break;
            case Item.KindOfItem.Defence:
                itemAssociated = Instantiate(item.GetItemEffect());
                break;
            case Item.KindOfItem.Missile:
                itemAssociated = Instantiate(item.GetItemAssociated());
                hasMissileItem = true;
                break;
            default:
                break;
        }
    }

    private void OnDestroy()
    {
        recordController.StopRecord();//記録を止める
        playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 0.35f);
        playerSprite.sortingLayerName = "Default";

        Object.Destroy(FindObjectOfType<Dodge>());

        shot.SetDefaultBullet();
        Destroy(itemAssociated);
    }

    private void OnDisable()
    {
        if (itemAssociated != null) 
            Destroy(itemAssociated);
    }
}
