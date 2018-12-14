using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadEffect : MonoBehaviour {
    [SerializeField]
    private GameObject deadEffectPrefab;

    private GameObject effect;

    void Start () {

        GetComponent<PlayerCollision>().OnBulletHit += () => PlayDeadEffect();
        GameMain.OnNextGame += () => DestroyEffect();
    }

    private void PlayDeadEffect()
    {
        effect = Instantiate(deadEffectPrefab,transform.position,Quaternion.identity);

        if(GetComponent<PlayerController>() != null)
            effect.GetComponent<SpriteRenderer>().material.SetFloat("_AlphaAmount", 1f);
    }

    private void DestroyEffect()
    {
        Destroy(effect);
    }
}
