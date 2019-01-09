using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotionTutorial : MonoBehaviour {
    [SerializeField]
    private float timeScale = 0.2f;
    [SerializeField]
    private GameObject postCamera;
    [SerializeField]
    private Transform player;

    private Vector3 distance;

    // Use this for initialization
    void Start () {
        distance = transform.position - player.position;
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = player.position + distance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == player.gameObject.layer)
            return;

        if (collision.tag == "Bullet")
        {
            postCamera.SetActive(true);
            Time.timeScale = timeScale;
            Invoke("DelayResetTime", 0.2f)
;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            Time.timeScale = 1f;
            postCamera.SetActive(false);
        }
    }

    private void DelayResetTime()
    {
        Time.timeScale = 1f;
        postCamera.SetActive(false);
    }
}
