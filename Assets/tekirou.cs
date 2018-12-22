using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class tekirou : MonoBehaviour {

    public Vector3[] vec;

    [SerializeField]
    private float loopSpeed;

    // Use this for initialization
    void Start () {
        transform.DOLocalPath(vec, loopSpeed, PathType.Linear)
            .SetOptions(true).SetEase(Ease.Linear)
            .SetLoops(-1);

    }
	
}
