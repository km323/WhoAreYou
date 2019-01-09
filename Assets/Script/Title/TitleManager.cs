using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class TitleManager : MonoBehaviour {
    [SerializeField]
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup.alpha = 0f;
    }

    // Use this for initialization
    void Start () {
        if (SceneManager.sceneCount > 1)
        {
            GameObject.Find("TestCamera").SetActive(false);
        }
        else
        {
            CanvasFadeIn();
        }
        SoundManager.Instance.StopBgm();
        SoundManager.Instance.PlayBgm(BGM.Game);
        SoundManager.Instance.PlayBgm(BGM.Game2);
    }

    private void CanvasFadeIn()
    {
        canvasGroup.DOFade(1f, 2f);
    }

    public void LoadTutorialScene()
    {
        SceneController.Instance.Change(Scene.Tutorial);
    }

    public void LoadGameScene()
    {
        SceneController.Instance.Change(Scene.Game);
    }


    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        //    SceneController.Instance.Change(Scene.Game);
    }
}
