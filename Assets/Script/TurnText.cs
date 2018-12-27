using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnText : MonoBehaviour {

    private GameMain gameMain;
    private int turn;

	// Use this for initialization
	void Start () {
        gameMain = GameObject.Find("GameMain").GetComponent<GameMain>();
        GameMain.OnNextGame += () => ShowTurn();

        gameObject.SetActive(false);
    }
	
    private void ShowTurn()
    {
        gameObject.SetActive(true);
        turn = gameMain.GetTurn();
        GetComponent<Text>().text = turn.ToString();

        Invoke("DelayDisable", 1.0f);
    }

    private void DelayDisable()
    {
        gameObject.SetActive(false);
    }
}
