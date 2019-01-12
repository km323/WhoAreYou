using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultProperty : MonoBehaviour {
    [SerializeField]
    private GameObject normalScoreObjBlack;
    [SerializeField]
    private GameObject bestScoreObjBlack;
    [SerializeField]
    private GameObject normalScoreObjWhite;
    [SerializeField]
    private GameObject bestScoreObjWhite;

    [SerializeField]
    private RectTransform barBlack;
    [SerializeField]
    private RectTransform barWhite;

    [SerializeField]
    private GameObject retryButtonBlack;
    [SerializeField]
    private GameObject titleButtonBlack;
    [SerializeField]
    private GameObject retryButtonWhite;
    [SerializeField]
    private GameObject titleButtonWhite;

    [SerializeField]
    private Color colorBlack;
    [SerializeField]
    private Color colorWhite;

    [SerializeField]
    private Text normalScore;
    [SerializeField]
    private Text bestScore;
    [SerializeField]
    private Text newBestScore;

    public Color GetColorBlack()
    {
        return colorBlack;
    }

    public Color GetColorWhite()
    {
        return colorWhite;
    }

    public GameObject GetNormalScoreObj()
    {
        if (GameMain.GetCurrentState() == GameMain.BLACK)
            return normalScoreObjBlack;
        else
            return normalScoreObjWhite;
    }

    public GameObject GetBestScoreObj()
    {
        if (GameMain.GetCurrentState() == GameMain.BLACK)
            return bestScoreObjBlack;
        else
            return bestScoreObjWhite;
    }

    public RectTransform GetBar()
    {
        if (GameMain.GetCurrentState() == GameMain.BLACK)
            return barBlack;
        else
            return barWhite;
    }

    public GameObject GetRetryButton()
    {
        if (GameMain.GetCurrentState() == GameMain.BLACK)
            return retryButtonBlack;
        else
            return retryButtonWhite;
    }

    public GameObject GetTitleButton()
    {
        if (GameMain.GetCurrentState() == GameMain.BLACK)
            return titleButtonBlack;
        else
            return titleButtonWhite;
    }

    public void SetNormalScore(string score)
    {
        normalScore.gameObject.SetActive(true);
        if (GameMain.GetCurrentState() == GameMain.BLACK)
            normalScore.color = colorWhite;
        else
            normalScore.color = colorBlack;

        normalScore.text = score;
    }

    public void SetBestScore(string score)
    {
        bestScore.gameObject.SetActive(true);
        if (GameMain.GetCurrentState() == GameMain.BLACK)
            bestScore.color = colorWhite;
        else
            bestScore.color = colorBlack;

        bestScore.text = score;
    }

    public void SetNewBestScore(string score)
    {
        newBestScore.gameObject.SetActive(true);
        if (GameMain.GetCurrentState() == GameMain.BLACK)
            newBestScore.color = colorWhite;
        else
            newBestScore.color = colorBlack;

        newBestScore.text = score;
    }

    public void SetRetryButtonColor(Color color)
    {
        if (GameMain.GetCurrentState() == GameMain.BLACK)
            retryButtonBlack.transform.Find("Mark").GetComponent<Image>().color = color;
        else
            retryButtonWhite.transform.Find("Mark").GetComponent<Image>().color = color;
    }

    public void SetTitleButtonColor(Color color)
    {
        if (GameMain.GetCurrentState() == GameMain.BLACK)
             titleButtonBlack.transform.Find("Mark").GetComponent<Image>().color = color;
        else
            titleButtonWhite.transform.Find("Mark").GetComponent<Image>().color = color;
    }

    // Use this for initialization
    void Awake () {
        normalScoreObjBlack.SetActive(false);
        normalScoreObjWhite.SetActive(false);

        bestScoreObjBlack.SetActive(false);
        bestScoreObjWhite.SetActive(false);

        barBlack.gameObject.SetActive(false);
        barWhite.gameObject.SetActive(false);

        retryButtonBlack.SetActive(false);
        retryButtonWhite.SetActive(false);

        titleButtonBlack.SetActive(false);
        titleButtonWhite.SetActive(false);

        normalScore.gameObject.SetActive(false);
        bestScore.gameObject.SetActive(false);
        newBestScore.gameObject.SetActive(false);
    }

    void Update()
    {
    }
}
