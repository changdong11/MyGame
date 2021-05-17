using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
//玩家控制类
public class PlayerControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //transform.position = AssetManager.Instance.PlayerStartPos;
        isJumping = false;
    }
    bool isMoveLeft;
    //下一个左边的位置
    Vector3 nextLeftPos;
    //下一个右边的位置
    Vector3 nextRightPos;
    //当前的位置
    Vector3 currentPos;
    bool isJumping;
    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 1)
        {
            if (Input.touches[0].phase == TouchPhase.Began && isJumping == false)
            {
                //EventCenter.Broadcast(EventType.GeneratePlatform);
                isJumping = true;
                Vector3 mousePos = Input.mousePosition;
                if (mousePos.x <= Screen.width / 2)
                {
                    isMoveLeft = true;
                }
                else
                {
                    isMoveLeft = false;
                }
                Jump();
            }
        }
        /*
        if (Input.GetMouseButtonDown(1) && isJumping == false)
        {
            EventCenter.Broadcast(EventType.GeneratePlatform);
            isJumping = true;
            Vector3 mousePos = Input.mousePosition;
            if (mousePos.x <= Screen.width / 2)
            {
                isMoveLeft = true;
            }
            else
            {
                isMoveLeft = false;
            }
            Jump();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.position = AssetManager.Instance.PlayerStartPos;
        }
        */
    }
    void Jump()
    {
        if (isMoveLeft)
        {
            transform.localScale = new Vector3(-1,1,1);
            transform.DOMoveX(nextLeftPos.x, 0.2f);
            transform.DOMoveY(nextLeftPos.y+0.8f, 0.2f);

        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
            transform.DOMoveX(nextRightPos.x, 0.2f);
            transform.DOMoveY(nextRightPos.y + 0.8f, 0.15f);
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("platform"))
        {
            isJumping = false;
            currentPos = collision.transform.position;
            //nextLeftPos = new Vector3(currentPos.x - AssetManager.Instance.nextPlatformX, 
            //    currentPos.y + AssetManager.Instance.nextPlatformY, 0);
            //nextRightPos = new Vector3(currentPos.x + AssetManager.Instance.nextPlatformX,
            //    currentPos.y + AssetManager.Instance.nextPlatformY, 0);
        }
        

    }
}
