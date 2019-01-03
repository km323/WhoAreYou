using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffectTutorial : MonoBehaviour {
    private const float speed = 2f;
    private SpriteRenderer spriteRenderer;
    private float radius;

    private bool hasStart = false;
    public bool GetHasStart()
    {
        return hasStart;
    }

	// Use this for initialization
	void Awake () {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    public void StartEffect()
    {
        StartCoroutine("AppearEffect");
    }
    IEnumerator AppearEffect()
    {
        spriteRenderer.material.SetFloat("_EffectRadius", 0);
        float radius = spriteRenderer.material.GetFloat("_EffectRadius");

        while (radius < 2)
        {
            radius += Time.deltaTime * speed;

            spriteRenderer.material.SetFloat("_EffectRadius", radius);
            yield return null;
        }
        hasStart = true;
    }

    public void DieEffect()
    {
        StartCoroutine("VanishEffect");
    }
    IEnumerator VanishEffect()
    {
        spriteRenderer.material.SetFloat("_EffectRadius", 2);
        float radius = spriteRenderer.material.GetFloat("_EffectRadius");

        while (radius > 0)
        {
            radius -= Time.deltaTime * speed;

            spriteRenderer.material.SetFloat("_EffectRadius", radius);
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        SceneController.Instance.Change(Scene.Title);
    }
}
