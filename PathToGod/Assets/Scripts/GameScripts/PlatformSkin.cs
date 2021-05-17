using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//挂载在平台预制  用于赋值sprite
public class PlatformSkin : MonoBehaviour
{
    SpriteRenderer obstacleSprite;
    /// <summary>
    /// 初始化障碍物皮肤
    /// </summary>
    public void InitObstacleSprite(Sprite skin)
    {
        obstacleSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();
        obstacleSprite.sprite = skin;
    }
    SpriteRenderer normalSprite;
    /// <summary>
    /// 初始化障碍物皮肤
    /// </summary>
    public void InitNormalSprite(Sprite skin)
    {
        normalSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
        normalSprite.sprite = skin;
    }
    /// <summary>
    /// 通用障碍平台 设置左右
    /// </summary>
    /// <param name="isLeft"></param>
    public void SetLeft(bool isLeft)
    {
        if (isLeft)
        {
            transform.GetChild(1).localPosition = new Vector3(Mathf.Abs(transform.GetChild(1).localPosition.x) , transform.GetChild(1).localPosition.y,0);
        }
        else
        {
            transform.GetChild(1).localPosition = new Vector3(-Mathf.Abs(transform.GetChild(1).localPosition.x), transform.GetChild(1).localPosition.y, 0);
        }
    }
    /// <summary>
    /// 钉子平台设置左右
    /// </summary>
    /// <param name="isLeft"></param>
    public void SetNailLeft(bool isLeft)
    {
        float nail = transform.GetChild(2).localPosition.x;
        Debug.Log("111111---" + nail);
        Debug.Log("222222---" + (-Mathf.Abs(nail)));
        if (isLeft)
        {
            transform.GetChild(1).localPosition = new Vector3(Mathf.Abs(transform.GetChild(1).localPosition.x), transform.GetChild(1).localPosition.y, 0);
            transform.GetChild(2).localPosition = new Vector3(Mathf.Abs(transform.GetChild(2).localPosition.x), transform.GetChild(2).localPosition.y, 0);
        }
        else
        {
            transform.GetChild(1).localPosition = new Vector3(-Mathf.Abs(transform.GetChild(1).localPosition.x), transform.GetChild(1).localPosition.y, 0);
            transform.GetChild(2).localPosition = new Vector3(-Mathf.Abs(transform.GetChild(2).localPosition.x), transform.GetChild(2).localPosition.y, 0);
        }
    }

}
