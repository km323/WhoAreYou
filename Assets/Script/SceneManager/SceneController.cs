﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Scene
{
    None = -1,
    Title,
    Tutorial,
    Game,
    Result,
    Pause,
}

public class SceneController : SingletonMonoBehaviour<SceneController> {

    static Dictionary<Scene, string> SceneName = new Dictionary<Scene, string>()
    {
        {Scene.None,""},
        {Scene.Title,"Title" },
        {Scene.Tutorial, "Tutorial" },
        {Scene.Game,"Game"},
        {Scene.Result,"Result"},
        {Scene.Pause,"Pause" }
    };

    //一個前と現在、次のシーン名
    public string BeforeSceneName
    {
        get { return SceneName[beforeScene]; }
    }
    
    public string CurrentSceneName
    {
        get { return SceneName[currentScene]; }
    }
    
    public string NextSceneName
    {
        get { return SceneName[nextScene]; }
    }

    private Scene beforeScene = Scene.None;
    private Scene currentScene = Scene.None;
    private Scene nextScene = Scene.None;
    private Scene additiveScene = Scene.None;

    //フェイド
    private const float fadeInterval = 0.7f;
    private bool isFading;
    private float fadeAlpha;
    private Color fadeColor;

    protected override void Awake()
    {
        base.Awake();

        isFading = false;
        fadeAlpha = 0;
        fadeColor = Color.black;

        DontDestroyOnLoad(gameObject);
        gameObject.name = "SceneController";
    }

    private void OnGUI()
    {
        if (isFading)
        {
            //色と透明度を更新して白テクスチャを描画 .
            fadeColor.a = fadeAlpha;
            GUI.color = this.fadeColor;
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Texture2D.whiteTexture);
        }
    }

    public void Additive(Scene additiveScene = Scene.None)
    {
        this.additiveScene = additiveScene;

        if (this.additiveScene != Scene.None)
            SceneManager.LoadScene(SceneName[this.additiveScene], LoadSceneMode.Additive);
    }

    public void UnLoad(Scene scene)
    {
        SceneManager.UnloadSceneAsync(SceneName[scene]);
    }

    public void Change(Scene scene)
    {
        if (isFading)
            return;

        nextScene = scene;

        StartCoroutine(ChangeScene(nextScene));

        

        //SceneManager.LoadScene(SceneName[nextScene]);

        beforeScene = currentScene;
        currentScene = nextScene;
        nextScene = Scene.None;
        this.additiveScene = Scene.None;
    }

    IEnumerator ChangeScene(Scene scene)
    {
        //fade out
        SoundManager.Instance.FadeOutBgm(fadeInterval);

        isFading = true;
        float time = 0;
        while (time <= fadeInterval)
        {
            fadeAlpha = Mathf.Lerp(0f, 1f, time / fadeInterval);
            time += Time.unscaledDeltaTime;
            yield return null;
        }

        //load scene
        SceneManager.LoadScene(SceneName[scene]);

        //fade in
        SoundManager.Instance.FadeInBgm(1.0f);

        time = 0;
        while (time <= fadeInterval)
        {
            fadeAlpha = Mathf.Lerp(1f, 0f, time / fadeInterval);
            time += Time.unscaledDeltaTime;
            yield return null;
        }
        isFading = false;
    }
}
