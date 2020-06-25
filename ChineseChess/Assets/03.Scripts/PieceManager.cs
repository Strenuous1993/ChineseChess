using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PieceManager : MonoBehaviour
{
    //初始化14种棋子的预制体
    public GameObject che_black;
    public GameObject ma_black;
    public GameObject xiang_black;
    public GameObject shi_black;
    public GameObject jiang_black;
    public GameObject pao_black;
    public GameObject bing_black;

    public GameObject che_red;
    public GameObject ma_red;
    public GameObject xiang_red;
    public GameObject shi_red;
    public GameObject jiang_red;
    public GameObject pao_red;
    public GameObject bing_red;
    public GameObject chess_board;
    private Dictionary<string, GameObject> openWith = new Dictionary<string, GameObject>();

    void Start()
    {
        //将坐标信息和棋子加入字典
        //红方
        openWith.Add("00", che_red);
        openWith.Add("08", che_red);
        openWith.Add("01", ma_red);
        openWith.Add("07", ma_red);
        openWith.Add("02", xiang_red);
        openWith.Add("06", xiang_red);
        openWith.Add("03", shi_red);
        openWith.Add("05", shi_red);
        openWith.Add("04", jiang_red);
        openWith.Add("21", pao_red);
        openWith.Add("27", pao_red);
        openWith.Add("30", bing_red);
        openWith.Add("32", bing_red);
        openWith.Add("34", bing_red);
        openWith.Add("36", bing_red);
        openWith.Add("38", bing_red);

        //黑方
        openWith.Add("90", che_black);
        openWith.Add("98", che_black);
        openWith.Add("91", ma_black);
        openWith.Add("97", ma_black);
        openWith.Add("92", xiang_black);
        openWith.Add("96", xiang_black);
        openWith.Add("93", shi_black);
        openWith.Add("95", shi_black);
        openWith.Add("94", jiang_black);
        openWith.Add("71", pao_black);
        openWith.Add("77", pao_black);
        openWith.Add("60", bing_black);
        openWith.Add("62", bing_black);
        openWith.Add("64", bing_black);
        openWith.Add("66", bing_black);
        openWith.Add("68", bing_black);
        InitialPiece();
    }
    void Awake()
    {

    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void InitialPiece()
    {
        foreach (KeyValuePair<string,GameObject> kvp in openWith)
        {
            //获得父级Object
            //GameObject obj1 = GameObject.Find("Coordinates");
            GameObject zuobiao = GameObject.Find(kvp.Key);
            //在相应坐标未知生成物体
            Instantiate(kvp.Value,zuobiao.transform.position,Quaternion.identity,chess_board.transform);
            //Debug.Log(zuobiao.name);
          
        }
    }


}
