using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffect : MonoBehaviour {
    [SerializeField]
    private float speed;
    [SerializeField]
    private GameObject deadParticle;

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
        gameObject.SetActive(false);
    }
}
