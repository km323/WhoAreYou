using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DodgeTutorial : MonoBehaviour
{
    private Sequence s;

    // Use this for initialization
    void Start()
    {
        s = DOTween.Sequence();

        s.Append(
             transform.DORotate(new Vector3(0, 89, 0), 0.3f)
        )
        .Append(
             transform.DORotate(Vector3.zero, 0.3f)
        );
    }

    // Update is called once per frame
    public void DodgeAttack()
    {
        if (!s.IsPlaying())
            s.Restart();
    }
}
