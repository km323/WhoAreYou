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

    private Texture curTexture;
    private Texture oldTexture;

    private const float firstPhase = 1 / 4f;
    private const float secondPhase = 2 / 4f;
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
    //0:0,　1/4:1,  2/4:2,  3/4:3,  1:4
    private void UpdateMask()
    {
        pressedTime = PlayerController.GetPlayerInput().TouchTime;

        if (ReachNeedTime(stageManager.GetPressTimeNeed()))
            curTexture = recMask[4];
        else if (ReachNeedTime(stageManager.GetPressTimeNeed() * thirdPhase))
            curTexture = recMask[3];
        else if (ReachNeedTime(stageManager.GetPressTimeNeed() * secondPhase))
            curTexture = recMask[2];
        else if (ReachNeedTime(stageManager.GetPressTimeNeed() * firstPhase))
            curTexture = recMask[1];
        else
            curTexture = recMask[0];

        SetTexture(curTexture);
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
        if (curTexture != oldTexture)
        {
            oldTexture = curTexture;
            frame.material.SetTexture("_AlphaTex", tex);
            if (tex == recMask[0])
                return;
            else if (tex == recMask[4])
                SoundManager.Instance.PlaySe(SE.DodgeGaugeMax);
            else
                SoundManager.Instance.PlaySe(SE.DodgeGaugeCharge);
        }
    }
}
