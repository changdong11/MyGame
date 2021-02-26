using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformControl : MonoBehaviour
{
    //下一个平台生成方向
    private bool isLeft;
    //预 生成数量
    private int spawnPlatformCount;
    private Vector3 platformPos;
    private GameObject platformPrefab;
    private GameObject PlayerObj;

    private void Awake()
    {
        isLeft = false;
        spawnPlatformCount = 5;
        //生成平台
        platformPos = AssetManager.Instance.PlatformStartPos;
        platformPrefab = AssetManager.Instance.PlatformPrefrab;
        for (int i = 0; i < 5; i++)
        {
            DecidePath();
            
        }
        //生成玩家
        PlayerObj = Instantiate(AssetManager.Instance.PlayerPrefrab, gameObject.transform);
        PlayerObj.AddComponent<PlayerControl>();
        EventCenter.AddListener(EventType.GeneratePlatform,DecidePath);
    }
    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventType.GeneratePlatform, DecidePath);
    }
    /// <summary>
    /// 确定路径方向
    /// </summary>
    void DecidePath()
    {
        if (spawnPlatformCount > 0 )
        {
            spawnPlatformCount--;
        }
        else
        {
            isLeft = !isLeft;
            spawnPlatformCount = Random.Range(1, 5);
        }
        SpawnPlatform();
    }
    /// <summary>
    /// 生成平台
    /// </summary>
    void SpawnPlatform()
    {
        SpawnNormalPlatform();
        //判断左右  确定下一个生成位置
        if (isLeft)
        {
            platformPos = new Vector3(platformPos.x-AssetManager.Instance.nextPlatformX,
                platformPos.y+AssetManager.Instance.nextPlatformY,0);
        }
        else
        {
            platformPos = new Vector3(platformPos.x + AssetManager.Instance.nextPlatformX,
                platformPos.y + AssetManager.Instance.nextPlatformY, 0);
        }
    }
    /// <summary>
    /// 生成普通平台（单个）
    /// </summary>
    void SpawnNormalPlatform()
    {
        GameObject tmp = Instantiate(platformPrefab, gameObject.transform);
        tmp.transform.position = platformPos;
        //Debug.Log(tmp.transform.position);
        //Debug.Log(platformPos);
        //Debug.Log("-------------------------");
    }
}
