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
    //普通平台预设
    private GameObject platformPrefab;
    //障碍平台预设
    private GameObject platformGroupPrefab;
    private GameObject PlayerObj;
    private Sprite platformSkin;

    private void Awake()
    {
        isLeft = false;
        spawnPlatformCount = 5;
        //生成平台
        //platformPos = AssetManager.Instance.PlatformStartPos;
        //platformPrefab = AssetManager.Instance.PlatformPrefrab;
        ////随机平台皮肤
        //platformSkin = AssetManager.Instance.PlatformSkin[Random.Range(1,4)];
        platformPrefab.GetComponent<SpriteRenderer>().sprite = platformSkin;
        for (int i = 0; i < 5; i++)
        {
            Debug.Log("spawnPlatformCount----" + spawnPlatformCount);
            DecidePath();
            
        }
        //生成玩家
        //PlayerObj = Instantiate(AssetManager.Instance.PlayerPrefrab, gameObject.transform);
        //PlayerObj.AddComponent<PlayerControl>();
        //EventCenter.AddListener(EventType.GeneratePlatform,DecidePath);
    }
    
    private void OnDestroy()
    {
        //EventCenter.RemoveListener(EventType.GeneratePlatform, DecidePath);
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
        if (spawnPlatformCount > 0)
        {
            Debug.Log("生成普通平台----");
            SpawnNormalPlatform();

        }
        else if(spawnPlatformCount == 0)
        {
            int ran = Random.Range(1, 3);
            //生成通用障碍
            if (ran == 1 )
            {
                SpawnGrounpPlatform();
            }
            //生成钉子
            else
            {
                SpawnNailPlatform();

            }
           
            
        }

        //判断左右  确定下一个生成位置
        if (isLeft)
        {
            //platformPos = new Vector3(platformPos.x-AssetManager.Instance.nextPlatformX,
            //    platformPos.y+AssetManager.Instance.nextPlatformY,0);
        }
        else
        {
            //platformPos = new Vector3(platformPos.x + AssetManager.Instance.nextPlatformX,
            //    platformPos.y + AssetManager.Instance.nextPlatformY, 0);
        }
    }
    /// <summary>
    /// 生成普通平台（单个）
    /// </summary>
    void SpawnNormalPlatform()
    {
        GameObject tmp = Instantiate(platformPrefab, gameObject.transform);
        tmp.transform.position = platformPos;
        PlatformSkin skinScripts = tmp.GetComponent<PlatformSkin>();
        if (skinScripts == null)
        {
            skinScripts = tmp.AddComponent<PlatformSkin>();
        }
    }
    /// <summary>
    /// 生成障碍物平台（组合）
    /// </summary>
    void SpawnGrounpPlatform()
    {
        //GameObject tmp = Instantiate(AssetManager.Instance.ObstaclePlatformPrefrab, gameObject.transform);
        //tmp.transform.position = platformPos;
        //PlatformSkin skinScripts = tmp.GetComponent<PlatformSkin>();
        //if (skinScripts == null)
        //{
        //    skinScripts = tmp.AddComponent<PlatformSkin>();
        //}
        //skinScripts.InitObstacleSprite(RandomObstacleSkin());
        //skinScripts.InitNormalSprite(platformSkin);
        //skinScripts.SetLeft(isLeft);
    }
    /// <summary>
    /// 生成 钉子障碍平台
    /// </summary>
    void SpawnNailPlatform()
    {
        //GameObject tmp = Instantiate(AssetManager.Instance.NailPlatformPrefrab, gameObject.transform);
        //tmp.transform.position = platformPos;
        //PlatformSkin skinScripts = tmp.GetComponent<PlatformSkin>();
        //if (skinScripts == null)
        //{
        //    skinScripts = tmp.AddComponent<PlatformSkin>();
        //}
        //skinScripts.InitObstacleSprite(platformSkin);
        //skinScripts.InitNormalSprite(platformSkin);
        //skinScripts.SetNailLeft(isLeft);
    }
    /// <summary>
    /// 随机返回障碍物皮肤
    /// </summary>
    /// <returns></returns>
    Sprite RandomObstacleSkin()
    {
        //int ran = Random.Range(0, AssetManager.Instance.ObstaclePlatformSkin.Count);
        //return AssetManager.Instance.ObstaclePlatformSkin[ran];
        return null;
    }
}
