using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class TitleManager : MonoBehaviour {
    [SerializeField]
    private CanvasGroup canvasGroup;
    [SerializeField]
    private TitleProperty property;
    [SerializeField]
    private Camera glitchCamera;

    [SerializeField]
    private float startBtnTargetPosY;
    [SerializeField]
    private float tutorialBtnTargetPosY;
    [SerializeField]
    private float titleTargetPosX;

    private const float glitchIntensity = 0.04f;

    private DigitalGlitch digitalGlitch;

    private bool canPlaySe = true;

    private void Awake()
    {
        canvasGroup.alpha = 0f;
        digitalGlitch = glitchCamera.GetComponent<DigitalGlitch>();
    }

    void Start () {

        if (SceneManager.sceneCount > 1)
        {
            GameObject.Find("TestCamera").SetActive(false);
        }
        else
        {
            StartCoroutine("PlayEffect");
        }
        SoundManager.Instance.StopBgm();
        SoundManager.Instance.PlayBgm(BGM.Title);
    }

    void Update()
    { 
    }

    public void LoadTutorialScene()
    {
        if (!canPlaySe)
            return;
        SceneController.Instance.Change(Scene.Tutorial);
        SoundManager.Instance.PlaySe(SE.UIResult);
        canPlaySe = false;
    }

    public void LoadGameScene()
    {
        if (!canPlaySe)
            return;
        SceneController.Instance.Change(Scene.Game);
        SoundManager.Instance.PlaySe(SE.UIResult);
        canPlaySe = false;
    }

    public void OnPointerDownSe()
    {
        if (!canPlaySe)
            return;
        SoundManager.Instance.PlaySe(SE.ShotBegin);

        Debug.Log("aaa");
    }

    IEnumerator PlayEffect()
    {
        yield return new WaitForSeconds(0.5f);

        MoveTitle();
        yield return new WaitForSeconds(1f);

        CanvasFadeIn();
        yield return new WaitForSeconds(0.5f);

        if (GameController.BestScore > 0)
            property.ShowBestScore();
        yield return new WaitForSeconds(0.5f);

        ActiveButton();

        StartCoroutine("Glitch");
    }

    IEnumerator Glitch()
    {
        while(true)
        {
            digitalGlitch.intensity = 0;
            yield return new WaitForSeconds(Random.Range(2,5));
            digitalGlitch.intensity = glitchIntensity;
            yield return new WaitForSeconds(1f);
        }
    }

    private void MoveTitle()
    {
        property.GetMoveE().DOAnchorPosX(titleTargetPosX, 1f);
        property.GetMoveO().DOAnchorPosX(titleTargetPosX, 1f);
    }


    private void CanvasFadeIn()
    {
        property.GetFadeObj().SetActive(true);
        canvasGroup.DOFade(1f, 3f);
    }

    private void ActiveButton()
    {
        property.GetStartButton().SetActive(true);
        property.GetTutorialButton().SetActive(true);

        property.GetStartButton().GetComponent<RectTransform>().DOAnchorPosY(startBtnTargetPosY, 0.3f);
        property.GetTutorialButton().GetComponent<RectTransform>().DOAnchorPosY(tutorialBtnTargetPosY, 0.3f);
    }
}
