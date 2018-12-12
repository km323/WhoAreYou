using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadEffect : MonoBehaviour {
    [SerializeField]
    private GameObject deadEffectPrefab;

    private GameObject effect;

    private void OnDisable()
    {

    }

    void Start () {
        GetComponent<PlayerCollision>().onBulletHit += () => PlayDeadEffect();
    }
	
    private void PlayDeadEffect()
    {
        effect = Instantiate(deadEffectPrefab,transform.position,Quaternion.identity);
    }
}
