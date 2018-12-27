using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// リプレイ時のカメラエフェクト
/// </summary>

namespace Kino
{
    public class CameraEffect : MonoBehaviour {
        private DigitalGlitch digitalGlitch;
        private AnalogGlitch analogGlitch;
        private Sequence s;


        // Use this for initialization
        void Start() {
            digitalGlitch = Camera.main.GetComponent<DigitalGlitch>();
            analogGlitch = Camera.main.GetComponent<AnalogGlitch>();

            s = DOTween.Sequence();

            StartCameraEffect();
        }

        // Update is called once per frame
        void Update() {

        }

        private void StartCameraEffect()
        {
            s.Append(DOTween.To(() => digitalGlitch.intensity
                ,num => digitalGlitch.intensity = num
                ,0.8f,1f)
            );
            s.Append(DOTween.To(() => digitalGlitch.intensity
            , num => digitalGlitch.intensity = num
            , 0, 1f)
            );
        }

    }
}
