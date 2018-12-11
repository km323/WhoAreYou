using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMainTmp : MonoBehaviour {

    private GameObject activePlayer;
	// Use this for initialization
	void Start () {
        activePlayer = GameObject.Find("Player");
        activePlayer.AddComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!activePlayer.activeSelf)
        {
            Invoke("ActivePlayer", 1f);
        }

        if (Input.GetKeyDown(KeyCode.A))
            Destroy(activePlayer.GetComponent<PlayerController>());

        if (Input.GetKeyDown(KeyCode.S))
            activePlayer.SetActive(false);
    }

    private void ActivePlayer()
    {
        activePlayer.SetActive(true);
    }
}
