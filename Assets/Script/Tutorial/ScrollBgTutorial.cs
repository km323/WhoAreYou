using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScrollBgTutorial : MonoBehaviour {
    [SerializeField]
    private GameObject centerWall;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void ScrollToBottom()
    {
        transform.DOMoveY(-Camera.main.orthographicSize, 1.5f);
    }
}
