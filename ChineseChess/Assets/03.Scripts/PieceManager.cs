using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PieceManager : MonoBehaviour
{
    //初始化14种棋子的预制体
    public GameObject che_black_1;
    public GameObject che_black_2;

    public GameObject ma_black_1;
    public GameObject ma_black_2;

    public GameObject xiang_black_1;
    public GameObject xiang_black_2;

    public GameObject shi_black_1;
    public GameObject shi_black_2;

    public GameObject jiang_black;

    public GameObject pao_black_1;
    public GameObject pao_black_2;

    public GameObject bing_black_1;
    public GameObject bing_black_2;
    public GameObject bing_black_3;
    public GameObject bing_black_4;
    public GameObject bing_black_5;

    public GameObject che_red_1;
    public GameObject che_red_2;

    public GameObject ma_red_1;
    public GameObject ma_red_2;

    public GameObject xiang_red_1;
    public GameObject xiang_red_2;

    public GameObject shi_red_1;
    public GameObject shi_red_2;

    public GameObject jiang_red;

    public GameObject pao_red_1;
    public GameObject pao_red_2;

    public GameObject bing_red_1;
    public GameObject bing_red_2;
    public GameObject bing_red_3;
    public GameObject bing_red_4;
    public GameObject bing_red_5;

    public GameObject chess_board;
    private Dictionary<string, GameObject> openWith = new Dictionary<string, GameObject>();

    //与所有棋子位置与二维数组对应
    static public GameObject[,] piece_array = new GameObject[10, 9];
    void Start()
    {
        //将坐标信息和棋子加入字典
        //红方
        openWith.Add("90", che_red_1);
        openWith.Add("98", che_red_2);
        openWith.Add("91", ma_red_1);
        openWith.Add("97", ma_red_2);
        openWith.Add("92", xiang_red_1);
        openWith.Add("96", xiang_red_2);
        openWith.Add("93", shi_red_1);
        openWith.Add("95", shi_red_2);
        openWith.Add("94", jiang_red);
        openWith.Add("71", pao_red_1);
        openWith.Add("77", pao_red_2);
        openWith.Add("60", bing_red_1);
        openWith.Add("62", bing_red_2);
        openWith.Add("64", bing_red_3);
        openWith.Add("66", bing_red_4);
        openWith.Add("68", bing_red_5);

        //黑方
        openWith.Add("00", che_black_1);
        openWith.Add("08", che_black_2);
        openWith.Add("01", ma_black_1);
        openWith.Add("07", ma_black_2);
        openWith.Add("02", xiang_black_1);
        openWith.Add("06", xiang_black_2);
        openWith.Add("03", shi_black_1);
        openWith.Add("05", shi_black_2);
        openWith.Add("04", jiang_black);
        openWith.Add("21", pao_black_1);
        openWith.Add("27", pao_black_2);
        openWith.Add("30", bing_black_1);
        openWith.Add("32", bing_black_2);
        openWith.Add("34", bing_black_3);
        openWith.Add("36", bing_black_4);
        openWith.Add("38", bing_black_5);
        InitialPiece();
        PieceMapArray();
        SetFirstPieceAllPower();

    }
    void Awake()
    {

    }
    // Update is called once per frame
    void Update()
    {
    }

    //初始化棋子
    public void InitialPiece()
    {
        foreach (KeyValuePair<string, GameObject> kvp in openWith)
        {
            //获得父级Object
            //GameObject obj1 = GameObject.Find("Coordinates");
            GameObject zuobiao = GameObject.Find(kvp.Key);
            //在相应坐标位置生成物体
            Instantiate(kvp.Value, zuobiao.transform.position, Quaternion.identity, chess_board.transform);
            //设置棋子的xy坐标
            kvp.Value.GetComponent<Piece>().coor_x = int.Parse(zuobiao.transform.name) / 10;
            kvp.Value.GetComponent<Piece>().coor_y = int.Parse(zuobiao.transform.name) % 10;

            //Debug.Log(zuobiao.name);
        }
    }

    //遍历棋盘，将棋子放到对应棋盘数组下标位置上
    public void PieceMapArray()
    {
        List<Transform> lst = new List<Transform>();
        foreach (Transform child in GameObject.Find("Coordinates").transform)
        {
            if (child.transform.tag == "BlackPiece" || child.transform.tag == "RedPiece")
            {
                GameObject temp = GameObject.Find(child.name);
                piece_array[child.GetComponent<Piece>().coor_x, child.GetComponent<Piece>().coor_y] = temp;
            }
        }
    }

    //根据棋盘数组改变场景中棋子的位置
    public void ArrayMapPiece()
    {
        foreach (GameObject gameObject in piece_array)
        {
            if (gameObject != null && gameObject.GetComponent<Piece>().moved)
            {
                int x = gameObject.GetComponent<Piece>().coor_x;
                int y = gameObject.GetComponent<Piece>().coor_y;
                string dec = x.ToString();
                string unit = y.ToString();
                GameObject coord = GameObject.Find(dec + unit);
                gameObject.transform.localPosition = coord.transform.localPosition;
            }
        }
    }

    //设置第一次初始化各棋子的power
    public void SetFirstPieceAllPower()
    {
        foreach (GameObject gameObject in piece_array)
        {
            if (gameObject != null)
            {
                gameObject.GetComponent<Piece>().PiecePowerSet();
                gameObject.GetComponent<Piece>().PieceAttackPowerSet();
            }
        }
    }

    //棋力计算
    public int PiecePower()
    {
        return 0;
    }
}
