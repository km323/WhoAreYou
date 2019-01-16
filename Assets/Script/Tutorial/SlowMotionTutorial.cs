using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotionTutorial : MonoBehaviour
{
    [SerializeField]
    private float timeScale = 0.2f;
    [SerializeField]
    private GameObject postCamera;
    [SerializeField]
    private Transform player;
    [SerializeField]
    private GameObject linePrefab;

    private Vector3 distance;
    private GameObject bulletNearPlayer;
    // Use this for initialization
    void Start()
    {
        postCamera.SetActive(false);
        distance = transform.position - player.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position + distance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == player.gameObject.layer)
            return;

        if (collision.tag == "Bullet")
        {
            Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
            bulletNearPlayer = collision.transform.GetChild(0).gameObject;
            bulletNearPlayer.AddComponent<GhostSprites>();
            postCamera.SetActive(true);
            Time.timeScale = timeScale;
            Invoke("DelayResetTime", 0.2f);
            SoundManager.Instance.PlaySe(SE.SlowMotion);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            Time.timeScale = 1f;
            postCamera.SetActive(false);
            foreach(GhostSprites ghost in FindObjectsOfType<GhostSprites>())
                Destroy(ghost);
        }
    }

    private void DelayResetTime()
    {
        Time.timeScale = 1f;
        postCamera.SetActive(false);
        foreach (GhostSprites ghost in FindObjectsOfType<GhostSprites>())
            Destroy(ghost);
    }
}
