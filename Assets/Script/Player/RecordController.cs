using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordController : MonoBehaviour {
    private List<Vector3> recordList;
    private Shot shot;

    void Awake () {
        shot = GetComponent<Shot>();
        recordList = new List<Vector3>();
    }

    private void OnEnable()
    {
        if (recordList == null || recordList.Count <= 0)
            return;

        StartPlayRecord();
    }

	private void OnDisable()
    {
        StopPlayRecord();
    }

    public void StartRecord()
    {
        StartCoroutine("Record");     
    }
    public void StopRecord()
    {
        StopCoroutine("Record");
    }
    public void StartPlayRecord()
    {
        StartCoroutine("PlayRecord");
    }
    public void StopPlayRecord()
    {
        StopCoroutine("PlayRecord");
    }

    //プレイヤーの動きを再生するメソッド
    IEnumerator PlayRecord()
    {
        int index = 0;
        bool goForward = true;

        while(true)
        {
            transform.position = new Vector3(recordList[index].x, recordList[index].y, transform.position.z);

            if (recordList[index].z == 1)
                shot.ShotBullet();

            if (/*index == 0 || */index == recordList.Count - 1)
                yield return new WaitForSeconds(0.8f);

            if (goForward)
                index++;
            else
                index--;

            if (index == recordList.Count - 1)
                goForward = false;
            else if (index == 0)
                goForward = true;

            yield return new WaitForFixedUpdate();
        }
    }

    //プレイヤーの動きを記録するメソッド
    IEnumerator Record()
    {
        while (gameObject != null)
        {
            //弾撃ったかどうかをvector3のzとして保存する
            //撃つと1、そうじゃないと0
            int z = PlayerController.GetPlayerInput().SameTimeTap ? 1 : 0;
            Vector3 recordTmp = new Vector3(transform.position.x, transform.position.y, z);

            recordList.Add(recordTmp);

            yield return new WaitForFixedUpdate();
        }
    }
}
