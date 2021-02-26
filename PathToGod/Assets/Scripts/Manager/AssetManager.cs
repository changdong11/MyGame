using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu ]
public class AssetManager : ScriptableObject
{
    private static AssetManager instance;

    public static AssetManager Instance
    {
        //属性访问器
        get
        {
            //保证第一次实例化之后的每次实例化都不会new，而是直接返回。
            if (instance == null)
            {
                instance = Resources.Load<AssetManager>("AssetVars");
            }
            return instance;
        }
    }
    //背景图
    public Sprite[] BackGrounds = new Sprite[4];
    //玩家皮肤
    public Sprite[] PlayerSkin = new Sprite[4];
    //平台皮肤
    public Sprite[] PlatformSkin = new Sprite[4];
    //平台预设
    public GameObject PlatformPrefrab;
    //玩家预设
    public GameObject PlayerPrefrab;
    //下一个平台的x y 增量
    public float nextPlatformX = 0.554f, nextPlatformY = 0.645f;
    //平台初始位置
    public Vector3 PlatformStartPos = new Vector3();
    //玩家初始位置
    public Vector3 PlayerStartPos = new Vector3();
}
