using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordTutorial : MonoBehaviour {
    [SerializeField]
    private PlayerControlTutorial control;

    private List<Vector3> recordList;
    private Shot shot;

    private Vector3 startPosition;
    public Vector3 GetStartPos()
    {
        return recordList[0];
    }

    // Use this for initialization
    void Awake () {
        shot = GetComponent<Shot>();
        recordList = new List<Vector3>();
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

    IEnumerator PlayRecord()
    {
        int index = 0;
        bool goForward = true;

        while (true)
        {
            transform.position = new Vector3(recordList[index].x, recordList[index].y, transform.position.z);

            if (recordList[index].z == 1)
                shot.ShotBullet();

            if (index == recordList.Count - 1)
                yield return new WaitForSeconds(0.5f);

            if (goForward)
                index++;
            else
                index--;

            if (index == recordList.Count - 1)
                goForward = false;
            else if (index == 0)
                goForward = true;

            yield return null;
        }
    }

    IEnumerator Record()
    {
        while (gameObject != null)
        {
            int z = control.GetPlayerInput().SameTimeTap ? 1 : 0;

            Vector3 recordTmp = new Vector3(transform.position.x, transform.position.y, z);
            recordList.Add(recordTmp);
            yield return null;
        }
    }
}
