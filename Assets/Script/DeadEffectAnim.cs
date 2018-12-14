using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadEffectAnim : MonoBehaviour {
    [SerializeField]
    private float speed;
    private SpriteRenderer renderer;

	// Use this for initialization
	void Start () {
        Play();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void Play()
    {
        renderer = GetComponent<SpriteRenderer>();
        StartCoroutine("DeadEffect");
    }

    IEnumerator DeadEffect()
    {
        float radius = renderer.material.GetFloat("_EffectRadius");
        while (radius > 0)
        {
            radius -= Time.fixedDeltaTime * speed;

            renderer.material.SetFloat("_EffectRadius", radius);
            yield return new WaitForFixedUpdate();
        }
    }
}
