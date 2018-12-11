using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Camera : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CameraRotate()
    {
        transform.DORotate(new Vector3(0, 0, transform.rotation.z + 180), 1);
    }
}
