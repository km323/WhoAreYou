using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartEffect : MonoBehaviour {

    [SerializeField]
    private float speed;
    private SpriteRenderer spriteRenderer;

    // Use this for initialization
    void Start()
    {
        Play();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Play()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine("PlayEffect");
    }

    IEnumerator PlayEffect()
    {
        float radius = spriteRenderer.material.GetFloat("_EffectRadius");
        while (radius < 2)
        {
            radius += Time.fixedDeltaTime * speed;

            spriteRenderer.material.SetFloat("_EffectRadius", radius);
            yield return new WaitForFixedUpdate();
        }
        Destroy(gameObject);
    }
}
