using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Scene
{
    None = -1,
    Title,
    Game,
    Result,
}

public class SceneController : SingletonMonoBehaviour<SceneController> {

    static Dictionary<Scene, string> SceneName = new Dictionary<Scene, string>()
    {
        {Scene.None,""},
        {Scene.Title,"Title" },
        {Scene.Game,"Game"},
        {Scene.Result,"Result"}
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

    protected override void Awake()
    {
        base.Awake();

        DontDestroyOnLoad(gameObject);
        gameObject.name = "SceneController";
    }

    public void Change(Scene scene, Scene additiveScene = Scene.None)
    {
        nextScene = scene;

        this.additiveScene = additiveScene;

        SceneManager.LoadScene(SceneName[nextScene]);

        if (this.additiveScene != Scene.None)
            SceneManager.LoadScene(SceneName[this.additiveScene], LoadSceneMode.Additive);

        beforeScene = currentScene;
        currentScene = nextScene;
        nextScene = Scene.None;
        this.additiveScene = Scene.None;
    }
    
}
