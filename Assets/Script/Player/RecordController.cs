using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordController : MonoBehaviour {
    private List<Vector3> recordList;
    private Shot shot;

    private int[] signalArray;

    [SerializeField]
    private SpriteRenderer frameObjRenderer;
    [SerializeField]
    private Sprite playSprite;

    [SerializeField]
    private int signalFrame;

    private const int beforSignal = 1;
    private const int afterSignal = -1;

    private StageManager stageManager;
    private Sprite signalSprite;
    private Vector3 startPosition;

    public Vector3 GetStartPos()
    {
        if (recordList != null && recordList.Count > 0)
            return recordList[0];

        return startPosition;
    }

    void Awake () {
        shot = GetComponent<Shot>();
        recordList = new List<Vector3>();
        stageManager = GameObject.Find("StageManager").GetComponent<StageManager>();
        signalSprite = SetPlayerPlaySprite();

        GetComponent<PlayerCollision>().OnBulletHit += StopPlayRecord;
        GameMain.OnNextGame += StopPlayRecord;

        startPosition = transform.position;
    }

    private void OnDestroy()
    {
        GetComponent<PlayerCollision>().OnBulletHit -= StopPlayRecord;
        GameMain.OnNextGame -=  StopPlayRecord;
    }

    private Sprite SetPlayerPlaySprite()
    {
        Sprite sprite = null;
        if (gameObject.layer == 9)
            sprite = stageManager.GetPlayerBlackPlay();
        if (gameObject.layer == 10)
            sprite = stageManager.GetPlayerWhitePlay();

        return sprite;
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
        CreateSignalArray();
    }
    public void StartPlayRecord()
    {
        StartCoroutine("PlayRecord");
    }
    public void StopPlayRecord()
    {
        StopCoroutine("PlayRecord");
    }

    private void CreateSignalArray()//予備動作のための配列を作る
    {
        signalArray = new int[recordList.Count];
        
        for(int i=0;i<recordList.Count;i++)
        {
            if (recordList[i].z == 1)
            {
                if (i - signalFrame >= 0)//配列外の時の例外処理
                    signalArray[i - signalFrame] = beforSignal;
                else
                    signalArray[0] = beforSignal;

                if (i + signalFrame < recordList.Count - 2)
                    signalArray[i + signalFrame] = afterSignal;
                else
                    signalArray[recordList.Count - 2] = afterSignal;
            }
        }
    }

    private void SignalSprite()
    {
        frameObjRenderer.sprite = signalSprite;
    }
    private void PlaySprite()
    {
        frameObjRenderer.sprite = playSprite;
    }

    //プレイヤーの動きを再生するメソッド
    IEnumerator PlayRecord()
    {
        int index = 1;
        bool goForward = true;

        //最初の一回
        transform.position = new Vector3(recordList[0].x, recordList[0].y, transform.position.z);

        if (stageManager.GetNeedToReset())
            yield return new WaitForSeconds(StageManager.EffectWaitInterval);
        
        yield return new WaitForSeconds(1f);

        while (true)
        {
            transform.position = new Vector3(recordList[index].x, recordList[index].y, transform.position.z);


            if (recordList[index].z == 1)
            {
                shot.ShotBullet();
                PlaySprite();//通常のspriteに変える
            }
            

            if (/*index == 0 || */index == recordList.Count - 1)
                yield return new WaitForSeconds(0.5f);

            if (goForward)
            {
                if (signalArray[index] == beforSignal)//予備動作のspriteに変える
                    SignalSprite();
                index++;
            }
            else
            {
                if (signalArray[index] == afterSignal)
                    SignalSprite();
                index--;
            }

            if (index == recordList.Count - 1)
                goForward = false;
            else if (index == 0)
                goForward = true;

            yield return null;
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

            yield return null;
        }
    }
}
