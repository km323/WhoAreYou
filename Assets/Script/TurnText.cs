using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnText : MonoBehaviour {

    private int turn;

	// Use this for initialization
	void Start () {
        turn = 0;
        GameMain.OnNextGame += () => AddTurn();

        gameObject.SetActive(false);
    }
	
    private void AddTurn()
    {
        gameObject.SetActive(true);
        turn++;
        GetComponent<Text>().text = turn.ToString();

        Invoke("DelayDisable", 1.0f);
    }

    private void DelayDisable()
    {
        gameObject.SetActive(false);
    }
}
