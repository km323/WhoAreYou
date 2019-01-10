using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowTouchTime : MonoBehaviour {
    [SerializeField]
    private Text text;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        text.text = GameObject.Find("StageManager").GetComponent<StageManager>().GetPressTimeNeed().ToString() 
            + "  <  "
            + PlayerController.GetPlayerInput().TouchTime.ToString();
    }
}
