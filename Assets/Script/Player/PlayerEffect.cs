using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerEffect : MonoBehaviour {
    [SerializeField]
    private float speed;
    [SerializeField]
    private GameObject deadParticle;
    [SerializeField]
    private GameObject frameOut; //prefabにある外側のフレーム、色変更×
    [SerializeField]
    private SpriteRenderer frameObjRenderer; //色を変更できるフレーム

    [SerializeField]
    private Sprite greyFrame;
    [SerializeField]
    private Texture playMaskAlpha;
    [SerializeField]
    private Texture playMask;
    [SerializeField]
    private Texture recMask;

    [SerializeField]
    private GameObject activeDeadPrefab;

    private StageManager stageManager;
    private Sprite recSprite;

    private GameMain gameMain;
    private SpriteRenderer spriteRenderer;
    private PolygonCollider2D polygonCollider;
    private bool isPlaying;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        polygonCollider = GetComponent<PolygonCollider2D>();
        stageManager = GameObject.Find("StageManager").GetComponent<StageManager>();
        isPlaying = false;
    }

    private void OnEnable()
    {
        polygonCollider.enabled = false;
        isPlaying = false;
        PlayStartEffect();
    }

    void Start () {
        gameMain = FindObjectOfType<GameMain>();

        GetComponent<PlayerCollision>().OnBulletHit += PlayVanishEffect;
        GetComponent<PlayerCollision>().OnBulletHit += DisableCollision;

        GameMain.OnNextGame += PlayVanishEffect;
        GameMain.OnNextGame += DestroyDeadParticle;
        GameMain.OnNextGame += ChageFrameObjSprite;
        GameMain.OnNextGame += DeadMove;

        CreateFrameObj();  
    }

    private void OnDestroy()
    {
        GetComponent<PlayerCollision>().OnBulletHit -= PlayVanishEffect;
        GetComponent<PlayerCollision>().OnBulletHit -= DisableCollision;

        GameMain.OnNextGame -= PlayVanishEffect;
        GameMain.OnNextGame -= DestroyDeadParticle;
        GameMain.OnNextGame -= ChageFrameObjSprite;
        GameMain.OnNextGame -= DeadMove;
    }

    //ターン変わると、キャラクターを初期位置に戻す
    private void DeadMove()
    {
        transform.DOMove(GetComponent<RecordController>().GetStartPos(), 1);
    }

    //start時、frameObjRendererの色を決めるメソッド
    private void CreateFrameObj()
    {
        if (Camera.main.transform.up.y == 1)
            frameObjRenderer.gameObject.transform.rotation = Quaternion.identity;
        else
            frameObjRenderer.gameObject.transform.eulerAngles = new Vector3(0, 0, 180);

        recSprite = SetPlayerRecSprite();

        if (stageManager.GetNeedToReset())
        {
            if (!stageManager.getClearLastStage())
            {
                frameObjRenderer.sprite = SetPreviousRecSprite();
                Invoke("SetCurStageRecSprite", StageManager.EffectWaitInterval / 2);
            }
            else
                SetCurStageRecSprite();
        }
        else
            frameObjRenderer.sprite = recSprite;

        //frameObjRenderer.material.SetTexture("_AlphaTex", recMask);
    }

    private void SetCurStageRecSprite()
    {
        frameObjRenderer.sprite = recSprite;
    }

    private Sprite SetPlayerRecSprite()
    {
        Sprite sprite = null;
        if (GameMain.GetCurrentState() == GameMain.BLACK)
            sprite = stageManager.GetCurrentBlack();
        if (GameMain.GetCurrentState() == GameMain.WHITE)
            sprite = stageManager.GetCurrentWhite();

        return sprite;
    }
    private Sprite SetPreviousRecSprite()
    {
        Sprite sprite = null;
        if (GameMain.GetCurrentState() == GameMain.BLACK)
            sprite = stageManager.GetPreviousBlack();
        if (GameMain.GetCurrentState() == GameMain.WHITE)
            sprite = stageManager.GetPreviousWhite();

        return sprite;
    }

    private void SetPlayTexture(Texture tex)
    {
        frameObjRenderer.material.SetTexture("_AlphaTex", tex);
    }

    //ターン変わるたびフレームの初期化
    private void ChageFrameObjSprite()
    {
        if (frameOut.activeSelf)
            frameOut.SetActive(false);
        frameObjRenderer.sprite = greyFrame;

        if (GameMain.GetCurrentState() == GameMain.BLACK)
        {
            if(frameObjRenderer.gameObject.layer == 10)
                SetPlayTexture(playMaskAlpha);
            else
                SetPlayTexture(playMask);
        }
        if (GameMain.GetCurrentState() == GameMain.WHITE)
        {
            if (frameObjRenderer.gameObject.layer == 9)
                SetPlayTexture(playMaskAlpha);
            else
                SetPlayTexture(playMask);
        }

        frameObjRenderer.sortingLayerName = "Default";
    }

    private void DisableCollision()
    {
        polygonCollider.enabled = false;
    }
    private void DestroyDeadParticle()
    {
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("DeadEffect"))
            Destroy(obj);
    }

    //start effect
    private void PlayStartEffect()
    {
        spriteRenderer.material.SetFloat("_AlphaAmount", 0.35f);

        if(!isPlaying)
            StartCoroutine("StartEffect");
    }
    IEnumerator StartEffect()
    {
        isPlaying = true;
        spriteRenderer.material.SetFloat("_EffectRadius", 0);
        float radius = spriteRenderer.material.GetFloat("_EffectRadius");

        while (radius < 2)
        {
            radius += Time.deltaTime * speed;

            spriteRenderer.material.SetFloat("_EffectRadius", radius);
            yield return null;
        }

        if (gameObject == gameMain.GetActivePlayer())
             spriteRenderer.material.SetFloat("_AlphaAmount", 1f);

        polygonCollider.enabled = true;
        isPlaying = false;
    }

    //vanish effect
    public void PlayVanishEffect()
    {
        if (!gameObject.activeSelf)
            return;

        if (!isPlaying)
            StartCoroutine("VanishEffect");
    }
    private IEnumerator VanishEffect()
    {
        isPlaying = true;
        spriteRenderer.material.SetFloat("_EffectRadius", 2);
        float radius = spriteRenderer.material.GetFloat("_EffectRadius");
        bool showActiveDeadEffect = false;

        while (radius > 0)
        {
            radius -= Time.deltaTime * speed;

            if (!showActiveDeadEffect && GetComponent<PlayerController>() != null && radius <= 1.2f)
            {
                Instantiate(activeDeadPrefab, transform.position, Quaternion.identity);
                showActiveDeadEffect = true;
            }

            spriteRenderer.material.SetFloat("_EffectRadius", radius);
            yield return null;
        }
        gameObject.SetActive(false);
        isPlaying = false;
    }
}
