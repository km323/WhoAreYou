using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestGauge : MonoBehaviour {
    [SerializeField]
    private GameObject[] gauge;

    protected const float firstPhase = 1 / 3f;
    protected const float secondPhase = 2 / 3f;

    private StageManager stageManager;
    protected float pressedTime;
    private int oldCurrentStage;

    // Use this for initialization
    void Start()
    {
        stageManager = FindObjectOfType<StageManager>();
        oldCurrentStage = GameMain.GetCurrentState();
    }

    // Update is called once per frame
    void Update()
    {

        if (GameMain.GetCurrentState() != oldCurrentStage)
        {
            foreach (GameObject obj in gauge)
            {
                if (GameMain.GetCurrentState() == GameMain.BLACK)
                    obj.GetComponent<Image>().color = stageManager.GetColorBlack();
                else
                    obj.GetComponent<Image>().color = stageManager.GetColorWhite();
            }
        }

        oldCurrentStage = GameMain.GetCurrentState();
        UpdateMask();
    }

    // 4段階 (過ぎてる時間の割合：どのテクスチャ番号）  
    //0:0,　1/4:1,  2/4:2,  3/4:3,  1:4
    private void UpdateMask()
    {
        pressedTime = PlayerController.GetPlayerInput().TouchTime;

        if (ReachNeedTime(stageManager.GetPressTimeNeed()))
            gauge[0].SetActive(true);
        else if (ReachNeedTime(stageManager.GetPressTimeNeed() * secondPhase))
            gauge[1].SetActive(true);
        else if (ReachNeedTime(stageManager.GetPressTimeNeed() * firstPhase))
            gauge[2].SetActive(true);
        else
        {
            foreach (GameObject obj in gauge)
                obj.SetActive(false);
        }
    }

    private bool ReachNeedTime(float needTime)
    {
        if (pressedTime >= needTime)
            return true;
        else
            return false;
    }
}
