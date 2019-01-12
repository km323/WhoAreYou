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
    private ResultProperty property;
    [SerializeField]
    private float barTargetPosX;
    [SerializeField]
    private float buttonTargetPosY;

    private void Awake()
    {
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
        SoundManager.Instance.PlayBgm(BGM.Game);
    }

    private void ShowNewBest()
    {
        GameController.BestScore = GameController.CurrentScore;
        property.GetBestScoreObj().SetActive(true);

        property.SetNewBestScore(GameController.BestScore.ToString());
    }

    private void ShowNormalScore()
    {
        property.GetNormalScoreObj().SetActive(true);

        property.SetNormalScore(GameController.CurrentScore.ToString());
        property.SetBestScore(GameController.BestScore.ToString());
    }

    IEnumerator GameoverEffect()
    {
        Gameover gameover = new Gameover();
        yield return new WaitForSeconds(0.5f);

        Time.timeScale = 0.3f;
        yield return new WaitForSeconds(0.8f);

        Time.timeScale = 1f;
        gameover.DestroyAllCharacter();
        yield return new WaitForSeconds(1f);

        gameover.ScrollBg();

        yield return new WaitForSeconds(1.2f);
        ShowBar();

        yield return new WaitForSeconds(0.8f);
        CanvasFadeIn();
        
        yield return new WaitForSeconds(0.3f);

        yield return new WaitForSeconds(1f);
        ActiveButton();
    }

    private void ActiveButton()
    {
        property.GetRetryButton().SetActive(true);
        property.GetTitleButton().SetActive(true);

        property.GetRetryButton().GetComponent<RectTransform>().DOAnchorPosY(buttonTargetPosY, 0.3f);
        property.GetTitleButton().GetComponent<RectTransform>().DOAnchorPosY(buttonTargetPosY, 0.3f);
    }

    private void ShowBar()
    {
        property.GetBar().gameObject.SetActive(true);
        property.GetBar().DOAnchorPosX(barTargetPosX, 0.4f);
    }

    private void CanvasFadeIn()
    {
        canvasGroup.DOFade(1f, 1.5f);
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
