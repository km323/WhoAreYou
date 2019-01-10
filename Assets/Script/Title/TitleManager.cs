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
    private float buttonTargetPosY;
    [SerializeField]
    private float titleTargetPosX;

    private void Awake()
    {
        canvasGroup.alpha = 0f;
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
        SceneController.Instance.Change(Scene.Tutorial);
    }

    public void LoadGameScene()
    {
        SceneController.Instance.Change(Scene.Game);
    }

    IEnumerator PlayEffect()
    {
        yield return new WaitForSeconds(0.5f);

        MoveTitle();
        yield return new WaitForSeconds(1f);

        CanvasFadeIn();
        yield return new WaitForSeconds(1f);

        ActiveButton();
    }

    private void MoveTitle()
    {
        property.GetMoveE().SetActive(true);
        property.GetMoveO().SetActive(true);

        property.GetMoveE().transform.DOMoveX(titleTargetPosX, 1f);
        property.GetMoveO().transform.DOMoveX(titleTargetPosX, 1f);
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

        property.GetStartButton().GetComponent<RectTransform>().DOAnchorPosY(buttonTargetPosY, 0.3f);
        property.GetTutorialButton().GetComponent<RectTransform>().DOAnchorPosY(buttonTargetPosY, 0.3f);
    }
}
