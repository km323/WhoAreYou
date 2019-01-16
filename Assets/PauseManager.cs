using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PauseManager : MonoBehaviour {

    [SerializeField]
    private Image playIcon;
    [SerializeField]
    private GameObject playObj;
    [SerializeField]
    private GameObject pauseText;
    [SerializeField]
    private Image alpha;

    private float curTimeScale = 0;
    private GameObject pauseObj;
    private Color color;

    private bool canInputButton = true;

    private void Awake()
    {
        curTimeScale = Time.timeScale;
        Time.timeScale = 0;
        pauseObj = GameObject.Find("PauseObj");
        pauseObj.SetActive(false);

        if (GameMain.GetCurrentState() == GameMain.BLACK)
            color = FindObjectOfType<StageManager>().GetColorBlack();
        else
            color = FindObjectOfType<StageManager>().GetColorWhite();

        playIcon.color = color;
        pauseText.GetComponent<Text>().color = color;

        pauseText.GetComponent<RectTransform>().DOAnchorPosX(1000, 1f).From().SetUpdate(true);
        alpha.DOFade(0, 1).From().SetUpdate(true);
    }

    public void PlayButton()
    {
        if (!canInputButton)
            return;

        Time.timeScale = curTimeScale;
        pauseObj.SetActive(true);

        SceneController.Instance.UnLoad(Scene.Pause);

        canInputButton = false;
    }

    public void TitleButton()
    {
        if (!canInputButton)
            return;

        Time.timeScale = 1;
        SceneController.Instance.Change(Scene.Title);

        canInputButton = false;
    }
}
