using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleProperty : MonoBehaviour {
    [SerializeField]
    private GameObject move_EBlack;
    [SerializeField]
    private GameObject move_OBlack;
    [SerializeField]
    private GameObject fadeObjBlack;

    [SerializeField]
    private GameObject startButtonBlack;
    [SerializeField]
    private GameObject tutorialButtonBlack;

    [SerializeField]
    private GameObject highScoreBlack;
    [SerializeField]
    private Text bestScore;

    public GameObject GetMoveE()
    {
        return move_EBlack;
    }

    public GameObject GetMoveO()
    {
        return move_OBlack;
    }

    public GameObject GetFadeObj()
    {
        return fadeObjBlack;
    }

    public GameObject GetStartButton()
    {
        return startButtonBlack;
    }

    public GameObject GetTutorialButton()
    {
        return tutorialButtonBlack;
    }

    public void ShowBestScore()
    {
        highScoreBlack.SetActive(true);
        bestScore.text = GameController.BestScore.ToString();
    }

    // Use this for initialization
    void Awake ()
    {
        move_EBlack.SetActive(false);

        move_OBlack.SetActive(false);
        fadeObjBlack.SetActive(false);

        startButtonBlack.SetActive(false);
        tutorialButtonBlack.SetActive(false);
        highScoreBlack.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
