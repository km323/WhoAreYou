using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// リプレイ時のカメラエフェクト
/// </summary>

public class CameraEffect{
    private DigitalGlitch digitalGlitch;
    private AnalogGlitch analogGlitch;
    private Sequence s;

    // Use this for initialization
    public CameraEffect(){
        digitalGlitch = Camera.main.GetComponent<DigitalGlitch>();
        analogGlitch = Camera.main.GetComponent<AnalogGlitch>();

        s = DOTween.Sequence();
        StartCameraEffect();
    }

    private void StartCameraEffect()
    {
        //s.Join(DOTween.To(() => analogGlitch.horizontalShake
        //    , num => analogGlitch.horizontalShake = num
        //    , 0.05f, 0.1f)
        //)
        s.PrependCallback(() => analogGlitch.horizontalShake = 0.05f)
        .AppendCallback(() => digitalGlitch.enabled = true)
        .AppendCallback(() => analogGlitch.enabled = true)
        .Append(DOTween.To(() => digitalGlitch.intensity
            , num => digitalGlitch.intensity = num
            , 0.8f, 1f)
        )
        .Append(DOTween.To(() => digitalGlitch.intensity
        , num => digitalGlitch.intensity = num
        , 0, 1f)
        )
        .AppendCallback(() => analogGlitch.horizontalShake = 0f);
    }

    public void Play()
    {
        s.Restart();
    }

    public void Stop()
    {
        s.Pause();
    }
}