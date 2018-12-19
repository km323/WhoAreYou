using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroyParticle : MonoBehaviour {
	// Use this for initialization
	void Start () {
        ParticleSystem partcleSystem = GetComponent<ParticleSystem>();
        Destroy(gameObject, (float)partcleSystem.main.duration);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
