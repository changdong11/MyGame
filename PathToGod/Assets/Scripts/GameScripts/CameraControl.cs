using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CameraControl : MonoBehaviour
{
    GameObject player;
    Vector3 offect;
    Vector3 start;
    Vector2 velocity;
    float time;
    float speed = 5;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        print(player.transform.name);
        offect = transform.position - player.transform.position;
        time = 0;
        start = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time < 0.1)
        {
            //return;
        }
        
        if (player != null)
        {
            print("更新一次位置");
            Vector3 pos = player.transform.position + offect;
            if (transform.position.y < pos.y)
            {
                transform.DOMove(pos,0.1f);
                //transform.position = pos;
                

            }
        }
        time = 0;
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.DOMove(start, 1f);
        }
    }
}
