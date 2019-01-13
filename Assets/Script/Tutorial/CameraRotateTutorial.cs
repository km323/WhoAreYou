using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraRotateTutorial : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CameraRotate()
    {
        transform.DORotate(new Vector3(0, 0, 180), 1).SetDelay(1f);
    }
}
