using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pauser : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (PlayerController.GetPlayerInput().HasTouch)
            Time.timeScale = 1;
        else
            Time.timeScale = 0;
    }

    //時間止まってるかどうか
    public static bool IsPause()
    {
        if (Time.timeScale == 1)
            return false;

        return true;
    }
}
