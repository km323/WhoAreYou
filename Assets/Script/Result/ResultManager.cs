using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class ResultManager : MonoBehaviour {
    [SerializeField]
    private CanvasGroup canvasGroup;
    [SerializeField]
    private GameObject normalScoreObj;
    [SerializeField]
    private GameObject bestScoreObj;
    [SerializeField]
    private GameObject retryButton;
    [SerializeField]
    private GameObject returnButton;
    [SerializeField]
    private float targetPosY;

    private void Awake()
    {
        normalScoreObj.SetActive(false);
        bestScoreObj.SetActive(false);
        retryButton.SetActive(false);
        returnButton.SetActive(false);
        canvasGroup.alpha = 0f;
    }

    // Use this for initialization
    void Start () {
        if (SceneManager.sceneCount > 1)
        {
            GameObject.Find("TestCamera").SetActive(false);
            StartCoroutine("GameoverEffect");
        }
        else
        {
            CanvasFadeIn();
            ActiveButton();
        }

        if (GameController.CurrentScore > GameController.BestScore)
            ShowNewBest();
        else
            ShowNormalScore();
    }

    private void ShowNewBest()
    {
        GameController.BestScore = GameController.CurrentScore;
        bestScoreObj.SetActive(true);
    }

    private void ShowNormalScore()
    {
        normalScoreObj.SetActive(true);
    }

    IEnumerator GameoverEffect()
    {
        Gameover gameover = new Gameover();

        Time.timeScale = 0.5f;
        yield return new WaitForSeconds(1f);

        gameover.DestroyAllCharacter();
        yield return new WaitForSeconds(1f);

        Time.timeScale = 1f;
        gameover.ScrollBg();

        yield return new WaitForSeconds(1f);
        CanvasFadeIn();

        yield return new WaitForSeconds(1f);
        ActiveButton();
    }

    private void ActiveButton()
    {
        retryButton.SetActive(true);
        returnButton.SetActive(true);

        retryButton.GetComponent<RectTransform>().DOAnchorPosY(targetPosY, 0.2f);
        returnButton.GetComponent<RectTransform>().DOAnchorPosY(targetPosY, 0.2f);
    }

    private void CanvasFadeIn()
    {
        canvasGroup.DOFade(1f, 2f);
    }

    public void LoadTitleScene()
    {
        SceneController.Instance.Change(Scene.Title);
    }

    public void LoadGameScene()
    {
        SceneController.Instance.Change(Scene.Game);
    }

    // Update is called once per frame
    void Update () {
        //if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        //    SceneController.Instance.Change(Scene.Title);
    }
}
