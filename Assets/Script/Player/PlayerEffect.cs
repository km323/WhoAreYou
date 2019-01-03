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
    private SpriteRenderer frameObjRenderer;
    [SerializeField]
    private Sprite playSprite;
    [SerializeField]
    private GameObject activeDeadPrefab;

    private StageManager stageManager;
    private Sprite recSprite;

    private SpriteRenderer spriteRenderer;
    private PolygonCollider2D polygonCollider;


    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        polygonCollider = GetComponent<PolygonCollider2D>();
        stageManager = GameObject.Find("StageManager").GetComponent<StageManager>();
    }

    private void OnEnable()
    {
        PlayStartEffect();    
    }

    void Start () {
        GetComponent<PlayerCollision>().OnBulletHit += PlayVanishEffect;
        GetComponent<PlayerCollision>().OnBulletHit += SetDeadParticle;

        GameMain.OnNextGame += PlayVanishEffect;
        GameMain.OnNextGame += DestroyDeadParticle;
        GameMain.OnNextGame += ChageFrameObjSprite;
        GameMain.OnNextGame += DeadMove;

        CreateFrameObj();
    }

    private void OnDestroy()
    {
        GetComponent<PlayerCollision>().OnBulletHit -= PlayVanishEffect;
        GetComponent<PlayerCollision>().OnBulletHit -= SetDeadParticle;

        GameMain.OnNextGame -= PlayVanishEffect;
        GameMain.OnNextGame -= DestroyDeadParticle;
        GameMain.OnNextGame -= ChageFrameObjSprite;
        GameMain.OnNextGame -= DeadMove;
    }

    private void DeadMove()
    {
        transform.DOMove(GetComponent<RecordController>().GetStartPos(), 1);
    }

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

    }
    private void SetCurStageRecSprite()
    {
        frameObjRenderer.sprite = recSprite;
    }

    private Sprite SetPlayerRecSprite()
    {
        Sprite sprite = null;
        if (GameMain.GetCurrentState() == GameMain.BLACK)
            sprite = stageManager.GetPlayerBlackRec();
        if (GameMain.GetCurrentState() == GameMain.WHITE)
            sprite = stageManager.GetPlayerWhiteRec();

        return sprite;
    }
    private Sprite SetPreviousRecSprite()
    {
        Sprite sprite = null;
        if (GameMain.GetCurrentState() == GameMain.BLACK)
            sprite = stageManager.GetPreviousBlackRec();
        if (GameMain.GetCurrentState() == GameMain.WHITE)
            sprite = stageManager.GetPreviousWhiteRec();

        return sprite;
    }

    private void ChageFrameObjSprite()
    {
        frameObjRenderer.sprite = playSprite;
        frameObjRenderer.sortingLayerName = "Default";
    }

    private void SetDeadParticle()
    {
        polygonCollider.enabled = false;
        //Instantiate(deadParticle, transform.position, Quaternion.identity,transform);
        //gameObject.SetActive(false);
    }

    private void DestroyDeadParticle()
    {
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("DeadEffect"))
            Destroy(obj);
    }

    //start effect
    private void PlayStartEffect()
    {
        if (GetComponent<PlayerController>() == null)
            spriteRenderer.material.SetFloat("_AlphaAmount", 0.35f);

        StartCoroutine("StartEffect");
    }
    IEnumerator StartEffect()
    {
        spriteRenderer.material.SetFloat("_EffectRadius", 0);
        float radius = spriteRenderer.material.GetFloat("_EffectRadius");

        while (radius < 2)
        {
            radius += Time.deltaTime * speed;

            spriteRenderer.material.SetFloat("_EffectRadius", radius);
            yield return null;
        }

        if (GetComponent<PlayerController>() != null)
            spriteRenderer.material.SetFloat("_AlphaAmount", 1f);

        polygonCollider.enabled = true;
    }

    //vanish effect
    public void PlayVanishEffect()
    {
        if (!gameObject.activeSelf)
            return;
        if (GetComponent<PlayerController>() != null)
            spriteRenderer.material.SetFloat("_AlphaAmount", 1f);

        StartCoroutine("VanishEffect");
    }
    private IEnumerator VanishEffect()
    {
        spriteRenderer.material.SetFloat("_EffectRadius", 2);
        float radius = spriteRenderer.material.GetFloat("_EffectRadius");
        bool showActiveDeadEffect = false;

        while (radius > 0)
        {
            radius -= Time.deltaTime * speed;

            if (!showActiveDeadEffect && GetComponent<PlayerController>() != null && radius <= 1)
            {
                Instantiate(activeDeadPrefab, transform.position, Quaternion.identity);
                showActiveDeadEffect = true;
            }

            spriteRenderer.material.SetFloat("_EffectRadius", radius);
            yield return null;
        }
        gameObject.SetActive(false);
    }
}
