using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public Vector3 startPosition;
    private int currentState;

	// Use this for initialization
	void Start () {
        //startPosition = transform.position;

    }

    private void OnEnable()
    {
        transform.position = startPosition;
    }

    // Update is called once per frame
    void Update () {
        startPosition = transform.position;
	}
}
