using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffect : MonoBehaviour {
    [SerializeField]
    private float speed;
    //[SerializeField]
    //private GameObject effectPrefab;
    //[SerializeField]
    //private GameObject startEffectPrefab;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void OnEnable()
    {
        PlayStartEffect();
    }

    void Start () {
        GetComponent<PlayerCollision>().OnBulletHit += () => PlayDeadEffect();
        //GameMain.OnNextGame += () => DestroyEffect();
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

        while (radius <= 2)
        {
            radius += Time.fixedDeltaTime * speed;

            spriteRenderer.material.SetFloat("_EffectRadius", radius);
            yield return null;
        }
        if (GetComponent<PlayerController>() != null)
            spriteRenderer.material.SetFloat("_AlphaAmount", 1f);
    }

    //dead effect
    private void PlayDeadEffect()
    {
        //if (GetComponent<PlayerController>() != null)
        //    deadEffect.GetComponent<SpriteRenderer>().material.SetFloat("_AlphaAmount", 1f);

        StartCoroutine("DeadEffect");
    }
    IEnumerator DeadEffect()
    {
        spriteRenderer.material.SetFloat("_EffectRadius", 2);
        float radius = spriteRenderer.material.GetFloat("_EffectRadius");
        while (radius >= 0)
        {
            radius -= Time.fixedDeltaTime * speed;

            spriteRenderer.material.SetFloat("_EffectRadius", radius);
            yield return null;
        }
        gameObject.SetActive(false);
    }

    //private void DisableMonoBehaviour()
    //{
    //    foreach (MonoBehaviour m in GetComponents<MonoBehaviour>())
    //    {
    //        if (m.GetType().Name == "PlayerEffect")
    //            continue;
    //        m.enabled = false;
    //    }
    //}
    //private void EnableMonoBehaviour()
    //{
    //    foreach (MonoBehaviour m in GetComponents<MonoBehaviour>())
    //    {
    //        if (m.name == "PlayerEffect")
    //            continue;
    //        m.enabled = true;
    //    }
    //}

    //private void DestroyEffect()
    //{
    //    if(deadEffect != null)
    //        Destroy(deadEffect);

    //    if (startEffect != null)
    //        Destroy(startEffect);
    //}
}
