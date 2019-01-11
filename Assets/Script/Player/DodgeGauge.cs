using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 回避に必要な長押しの時間を自機のフレームで表現するクラス
/// </summary>

public class DodgeGauge : MonoBehaviour {
    [SerializeField]
    private SpriteRenderer frame;
    [SerializeField]
    private Texture[] recMask;

    private const float firstPhase = 1 / 4f;
    private const float secondPhase = 1 / 2f;
    private const float thirdPhase = 3 / 4f;

    private StageManager stageManager;
    private float pressedTime;

	// Use this for initialization
	void Start () {
        stageManager = FindObjectOfType<StageManager>();
    }
	
	// Update is called once per frame
	void Update () {
        UpdateMask();
    }

    // 4段階 (過ぎてる時間の割合：どのテクスチャ番号）  
    //0:0,　1/4:1,  1/2:2,  3/4:3,  1:4
    private void UpdateMask()
    {
        pressedTime = PlayerController.GetPlayerInput().TouchTime;

        if (ReachNeedTime(stageManager.GetPressTimeNeed()))
            SetTexture(recMask[4]);
        else if (ReachNeedTime(stageManager.GetPressTimeNeed() * thirdPhase))
            SetTexture(recMask[3]);
        else if (ReachNeedTime(stageManager.GetPressTimeNeed() * secondPhase))
            SetTexture(recMask[2]);
        else if (ReachNeedTime(stageManager.GetPressTimeNeed() * firstPhase))
            SetTexture(recMask[1]);
        else
            SetTexture(recMask[0]);
    }

    private bool ReachNeedTime(float needTime)
    {
        if (pressedTime >= needTime)
            return true;
        else
            return false;
    }

    private void SetTexture(Texture tex)
    {
        frame.material.SetTexture("_AlphaTex", tex);
    }
}
