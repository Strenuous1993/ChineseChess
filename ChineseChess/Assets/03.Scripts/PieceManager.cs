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
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                piece_array[i, j] = null;
            }
        }
        foreach (Transform child in GameObject.Find("Coordinates").transform)
        {
            if (child.transform.tag == "BlackPiece" || child.transform.tag == "RedPiece")
            {
                GameObject temp = child.gameObject;
                piece_array[child.GetComponent<Piece>().coor_x, child.GetComponent<Piece>().coor_y] = temp;
            }
        }
    }

    //根据棋盘数组改变场景中棋子的位置
    static public void ArrayMapPiece()
    {
        foreach (Transform child in GameObject.Find("Coordinates").transform)
        {
            if (child.transform.tag == "BlackPiece" || child.transform.tag == "RedPiece")
            {
                foreach (GameObject gameObject in PieceManager.piece_array)

                {
                    if ( gameObject != null && child.gameObject.GetComponent<Piece>().piece_id == gameObject.GetComponent<Piece>().piece_id)
                    {
                        int x = gameObject.GetComponent<Piece>().coor_x;
                        int y = gameObject.GetComponent<Piece>().coor_y;
                        string dec = x.ToString();
                        string unit = y.ToString();
                        GameObject coord = GameObject.Find(dec + unit);
                        child.transform.localPosition = coord.transform.localPosition;
                        child.gameObject.GetComponent<Piece>().is_destory = false;
                        break;
                    }
                }
                if(child.gameObject.GetComponent<Piece>().is_destory == true)
                {
                    Destroy(child.gameObject);
                }
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

    //棋力静态评估函数
    static public double PiecePower(GameObject[,] piece_array)
    {
        int attack_power_red = 0;
        int power_red = 0;
        int attack_power_black = 0;
        int power_black = 0;
        double total_power;
        foreach (GameObject gameObject in piece_array)
        {
            if (gameObject != null && gameObject.transform.tag == "RedPiece")
            {
                power_red -= gameObject.GetComponent<Piece>().piece_power;
                attack_power_red -= gameObject.GetComponent<Piece>().piece_attack_power;
            }
            else if (gameObject != null && gameObject.transform.tag == "BlackPiece")
            {
                power_black += gameObject.GetComponent<Piece>().piece_power;
                attack_power_black += gameObject.GetComponent<Piece>().piece_attack_power;
            }
        }
        System.Random rd = new System.Random();
        total_power = power_red + power_black + attack_power_black + attack_power_red + rd.NextDouble() * rd.NextDouble();
        return total_power;
    }

    //获得黑方所有的moves
    static public void GetAllBlackMoves(GameObject[,] piece_array, List<GameObject[,]> all_next_move)
    {
        foreach (GameObject gameObject in piece_array)
        {
            GameObject[,] temp = new GameObject[10, 9];

            if (gameObject != null)
            {
                int x = gameObject.GetComponent<Piece>().coor_x;
                int y = gameObject.GetComponent<Piece>().coor_y;
                switch (gameObject.transform.name)
                {
                    case "black_bing(Clone)":
                        {
                            if (x < 5 && (piece_array[x + 1, y] == null || piece_array[x + 1, y].transform.tag == "RedPiece"))
                            {
                                temp = (GameObject[,])piece_array.Clone();
                                temp[x + 1, y] = temp[x, y];
                                temp[x, y] = null;
                                all_next_move.Add(temp);
                            }
                            if (x >= 5 && x + 1 < 10 && (piece_array[x + 1, y] == null || piece_array[x + 1, y].transform.tag == "RedPiece"))
                            {
                                temp = (GameObject[,])piece_array.Clone();
                                temp[x + 1, y] = temp[x, y];
                                temp[x, y] = null;
                                all_next_move.Add(temp);
                            }
                            if (x >= 5 && y - 1 > -1 && (piece_array[x, y - 1] == null || piece_array[x, y - 1].transform.tag == "RedPiece"))
                            {
                                temp = (GameObject[,])piece_array.Clone();
                                temp[x, y - 1] = temp[x, y];
                                temp[x, y] = null;
                                all_next_move.Add(temp);
                            }
                            if (x >= 5 && y + 1 < 9 && (piece_array[x, y + 1] == null || piece_array[x, y + 1].transform.tag == "RedPiece"))
                            {
                                temp = (GameObject[,])piece_array.Clone();
                                temp[x, y + 1] = temp[x, y];
                                temp[x, y] = null;
                                all_next_move.Add(temp);
                            }
                            break;
                        }
                    case "black_che(Clone)":
                        {
                            for (int i = x + 1; i < 10; i++)
                            {
                                if (piece_array[i, y] == null)
                                {
                                    temp = (GameObject[,])piece_array.Clone();
                                    temp[i, y] = temp[x, y];
                                    temp[x, y] = null;
                                    all_next_move.Add(temp);
                                }
                                else if (piece_array[i, y] != null && piece_array[i, y].transform.tag == "RedPiece")
                                {
                                    temp = (GameObject[,])piece_array.Clone();
                                    temp[i, y] = temp[x, y];
                                    temp[x, y] = null;
                                    all_next_move.Add(temp);
                                    break;
                                }
                                else if (piece_array[i, y] != null && piece_array[i, y].transform.tag == "BlackPiece")
                                {
                                    break;
                                }

                            }

                            for (int i = x - 1; i >= 0; i--)
                            {
                                if (piece_array[i, y] == null)
                                {
                                    temp = (GameObject[,])piece_array.Clone();
                                    temp[i, y] = temp[x, y];
                                    temp[x, y] = null;
                                    all_next_move.Add(temp);
                                }
                                else if (piece_array[i, y] != null && piece_array[i, y].transform.tag == "RedPiece")
                                {
                                    temp = (GameObject[,])piece_array.Clone();
                                    temp[i, y] = temp[x, y];
                                    temp[x, y] = null;
                                    all_next_move.Add(temp);
                                    break;
                                }
                                else if (piece_array[i, y] != null && piece_array[i, y].transform.tag == "BlackPiece")
                                {
                                    break;
                                }
                            }

                            for (int i = y + 1; i < 9; i++)
                            {
                                if (piece_array[x, i] == null)
                                {
                                    temp = (GameObject[,])piece_array.Clone();
                                    temp[x, i] = temp[x, y];
                                    temp[x, y] = null;
                                    all_next_move.Add(temp);
                                }
                                else if (piece_array[x, i] != null && piece_array[x, i].transform.tag == "RedPiece")
                                {
                                    temp = (GameObject[,])piece_array.Clone();
                                    temp[x, i] = temp[x, y];
                                    temp[x, y] = null;
                                    all_next_move.Add(temp);
                                    break;
                                }
                                else if (piece_array[x, i] != null && piece_array[x, i].transform.tag == "BlackPiece")
                                {
                                    break;
                                }
                            }

                            for (int i = y - 1; i >= 0; i--)
                            {
                                if (piece_array[x, i] == null)
                                {
                                    temp = (GameObject[,])piece_array.Clone();
                                    temp[x, i] = temp[x, y];
                                    temp[x, y] = null;
                                    all_next_move.Add(temp);
                                }
                                else if (piece_array[x, i] != null && piece_array[x, i].transform.tag == "RedPiece")
                                {
                                    temp = (GameObject[,])piece_array.Clone();
                                    temp[x, i] = temp[x, y];
                                    temp[x, y] = null;
                                    all_next_move.Add(temp);
                                    break;
                                }
                                else if (piece_array[x, i] != null && piece_array[x, i].transform.tag == "BlackPiece")
                                {
                                    break;
                                }
                            }
                            break;
                        }
                    case "black_jiang(Clone)":
                        {
                            if (x - 1 > -1 && (piece_array[x - 1, y] == null || piece_array[x - 1, y].transform.tag == "RedPiece"))
                            {
                                temp = (GameObject[,])piece_array.Clone();
                                temp[x - 1, y] = temp[x, y];
                                temp[x, y] = null;
                                all_next_move.Add(temp);
                            }

                            if (x + 1 < 3 && (piece_array[x + 1, y] == null || piece_array[x + 1, y].transform.tag == "RedPiece"))
                            {
                                temp = (GameObject[,])piece_array.Clone();
                                temp[x + 1, y] = temp[x, y];
                                temp[x, y] = null;
                                all_next_move.Add(temp);
                            }

                            if (y - 1 > 2 && (piece_array[x, y - 1] == null || piece_array[x, y - 1].transform.tag == "RedPiece"))
                            {
                                temp = (GameObject[,])piece_array.Clone();
                                temp[x, y - 1] = temp[x, y];
                                temp[x, y] = null;
                                all_next_move.Add(temp);
                            }
                            if (y + 1 < 6 && (piece_array[x, y + 1] == null || piece_array[x, y + 1].transform.tag == "RedPiece"))
                            {
                                temp = (GameObject[,])piece_array.Clone();
                                temp[x, y + 1] = temp[x, y];
                                temp[x, y] = null;
                                all_next_move.Add(temp);
                            }
                            break;
                        }
                    case "black_ma(Clone)":
                        {
                            if (x - 2 > -1 && y + 1 < 9 && piece_array[x - 1, y] == null && (piece_array[x - 2, y + 1] == null || piece_array[x - 2, y + 1].transform.tag == "RedPiece"))
                            {
                                temp = (GameObject[,])piece_array.Clone();
                                temp[x - 2, y + 1] = temp[x, y];
                                temp[x, y] = null;
                                all_next_move.Add(temp);
                            }
                            if (x - 2 > -1 && y - 1 > -1 && piece_array[x - 1, y] == null && (piece_array[x - 2, y - 1] == null || piece_array[x - 2, y - 1].transform.tag == "RedPiece"))
                            {
                                temp = (GameObject[,])piece_array.Clone();
                                temp[x - 2, y - 1] = temp[x, y];
                                temp[x, y] = null;
                                all_next_move.Add(temp);
                            }
                            if (x + 2 < 10 && y + 1 < 9 && piece_array[x + 1, y] == null && (piece_array[x + 2, y + 1] == null || piece_array[x + 2, y + 1].transform.tag == "RedPiece"))
                            {
                                temp = (GameObject[,])piece_array.Clone();
                                temp[x + 2, y + 1] = temp[x, y];
                                temp[x, y] = null;
                                all_next_move.Add(temp);
                            }
                            if (x + 2 < 10 && y - 1 > -1 && piece_array[x + 1, y] == null && (piece_array[x + 2, y - 1] == null || piece_array[x + 2, y - 1].transform.tag == "RedPiece"))
                            {
                                temp = (GameObject[,])piece_array.Clone();
                                temp[x + 2, y - 1] = temp[x, y];
                                temp[x, y] = null;
                                all_next_move.Add(temp);
                            }
                            if (x - 1 > -1 && y + 2 < 9 && piece_array[x, y + 1] == null && (piece_array[x - 1, y + 2] == null || piece_array[x - 1, y + 2].transform.tag == "RedPiece"))
                            {
                                temp = (GameObject[,])piece_array.Clone();
                                temp[x - 1, y + 2] = temp[x, y];
                                temp[x, y] = null;
                                all_next_move.Add(temp);
                            }

                            if (x + 1 < 10 && y + 2 < 9 && piece_array[x, y + 1] == null && (piece_array[x + 1, y + 2] == null || piece_array[x + 1, y + 2].transform.tag == "RedPiece"))
                            {
                                temp = (GameObject[,])piece_array.Clone();
                                temp[x + 1, y + 2] = temp[x, y];
                                temp[x, y] = null;
                                all_next_move.Add(temp);
                            }
                            if (x - 1 > -1 && y - 2 > -1 && piece_array[x, y - 1] == null && (piece_array[x - 1, y - 2] == null || piece_array[x - 1, y - 2].transform.tag == "RedPiece"))
                            {
                                temp = (GameObject[,])piece_array.Clone();
                                temp[x - 1, y - 2] = temp[x, y];
                                temp[x, y] = null;
                                all_next_move.Add(temp);
                            }
                            if (x + 1 < 10 && y - 2 > -1 && piece_array[x, y - 1] == null && (piece_array[x + 1, y - 2] == null || piece_array[x + 1, y - 2].transform.tag == "RedPiece"))
                            {
                                temp = (GameObject[,])piece_array.Clone();
                                temp[x + 1, y - 2] = temp[x, y];
                                temp[x, y] = null;
                                all_next_move.Add(temp);
                            }
                            break;
                        }
                    case "black_shi(Clone)":
                        {
                            if (x - 1 > -1 && y - 1 > 2 && (piece_array[x - 1, y - 1] == null || piece_array[x - 1, y - 1].transform.tag == "RedPiece"))
                            {
                                temp = (GameObject[,])piece_array.Clone();
                                temp[x - 1, y - 1] = temp[x, y];
                                temp[x, y] = null;
                                all_next_move.Add(temp);
                            }
                            if (x - 1 > -1 && y + 1 < 6 && (piece_array[x - 1, y + 1] == null || piece_array[x - 1, y + 1].transform.tag == "RedPiece"))
                            {
                                temp = (GameObject[,])piece_array.Clone();
                                temp[x - 1, y + 1] = temp[x, y];
                                temp[x, y] = null;
                                all_next_move.Add(temp);
                            }
                            if (x + 1 < 3 && y - 1 > 2 && (piece_array[x + 1, y - 1] == null || piece_array[x + 1, y - 1].transform.tag == "RedPiece"))
                            {
                                temp = (GameObject[,])piece_array.Clone();
                                temp[x + 1, y - 1] = temp[x, y];
                                temp[x, y] = null;
                                all_next_move.Add(temp);
                            }
                            if (x + 1 < 3 && y + 1 < 6 && (piece_array[x + 1, y + 1] == null || piece_array[x + 1, y + 1].transform.tag == "RedPiece"))
                            {
                                temp = (GameObject[,])piece_array.Clone();
                                temp[x + 1, y + 1] = temp[x, y];
                                temp[x, y] = null;
                                all_next_move.Add(temp);
                            }
                            break;
                        }
                    case "black_xiang(Clone)":
                        {
                            if (x - 2 > -1 && y - 2 > -1 && piece_array[x - 1, y - 1] == null && (piece_array[x - 2, y - 2] == null || piece_array[x - 2, y - 2].transform.tag == "RedPiece"))
                            {
                                temp = (GameObject[,])piece_array.Clone();
                                temp[x - 2, y - 2] = temp[x, y];
                                temp[x, y] = null;
                                all_next_move.Add(temp);
                            }

                            if (x - 2 > -1 && y + 2 < 9 && piece_array[x - 1, y + 1] == null && (piece_array[x - 2, y + 2] == null || piece_array[x - 2, y + 2].transform.tag == "RedPiece"))
                            {
                                temp = (GameObject[,])piece_array.Clone();
                                temp[x - 2, y + 2] = temp[x, y];
                                temp[x, y] = null;
                                all_next_move.Add(temp);
                            }

                            if (x + 2 < 5 && y - 2 > -1 && piece_array[x + 1, y - 1] == null && (piece_array[x + 2, y - 2] == null || piece_array[x + 2, y - 2].transform.tag == "RedPiece"))
                            {
                                temp = (GameObject[,])piece_array.Clone();
                                temp[x + 2, y - 2] = temp[x, y];
                                temp[x, y] = null;
                                all_next_move.Add(temp);
                            }

                            if (x + 2 < 5 && y + 2 < 9 && piece_array[x + 1, y + 1] == null && (piece_array[x + 2, y + 2] == null || piece_array[x + 2, y + 2].transform.tag == "RedPiece"))
                            {
                                temp = (GameObject[,])piece_array.Clone();
                                temp[x + 2, y + 2] = temp[x, y];
                                temp[x, y] = null;
                                all_next_move.Add(temp);
                            }
                            break;
                        }
                    case "black_pao(Clone)":
                        {
                            bool first_piece_black = false;
                            for (int i = x + 1; i < 10; i++)
                            {
                                if (piece_array[i, y] == null && first_piece_black == false)
                                {
                                    temp = (GameObject[,])piece_array.Clone();
                                    temp[i, y] = temp[x, y];
                                    temp[x, y] = null;
                                    all_next_move.Add(temp);
                                    continue;
                                }
                                else if (piece_array[i, y] == null && first_piece_black == true)
                                {
                                    continue;
                                }
                                else if (first_piece_black == false && piece_array[i, y] != null)
                                {
                                    first_piece_black = true;
                                    continue;
                                }
                                else if (first_piece_black == true && piece_array[i, y] != null && piece_array[i, y].transform.tag == "RedPiece")
                                {
                                    temp = (GameObject[,])piece_array.Clone();
                                    temp[i, y] = temp[x, y];
                                    temp[x, y] = null;
                                    all_next_move.Add(temp);
                                    first_piece_black = false;
                                    break;
                                }
                                else if (first_piece_black == true && piece_array[i, y] != null && piece_array[i, y].transform.tag == "BlackjPiece")
                                {
                                    first_piece_black = false;
                                    break;
                                }
                            }
                            first_piece_black = false;
                            for (int i = x - 1; i >= 0; i--)
                            {
                                if (piece_array[i, y] == null && first_piece_black == false)
                                {
                                    temp = (GameObject[,])piece_array.Clone();
                                    temp[i, y] = temp[x, y];
                                    temp[x, y] = null;
                                    all_next_move.Add(temp);
                                    continue;
                                }
                                else if (piece_array[i, y] == null && first_piece_black == true)
                                {
                                    continue;
                                }
                                else if (first_piece_black == false && piece_array[i, y] != null)
                                {
                                    first_piece_black = true;
                                    continue;
                                }
                                else if (first_piece_black == true && piece_array[i, y] != null && piece_array[i, y].transform.tag == "RedPiece")
                                {
                                    temp = (GameObject[,])piece_array.Clone();
                                    temp[i, y] = temp[x, y];
                                    temp[x, y] = null;
                                    all_next_move.Add(temp);
                                    first_piece_black = false;
                                    break;
                                }
                                else if (first_piece_black == true && piece_array[i, y] != null && piece_array[i, y].transform.tag == "BlackjPiece")
                                {
                                    first_piece_black = false;
                                    break;
                                }
                            }
                            first_piece_black = false;

                            for (int i = y + 1; i < 9; i++)
                            {
                                if (piece_array[x, i] == null && first_piece_black == false)
                                {
                                    temp = (GameObject[,])piece_array.Clone();
                                    temp[x, i] = temp[x, y];
                                    temp[x, y] = null;
                                    all_next_move.Add(temp);
                                    continue;
                                }
                                else if (piece_array[x, i] == null && first_piece_black == true)
                                {
                                    continue;
                                }
                                else if (first_piece_black == false && piece_array[x, i] != null)
                                {
                                    first_piece_black = true;
                                    continue;
                                }
                                else if (first_piece_black == true && piece_array[x, i] != null && piece_array[x, i].transform.tag == "RedPiece")
                                {
                                    temp = (GameObject[,])piece_array.Clone();
                                    temp[x, i] = temp[x, y];
                                    temp[x, y] = null;
                                    all_next_move.Add(temp);
                                    first_piece_black = false;
                                    break;
                                }
                                else if (first_piece_black == true && piece_array[x, i] != null && piece_array[x, i].transform.tag == "BlackjPiece")
                                {
                                    first_piece_black = false;
                                    break;
                                }
                            }
                            first_piece_black = false;

                            for (int i = y - 1; i >= 0; i--)
                            {
                                if (piece_array[x, i] == null && first_piece_black == false)
                                {
                                    temp = (GameObject[,])piece_array.Clone();
                                    temp[x, i] = temp[x, y];
                                    temp[x, y] = null;
                                    all_next_move.Add(temp);
                                    continue;
                                }
                                else if (piece_array[x, i] == null && first_piece_black == true)
                                {
                                    continue;
                                }
                                else if (first_piece_black == false && piece_array[x, i] != null)
                                {
                                    first_piece_black = true;
                                    continue;
                                }
                                else if (first_piece_black == true && piece_array[x, i] != null && piece_array[x, i].transform.tag == "RedPiece")
                                {
                                    temp = (GameObject[,])piece_array.Clone();
                                    temp[x, i] = temp[x, y];
                                    temp[x, y] = null;
                                    all_next_move.Add(temp);
                                    first_piece_black = false;
                                    break;
                                }
                                else if (first_piece_black == true && piece_array[x, i] != null && piece_array[x, i].transform.tag == "BlackjPiece")
                                {
                                    first_piece_black = false;
                                    break;
                                }
                            }
                            break;
                        }
                }
            }
        }
    }


    //获得红方所有的moves
    static public void GetAllRedMoves(GameObject[,] piece_array, List<GameObject[,]> all_next_move)
    {
        foreach (GameObject gameObject in piece_array)
        {
            GameObject[,] temp = new GameObject[10, 9];

            if (gameObject != null)
            {
                int x = gameObject.GetComponent<Piece>().coor_x;
                int y = gameObject.GetComponent<Piece>().coor_y;
                switch (gameObject.transform.name)
                {
                    case "red_bing(Clone)":
                        {
                            if (x > 4 && (piece_array[x - 1, y] == null || piece_array[x - 1, y].transform.tag == "BlackPiece"))
                            {
                                temp = (GameObject[,])piece_array.Clone();
                                temp[x - 1, y] = temp[x, y];
                                temp[x, y] = null;
                                all_next_move.Add(temp);
                            }
                            if (x <= 4 && x + 1 < 10 && (piece_array[x - 1, y] == null || piece_array[x - 1, y].transform.tag == "BlackPiece"))
                            {
                                temp = (GameObject[,])piece_array.Clone();
                                temp[x - 1, y] = temp[x, y];
                                temp[x, y] = null;
                                all_next_move.Add(temp);
                            }
                            if (x <= 4 && y - 1 > -1 && (piece_array[x, y - 1] == null || piece_array[x, y - 1].transform.tag == "BlackPiece"))
                            {
                                temp = (GameObject[,])piece_array.Clone();
                                temp[x, y - 1] = temp[x, y];
                                temp[x, y] = null;
                                all_next_move.Add(temp);
                            }
                            if (x <= 4 && y + 1 < 9 && (piece_array[x, y + 1] == null || piece_array[x, y + 1].transform.tag == "BlackPiece"))
                            {
                                temp = (GameObject[,])piece_array.Clone();
                                temp[x, y + 1] = temp[x, y];
                                temp[x, y] = null;
                                all_next_move.Add(temp);
                            }
                            break;
                        }
                    case "red_che(Clone)":
                        {
                            for (int i = x + 1; i < 10; i++)
                            {
                                if (piece_array[i, y] == null)
                                {
                                    temp = (GameObject[,])piece_array.Clone();
                                    temp[i, y] = temp[x, y];
                                    temp[x, y] = null;
                                    all_next_move.Add(temp);
                                }
                                else if (piece_array[i, y] != null && piece_array[i, y].transform.tag == "BlackPiece")
                                {
                                    temp = (GameObject[,])piece_array.Clone();
                                    temp[i, y] = temp[x, y];
                                    temp[x, y] = null;
                                    all_next_move.Add(temp);
                                    break;
                                }
                                else if (piece_array[i, y] != null && piece_array[i, y].transform.tag == "RedPiece")
                                {
                                    break;
                                }

                            }

                            for (int i = x - 1; i >= 0; i--)
                            {
                                if (piece_array[i, y] == null)
                                {
                                    temp = (GameObject[,])piece_array.Clone();
                                    temp[i, y] = temp[x, y];
                                    temp[x, y] = null;
                                    all_next_move.Add(temp);
                                }
                                else if (piece_array[i, y] != null && piece_array[i, y].transform.tag == "BlackPiece")
                                {
                                    temp = (GameObject[,])piece_array.Clone();
                                    temp[i, y] = temp[x, y];
                                    temp[x, y] = null;
                                    all_next_move.Add(temp);
                                    break;
                                }
                                else if (piece_array[i, y] != null && piece_array[i, y].transform.tag == "RedPiece")
                                {
                                    break;
                                }
                            }

                            for (int i = y + 1; i < 9; i++)
                            {
                                if (piece_array[x, i] == null)
                                {
                                    temp = (GameObject[,])piece_array.Clone();
                                    temp[x, i] = temp[x, y];
                                    temp[x, y] = null;
                                    all_next_move.Add(temp);
                                }
                                else if (piece_array[x, i] != null && piece_array[x, i].transform.tag == "BlackPiece")
                                {
                                    temp = (GameObject[,])piece_array.Clone();
                                    temp[x, i] = temp[x, y];
                                    temp[x, y] = null;
                                    all_next_move.Add(temp);
                                    break;
                                }
                                else if (piece_array[x, i] != null && piece_array[x, i].transform.tag == "RedPiece")
                                {
                                    break;
                                }
                            }

                            for (int i = y - 1; i >= 0; i--)
                            {
                                if (piece_array[x, i] == null)
                                {
                                    temp = (GameObject[,])piece_array.Clone();
                                    temp[x, i] = temp[x, y];
                                    temp[x, y] = null;
                                    all_next_move.Add(temp);
                                }
                                else if (piece_array[x, i] != null && piece_array[x, i].transform.tag == "BlackPiece")
                                {
                                    temp = (GameObject[,])piece_array.Clone();
                                    temp[x, i] = temp[x, y];
                                    temp[x, y] = null;
                                    all_next_move.Add(temp);
                                    break;
                                }
                                else if (piece_array[x, i] != null && piece_array[x, i].transform.tag == "RedPiece")
                                {
                                    break;
                                }
                            }
                            break;
                        }
                    case "red_jiang(Clone)":
                        {
                            if (x - 1 > 6 && (piece_array[x - 1, y] == null || piece_array[x - 1, y].transform.tag == "BlackPiece"))
                            {
                                temp = (GameObject[,])piece_array.Clone();
                                temp[x - 1, y] = temp[x, y];
                                temp[x, y] = null;
                                all_next_move.Add(temp);
                            }

                            if (x + 1 < 10 && (piece_array[x + 1, y] == null || piece_array[x + 1, y].transform.tag == "BlackPiece"))
                            {
                                temp = (GameObject[,])piece_array.Clone();
                                temp[x + 1, y] = temp[x, y];
                                temp[x, y] = null;
                                all_next_move.Add(temp);
                            }

                            if (y - 1 > 2 && (piece_array[x, y - 1] == null || piece_array[x, y - 1].transform.tag == "BlackPiece"))
                            {
                                temp = (GameObject[,])piece_array.Clone();
                                temp[x, y - 1] = temp[x, y];
                                temp[x, y] = null;
                                all_next_move.Add(temp);
                            }
                            if (y + 1 < 6 && (piece_array[x, y + 1] == null || piece_array[x, y + 1].transform.tag == "BlackPiece"))
                            {
                                temp = (GameObject[,])piece_array.Clone();
                                temp[x, y + 1] = temp[x, y];
                                temp[x, y] = null;
                                all_next_move.Add(temp);
                            }
                            break;
                        }
                    case "red_ma(Clone)":
                        {
                            if (x - 2 > -1 && y + 1 < 9 && piece_array[x - 1, y] == null && (piece_array[x - 2, y + 1] == null || piece_array[x - 2, y + 1].transform.tag == "BlackPiece"))
                            {
                                temp = (GameObject[,])piece_array.Clone();
                                temp[x - 2, y + 1] = temp[x, y];
                                temp[x, y] = null;
                                all_next_move.Add(temp);
                            }
                            if (x - 2 > -1 && y - 1 > -1 && piece_array[x - 1, y] == null && (piece_array[x - 2, y - 1] == null || piece_array[x - 2, y - 1].transform.tag == "BlackPiece"))
                            {
                                temp = (GameObject[,])piece_array.Clone();
                                temp[x - 2, y - 1] = temp[x, y];
                                temp[x, y] = null;
                                all_next_move.Add(temp);
                            }
                            if (x + 2 < 10 && y + 1 < 9 && piece_array[x + 1, y] == null && (piece_array[x + 2, y + 1] == null || piece_array[x + 2, y + 1].transform.tag == "BlackPiece"))
                            {
                                temp = (GameObject[,])piece_array.Clone();
                                temp[x + 2, y + 1] = temp[x, y];
                                temp[x, y] = null;
                                all_next_move.Add(temp);
                            }
                            if (x + 2 < 10 && y - 1 > -1 && piece_array[x + 1, y] == null && (piece_array[x + 2, y - 1] == null || piece_array[x + 2, y - 1].transform.tag == "BlackPiece"))
                            {
                                temp = (GameObject[,])piece_array.Clone();
                                temp[x + 2, y - 1] = temp[x, y];
                                temp[x, y] = null;
                                all_next_move.Add(temp);
                            }
                            if (x - 1 > -1 && y + 2 < 9 && piece_array[x, y + 1] == null && (piece_array[x - 1, y + 2] == null || piece_array[x - 1, y + 2].transform.tag == "BlackPiece"))
                            {
                                temp = (GameObject[,])piece_array.Clone();
                                temp[x - 1, y + 2] = temp[x, y];
                                temp[x, y] = null;
                                all_next_move.Add(temp);
                            }

                            if (x + 1 < 10 && y + 2 < 9 && piece_array[x, y + 1] == null && (piece_array[x + 1, y + 2] == null || piece_array[x + 1, y + 2].transform.tag == "BlackPiece"))
                            {
                                temp = (GameObject[,])piece_array.Clone();
                                temp[x + 1, y + 2] = temp[x, y];
                                temp[x, y] = null;
                                all_next_move.Add(temp);
                            }
                            if (x - 1 > -1 && y - 2 > -1 && piece_array[x, y - 1] == null && (piece_array[x - 1, y - 2] == null || piece_array[x - 1, y - 2].transform.tag == "BlackPiece"))
                            {
                                temp = (GameObject[,])piece_array.Clone();
                                temp[x - 1, y - 2] = temp[x, y];
                                temp[x, y] = null;
                                all_next_move.Add(temp);
                            }
                            if (x + 1 < 10 && y - 2 > -1 && piece_array[x, y - 1] == null && (piece_array[x + 1, y - 2] == null || piece_array[x + 1, y - 2].transform.tag == "BlackPiece"))
                            {
                                temp = (GameObject[,])piece_array.Clone();
                                temp[x + 1, y - 2] = temp[x, y];
                                temp[x, y] = null;
                                all_next_move.Add(temp);
                            }
                            break;
                        }
                    case "red_shi(Clone)":
                        {
                            if (x - 1 > 6 && y - 1 > 2 && (piece_array[x - 1, y - 1] == null || piece_array[x - 1, y - 1].transform.tag == "BlackPiece"))
                            {
                                temp = (GameObject[,])piece_array.Clone();
                                temp[x - 1, y - 1] = temp[x, y];
                                temp[x, y] = null;
                                all_next_move.Add(temp);
                            }
                            if (x - 1 > 6 && y + 1 < 6 && (piece_array[x - 1, y + 1] == null || piece_array[x - 1, y + 1].transform.tag == "BlackPiece"))
                            {
                                temp = (GameObject[,])piece_array.Clone();
                                temp[x - 1, y + 1] = temp[x, y];
                                temp[x, y] = null;
                                all_next_move.Add(temp);
                            }
                            if (x + 1 < 10 && y - 1 > 2 && (piece_array[x + 1, y - 1] == null || piece_array[x + 1, y - 1].transform.tag == "BlackPiece"))
                            {
                                temp = (GameObject[,])piece_array.Clone();
                                temp[x + 1, y - 1] = temp[x, y];
                                temp[x, y] = null;
                                all_next_move.Add(temp);
                            }
                            if (x + 1 < 10 && y + 1 < 6 && (piece_array[x + 1, y + 1] == null || piece_array[x + 1, y + 1].transform.tag == "BlackPiece"))
                            {
                                temp = (GameObject[,])piece_array.Clone();
                                temp[x + 1, y + 1] = temp[x, y];
                                temp[x, y] = null;
                                all_next_move.Add(temp);
                            }
                            break;
                        }
                    case "red_xiang(Clone)":
                        {
                            if (x - 2 > 4 && y - 2 > -1 && piece_array[x - 1, y - 1] == null && (piece_array[x - 2, y - 2] == null || piece_array[x - 2, y - 2].transform.tag == "RedPiece"))
                            {
                                temp = (GameObject[,])piece_array.Clone();
                                temp[x - 2, y - 2] = temp[x, y];
                                temp[x, y] = null;
                                all_next_move.Add(temp);
                            }

                            if (x - 2 > 4 && y + 2 < 9 && piece_array[x - 1, y + 1] == null && (piece_array[x - 2, y + 2] == null || piece_array[x - 2, y + 2].transform.tag == "RedPiece"))
                            {
                                temp = (GameObject[,])piece_array.Clone();
                                temp[x - 2, y + 2] = temp[x, y];
                                temp[x, y] = null;
                                all_next_move.Add(temp);
                            }

                            if (x + 2 < 10 && y - 2 > -1 && piece_array[x + 1, y - 1] == null && (piece_array[x + 2, y - 2] == null || piece_array[x + 2, y - 2].transform.tag == "RedPiece"))
                            {
                                temp = (GameObject[,])piece_array.Clone();
                                temp[x + 2, y - 2] = temp[x, y];
                                temp[x, y] = null;
                                all_next_move.Add(temp);
                            }

                            if (x + 2 < 10 && y + 2 < 9 && piece_array[x + 1, y + 1] == null && (piece_array[x + 2, y + 2] == null || piece_array[x + 2, y + 2].transform.tag == "RedPiece"))
                            {
                                temp = (GameObject[,])piece_array.Clone();
                                temp[x + 2, y + 2] = temp[x, y];
                                temp[x, y] = null;
                                all_next_move.Add(temp);
                            }
                            break;
                        }
                    case "red_pao(Clone)":
                        {
                            bool first_piece_black = false;
                            for (int i = x + 1; i < 10; i++)
                            {
                                if (piece_array[i, y] == null && first_piece_black == false)
                                {
                                    temp = (GameObject[,])piece_array.Clone();
                                    temp[i, y] = temp[x, y];
                                    temp[x, y] = null;
                                    all_next_move.Add(temp);
                                    continue;
                                }
                                else if (piece_array[i, y] == null && first_piece_black == true)
                                {
                                    continue;
                                }
                                else if (first_piece_black == false && piece_array[i, y] != null)
                                {
                                    first_piece_black = true;
                                    continue;
                                }
                                else if (first_piece_black == true && piece_array[i, y] != null && piece_array[i, y].transform.tag == "BlackPiece")
                                {
                                    temp = (GameObject[,])piece_array.Clone();
                                    temp[i, y] = temp[x, y];
                                    temp[x, y] = null;
                                    all_next_move.Add(temp);
                                    first_piece_black = false;
                                    break;
                                }
                                else if (first_piece_black == true && piece_array[i, y] != null && piece_array[i, y].transform.tag == "RedPiece")
                                {
                                    first_piece_black = false;
                                    break;
                                }

                            }
                            first_piece_black = false;

                            for (int i = x - 1; i >= 0; i--)
                            {
                                if (piece_array[i, y] == null && first_piece_black == false)
                                {
                                    temp = (GameObject[,])piece_array.Clone();
                                    temp[i, y] = temp[x, y];
                                    temp[x, y] = null;
                                    all_next_move.Add(temp);
                                    continue;
                                }
                                else if (piece_array[i, y] == null && first_piece_black == true)
                                {
                                    continue;
                                }
                                else if (first_piece_black == false && piece_array[i, y] != null)
                                {
                                    first_piece_black = true;
                                    continue;
                                }
                                else if (first_piece_black == true && piece_array[i, y] != null && piece_array[i, y].transform.tag == "BlackPiece")
                                {
                                    temp = (GameObject[,])piece_array.Clone();
                                    temp[i, y] = temp[x, y];
                                    temp[x, y] = null;
                                    all_next_move.Add(temp);
                                    first_piece_black = false;
                                    break;
                                }
                                else if (first_piece_black == true && piece_array[i, y] != null && piece_array[i, y].transform.tag == "RedPiece")
                                {
                                    first_piece_black = false;
                                    break;
                                }
                            }
                            first_piece_black = false;

                            for (int i = y + 1; i < 9; i++)
                            {
                                if (piece_array[x, i] == null && first_piece_black == false)
                                {
                                    temp = (GameObject[,])piece_array.Clone();
                                    temp[x, i] = temp[x, y];
                                    temp[x, y] = null;
                                    all_next_move.Add(temp);
                                    continue;
                                }
                                else if (piece_array[x, i] == null && first_piece_black == true)
                                {
                                    continue;
                                }
                                else if (first_piece_black == false && piece_array[x, i] != null)
                                {
                                    first_piece_black = true;
                                    continue;
                                }
                                else if (first_piece_black == true && piece_array[x, i] != null && piece_array[x, i].transform.tag == "BlackPiece")
                                {
                                    temp = (GameObject[,])piece_array.Clone();
                                    temp[x, i] = temp[x, y];
                                    temp[x, y] = null;
                                    all_next_move.Add(temp);
                                    first_piece_black = false;
                                    break;
                                }
                                else if (first_piece_black == true && piece_array[x, i] != null && piece_array[x, i].transform.tag == "RedPiece")
                                {
                                    first_piece_black = false;
                                    break;
                                }
                            }
                            first_piece_black = false;

                            for (int i = y - 1; i >= 0; i--)
                            {
                                if (piece_array[x, i] == null && first_piece_black == false)
                                {
                                    temp = (GameObject[,])piece_array.Clone();
                                    temp[x, i] = temp[x, y];
                                    temp[x, y] = null;
                                    all_next_move.Add(temp);
                                    continue;
                                }
                                else if (piece_array[x, i] == null && first_piece_black == true)
                                {
                                    continue;
                                }
                                else if (first_piece_black == false && piece_array[x, i] != null)
                                {
                                    first_piece_black = true;
                                    continue;
                                }
                                else if (first_piece_black == true && piece_array[x, i] != null && piece_array[x, i].transform.tag == "BlackPiece")
                                {
                                    temp = (GameObject[,])piece_array.Clone();
                                    temp[x, i] = temp[x, y];
                                    temp[x, y] = null;
                                    all_next_move.Add(temp);
                                    first_piece_black = false;
                                    break;
                                }
                                else if (first_piece_black == true && piece_array[x, i] != null && piece_array[x, i].transform.tag == "RedPiece")
                                {
                                    first_piece_black = false;
                                    break;
                                }
                            }
                            break;
                        }
                }
            }
        }
    }

    //获得棋盘后更新每个物体Piece中的信息
    static public void UpdatePiece()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (PieceManager.piece_array[i, j] != null)
                {
                    PieceManager.piece_array[i, j].GetComponent<Piece>().coor_x = i;
                    PieceManager.piece_array[i, j].GetComponent<Piece>().coor_y = j;
                    PieceManager.piece_array[i, j].GetComponent<Piece>().is_destory = true;
                }
            }
        }
    }
}