using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PathToGodGame : MonoBehaviour
{
    private Button play;
    private Text performance;
    private Text diamond;
    // Start is called before the first frame update
    void Start()
    {
        play = transform.Find("Canvas/play").GetComponent<Button>();
        performance = transform.Find("Canvas/Text").GetComponent<Text>();
        diamond = transform.Find("Canvas/diamond/num").GetComponent<Text>();
        BindEvent();
    }
    void BindEvent()
    {
        play.onClick.AddListener(ClickPlayFun);
    }
    void ClickPlayFun()
    {

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
