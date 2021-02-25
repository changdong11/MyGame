using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AppMain : MonoBehaviour
{
    private Button startGame;
    private Button skin;
    private Button reset;
    private Button ranking;
    private Button volume;
    void Start()
    {
        FindComponent();
        BindEvent();
    }
    void FindComponent()
    {
        startGame = transform.Find("Canvas/StartGame").GetComponent<Button>();
        skin = transform.Find("Canvas/Button/skin").GetComponent<Button>();
        reset = transform.Find("Canvas/Button/reset").GetComponent<Button>();
        ranking = transform.Find("Canvas/Button/ranking").GetComponent<Button>();
        volume = transform.Find("Canvas/Button/volume").GetComponent<Button>();
    }
    void BindEvent()
    {
        startGame.onClick.AddListener(StartGameFun);
        skin.onClick.AddListener(SkinFun);
        reset.onClick.AddListener(ResetGameFun);
        ranking.onClick.AddListener(RankingFun);
        volume.onClick.AddListener(VolumeFun);
    }
    void StartGameFun()
    {

    }
    void SkinFun()
    {

    }
    void ResetGameFun()
    {

    }
    void RankingFun()
    {

    }
    void VolumeFun()
    {

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
