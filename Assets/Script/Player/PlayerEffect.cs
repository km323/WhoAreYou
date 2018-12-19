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
    private Sprite recSprite;
    [SerializeField]
    private Sprite playSprite;

    private SpriteRenderer spriteRenderer;
    private PolygonCollider2D collider;


    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        collider = GetComponent<PolygonCollider2D>();
    }

    private void OnEnable()
    {
        PlayStartEffect();    
    }

    void Start () {
        //GetComponent<PlayerCollision>().OnBulletHit += () => PlayDeadEffect();
        GetComponent<PlayerCollision>().OnBulletHit += () => SetDeadParticle();

        GameMain.OnNextGame += () => PlayVanishEffect();
        GameMain.OnNextGame += () => DestroyDeadParticle();
        GameMain.OnNextGame += () => ChageFrameObjSprite();
        GameMain.OnNextGame += () => DeadMove();

        CreateFrameObj();
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
        frameObjRenderer.sprite = recSprite;
    }
    private void ChageFrameObjSprite()
    {
        frameObjRenderer.sprite = playSprite;
    }
   
    private void SetDeadParticle()
    {
        collider.enabled = false;
        Instantiate(deadParticle, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
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
            radius += Time.fixedDeltaTime * speed;

            spriteRenderer.material.SetFloat("_EffectRadius", radius);
            yield return null;
        }

        if (GetComponent<PlayerController>() != null)
            spriteRenderer.material.SetFloat("_AlphaAmount", 1f);

        collider.enabled = true;
    }

    //vanish effect
    private void PlayVanishEffect()
    {
        if (!gameObject.activeSelf)
            return;
        if (GetComponent<PlayerController>() != null)
            spriteRenderer.material.SetFloat("_AlphaAmount", 1f);

        StartCoroutine("VanishEffect");
    }
    IEnumerator VanishEffect()
    {
        spriteRenderer.material.SetFloat("_EffectRadius", 2);
        float radius = spriteRenderer.material.GetFloat("_EffectRadius");

        while (radius > 0)
        {
            radius -= Time.fixedDeltaTime * speed;

            spriteRenderer.material.SetFloat("_EffectRadius", radius);
            yield return null;
        }
        //gameObject.SetActive(false);
    }
}
