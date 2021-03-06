﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{

    public int coor_x;
    public int coor_y;
    public bool moved = false;
    public string piece_id;
    public int piece_power = 0;
    public int piece_attack_power = 0;
    public bool is_destory = true;
    // Use this for initialization

    private void Awake()
    {
        Nameing();
        PiecePowerSet();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    //去掉名字中的数字和空格，配合PieceCtrl中的switch
    public void Nameing()
    {

        //用来区别每个棋子
        piece_id = gameObject.transform.name;
        gameObject.transform.name = gameObject.transform.name.Replace(" 1", "");
        gameObject.transform.name = gameObject.transform.name.Replace(" 2", "");
        gameObject.transform.name = gameObject.transform.name.Replace(" 3", "");
        gameObject.transform.name = gameObject.transform.name.Replace(" 4", "");
        gameObject.transform.name = gameObject.transform.name.Replace(" 5", "");

    }



    public void PiecePowerSet()
    {
        switch (gameObject.transform.name)
        {
            //红方棋子棋力
            case "red_bing(Clone)":
                piece_power = -100;
                break;
            case "red_che(Clone)":
                piece_power = -900;
                break;
            case "red_jiang(Clone)":
                piece_power = -1000;
                break;
            case "red_ma(Clone)":
                piece_power = -400;
                break;
            case "red_shi(Clone)":
                piece_power = -250;
                break;
            case "red_xiang(Clone)":
                piece_power = -200;
                break;
            case "red_pao(Clone)":
                piece_power = -450;
                break;


            //黑方棋子棋力
            case "black_bing(Clone)":
                piece_power = 100;
                break;
            case "black_che(Clone)":
                piece_power = 900;
                break;
            case "black_jiang(Clone)":
                piece_power = 1000;
                break;
            case "black_ma(Clone)":
                piece_power = 400;
                break;
            case "black_shi(Clone)":
                piece_power = 250;
                break;
            case "black_xiang(Clone)":
                piece_power = 200;
                break;
            case "black_pao(Clone)":
                piece_power = 450;
                break;
            default:
                break;
        }
    }
    //设置棋子攻击能力 攻击能力等于棋子可攻击的对方棋子的picec_power
    public void PieceAttackPowerSet()
    {
        piece_attack_power = 0;
        switch (gameObject.transform.name)
        {
            //红方棋子棋力
            case "red_bing(Clone)":
                {
                    if (coor_x > 4 && PieceManager.piece_array[coor_x - 1, coor_y] != null && PieceManager.piece_array[coor_x - 1, coor_y].transform.tag == "BlackPiece")
                    {
                        piece_attack_power += Mathf.Abs(PieceManager.piece_array[coor_x - 1, coor_y].GetComponent<Piece>().piece_power);
                    }

                    if (coor_x <= 4 && coor_x  - 1 > 0&&PieceManager.piece_array[coor_x - 1, coor_y] != null && PieceManager.piece_array[coor_x - 1, coor_y].transform.tag == "BlackPiece")
                    {
                        piece_attack_power += Mathf.Abs(PieceManager.piece_array[coor_x - 1, coor_y].GetComponent<Piece>().piece_power);
                    }

                    if (coor_x <= 4 && coor_y - 1 > -1 && PieceManager.piece_array[coor_x, coor_y - 1] != null && PieceManager.piece_array[coor_x, coor_y - 1].transform.tag == "BlackPiece")
                    {
                        piece_attack_power += Mathf.Abs(PieceManager.piece_array[coor_x, coor_y - 1].GetComponent<Piece>().piece_power);
                    }

                    if (coor_x <= 4 && coor_y + 1 < 9 && PieceManager.piece_array[coor_x, coor_y + 1] != null && PieceManager.piece_array[coor_x, coor_y + 1].transform.tag == "BlackPiece")
                    {
                        piece_attack_power += Mathf.Abs(PieceManager.piece_array[coor_x, coor_y + 1].GetComponent<Piece>().piece_power);
                    }
                    break;
                }
            case "red_che(Clone)":
                {
                    for (int i = coor_x + 1; i < 10; i++)
                    {
                        if (PieceManager.piece_array[i, coor_y] != null && PieceManager.piece_array[i, coor_y].transform.tag == "RedPiece")
                            break;
                        else if (PieceManager.piece_array[i, coor_y] != null && PieceManager.piece_array[i, coor_y].transform.tag == "BlackPiece")
                            piece_attack_power += Mathf.Abs(PieceManager.piece_array[i, coor_y].GetComponent<Piece>().piece_power);
                    }

                    for (int i = coor_x - 1; i >= 0; i--)
                    {
                        if (PieceManager.piece_array[i, coor_y] != null && PieceManager.piece_array[i, coor_y].transform.tag == "RedPiece")
                            break;
                        else if (PieceManager.piece_array[i, coor_y] != null && PieceManager.piece_array[i, coor_y].transform.tag == "BlackPiece")
                            piece_attack_power += Mathf.Abs(PieceManager.piece_array[i, coor_y].GetComponent<Piece>().piece_power);
                    }

                    for (int i = coor_y + 1; i < 9; i++)
                    {
                        if (PieceManager.piece_array[coor_x, i] != null && PieceManager.piece_array[coor_x, i].transform.tag == "RedPiece")
                            break;
                        else if (PieceManager.piece_array[coor_y, i] != null && PieceManager.piece_array[coor_y, i].transform.tag == "BlackPiece")
                            piece_attack_power += Mathf.Abs(PieceManager.piece_array[coor_y, i].GetComponent<Piece>().piece_power);
                    }

                    for (int i = coor_y - 1; i >= 0; i--)
                    {
                        if (PieceManager.piece_array[coor_x, i] != null && PieceManager.piece_array[coor_x, i].transform.tag == "RedPiece")
                            break;
                        else if (PieceManager.piece_array[coor_y, i] != null && PieceManager.piece_array[coor_y, i].transform.tag == "BlackPiece")
                            piece_attack_power += Mathf.Abs(PieceManager.piece_array[coor_y, i].GetComponent<Piece>().piece_power);
                    }
                    break;
                }
            case "red_jiang(Clone)":
                {
                    if (coor_x - 1 > 6 && PieceManager.piece_array[coor_x - 1, coor_y] != null && PieceManager.piece_array[coor_x - 1, coor_y].transform.tag == "BlackPiece")
                        piece_attack_power += Mathf.Abs(PieceManager.piece_array[coor_x - 1, coor_y].GetComponent<Piece>().piece_power);
                    if (coor_x + 1 < 10 && PieceManager.piece_array[coor_x + 1, coor_y] != null && PieceManager.piece_array[coor_x + 1, coor_y].transform.tag == "BlackPiece")
                        piece_attack_power += Mathf.Abs(PieceManager.piece_array[coor_x + 1, coor_y].GetComponent<Piece>().piece_power);
                    if (coor_y - 1 > 2 && PieceManager.piece_array[coor_x, coor_y - 1] != null && PieceManager.piece_array[coor_x, coor_y - 1].transform.tag == "BlackPiece")
                        piece_attack_power += Mathf.Abs(PieceManager.piece_array[coor_x, coor_y - 1].GetComponent<Piece>().piece_power);
                    if (coor_y + 1 < 6 && PieceManager.piece_array[coor_x, coor_y + 1] != null && PieceManager.piece_array[coor_x, coor_y + 1].transform.tag == "BlackPiece")
                        piece_attack_power += Mathf.Abs(PieceManager.piece_array[coor_x, coor_y + 1].GetComponent<Piece>().piece_power);
                    break;
                }
            case "red_ma(Clone)":
                {
                    if (coor_x - 2 > -1 && coor_y + 1 < 9 && PieceManager.piece_array[coor_x - 1, coor_y] == null && PieceManager.piece_array[coor_x - 2, coor_y + 1] != null && PieceManager.piece_array[coor_x - 2, coor_y + 1].transform.tag == "BlackPiece")
                        piece_attack_power += Mathf.Abs(PieceManager.piece_array[coor_x - 2, coor_y + 1].GetComponent<Piece>().piece_power);
                    if (coor_x - 2 > -1 && coor_y - 1 > -1 && PieceManager.piece_array[coor_x - 1, coor_y] == null && PieceManager.piece_array[coor_x - 2, coor_y - 1] != null && PieceManager.piece_array[coor_x - 2, coor_y - 1].transform.tag == "BlackPiece")
                        piece_attack_power += Mathf.Abs(PieceManager.piece_array[coor_x - 2, coor_y - 1].GetComponent<Piece>().piece_power);
                    if (coor_x + 2 < 10 && coor_y + 1 < 9 && PieceManager.piece_array[coor_x + 1, coor_y] == null && PieceManager.piece_array[coor_x + 2, coor_y + 1] != null && PieceManager.piece_array[coor_x + 2, coor_y + 1].transform.tag == "BlackPiece")
                        piece_attack_power += Mathf.Abs(PieceManager.piece_array[coor_x + 2, coor_y + 1].GetComponent<Piece>().piece_power);
                    if (coor_x + 2 < 10 && coor_y - 1 > -1 && PieceManager.piece_array[coor_x + 1, coor_y] == null && PieceManager.piece_array[coor_x + 2, coor_y - 1] != null && PieceManager.piece_array[coor_x + 2, coor_y - 1].transform.tag == "BlackPiece")
                        piece_attack_power += Mathf.Abs(PieceManager.piece_array[coor_x + 2, coor_y + 1].GetComponent<Piece>().piece_power);
                    if (coor_x - 1 > -1 && coor_y + 2 < 9 && PieceManager.piece_array[coor_x, coor_y + 1] == null && PieceManager.piece_array[coor_x - 1, coor_y + 2] != null && PieceManager.piece_array[coor_x - 1, coor_y + 2].transform.tag == "BlackPiece")
                        piece_attack_power += Mathf.Abs(PieceManager.piece_array[coor_x - 1, coor_y + 2].GetComponent<Piece>().piece_power);
                    if (coor_x + 1 < 10 && coor_y + 2 < 9 && PieceManager.piece_array[coor_x, coor_y + 1] == null && PieceManager.piece_array[coor_x + 1, coor_y + 2] != null && PieceManager.piece_array[coor_x + 1, coor_y + 2].transform.tag == "BlackPiece")
                        piece_attack_power += Mathf.Abs(PieceManager.piece_array[coor_x + 1, coor_y + 2].GetComponent<Piece>().piece_power);
                    if (coor_x - 1 > -1 && coor_y - 2 > -1 && PieceManager.piece_array[coor_x, coor_y - 1] == null && PieceManager.piece_array[coor_x - 1, coor_y - 2] != null && PieceManager.piece_array[coor_x - 1, coor_y - 2].transform.tag == "BlackPiece")
                        piece_attack_power += Mathf.Abs(PieceManager.piece_array[coor_x - 1, coor_y - 2].GetComponent<Piece>().piece_power);
                    if (coor_x + 1 < 10 && coor_y - 2 > -1 && PieceManager.piece_array[coor_x, coor_y - 1] == null && PieceManager.piece_array[coor_x + 1, coor_y - 2] != null && PieceManager.piece_array[coor_x + 1, coor_y - 2].transform.tag == "BlackPiece")
                        piece_attack_power += Mathf.Abs(PieceManager.piece_array[coor_x + 1, coor_y - 2].GetComponent<Piece>().piece_power);
                    break;
                }
            case "red_shi(Clone)":
                {
                    if (coor_x - 1 > 6 && coor_y - 1 > 2 && PieceManager.piece_array[coor_x - 1, coor_y - 1] != null && PieceManager.piece_array[coor_x - 1, coor_y - 1].transform.tag == "BlackPiece")
                    {
                        piece_attack_power += Mathf.Abs(PieceManager.piece_array[coor_x - 1, coor_y - 1].GetComponent<Piece>().piece_power);
                    }
                    if (coor_x - 1 > 6 && coor_y + 1 < 6 && PieceManager.piece_array[coor_x - 1, coor_y + 1] != null && PieceManager.piece_array[coor_x - 1, coor_y + 1].transform.tag == "BlackPiece")
                    {
                        piece_attack_power += Mathf.Abs(PieceManager.piece_array[coor_x - 1, coor_y + 1].GetComponent<Piece>().piece_power);
                    }
                    if (coor_x + 1 < 10 && coor_y - 1 > 2 && PieceManager.piece_array[coor_x + 1, coor_y - 1] != null && PieceManager.piece_array[coor_x + 1, coor_y - 1].transform.tag == "BlackPiece")
                    {
                        piece_attack_power += Mathf.Abs(PieceManager.piece_array[coor_x + 1, coor_y - 1].GetComponent<Piece>().piece_power);
                    }
                    if (coor_x + 1 < 10 && coor_y + 1 < 6 && PieceManager.piece_array[coor_x + 1, coor_y + 1] != null && PieceManager.piece_array[coor_x + 1, coor_y + 1].transform.tag == "BlackPiece")
                    {
                        piece_attack_power += Mathf.Abs(PieceManager.piece_array[coor_x + 1, coor_y + 1].GetComponent<Piece>().piece_power);
                    }
                    break;
                }
            case "red_xiang(Clone)":
                {
                    if (coor_x - 2 > 4 && coor_y - 2 > -1 && PieceManager.piece_array[coor_x - 1, coor_y - 1] == null && PieceManager.piece_array[coor_x - 2, coor_y - 2] != null && PieceManager.piece_array[coor_x - 2, coor_y - 2].transform.tag == "BlackPiece")
                    {
                        piece_attack_power += Mathf.Abs(PieceManager.piece_array[coor_x - 2, coor_y - 2].GetComponent<Piece>().piece_power);
                    }

                    if (coor_x - 2 > 4 && coor_y + 2 < 9 && PieceManager.piece_array[coor_x - 1, coor_y + 1] == null && PieceManager.piece_array[coor_x - 2, coor_y + 2] != null && PieceManager.piece_array[coor_x - 2, coor_y + 2].transform.tag == "BlackPiece")
                    {
                        piece_attack_power += Mathf.Abs(PieceManager.piece_array[coor_x - 2, coor_y + 2].GetComponent<Piece>().piece_power);
                    }

                    if (coor_x + 2 < 10 && coor_y - 2 > -1 && PieceManager.piece_array[coor_x + 1, coor_y - 1] == null && PieceManager.piece_array[coor_x + 2, coor_y - 2] != null && PieceManager.piece_array[coor_x + 2, coor_y - 2].transform.tag == "BlackPiece")
                    {
                        piece_attack_power += Mathf.Abs(PieceManager.piece_array[coor_x + 2, coor_y - 2].GetComponent<Piece>().piece_power);
                    }

                    if (coor_x + 2 < 10 && coor_y + 2 < 9 && PieceManager.piece_array[coor_x + 1, coor_y + 1] == null && PieceManager.piece_array[coor_x + 2, coor_y + 2] != null && PieceManager.piece_array[coor_x + 2, coor_y + 2].transform.tag == "BlackPiece")
                    {
                        piece_attack_power += Mathf.Abs(PieceManager.piece_array[coor_x + 2, coor_y + 2].GetComponent<Piece>().piece_power);
                    }
                    break;
                }
            case "red_pao(Clone)":
                {
                    bool first_piece = false;
                    for (int i = coor_x + 1; i < 10; i++)
                    {
                        if (PieceManager.piece_array[i, coor_y] != null && first_piece == false)
                        {
                            first_piece = true;
                            continue;
                        }
                        if (first_piece == true && PieceManager.piece_array[i, coor_y] != null && PieceManager.piece_array[i, coor_y].transform.tag == "BlackPiece")
                        {
                            piece_attack_power += Mathf.Abs(PieceManager.piece_array[i, coor_y].GetComponent<Piece>().piece_power);
                            first_piece = false;
                            break;
                        }
                    }
                    first_piece = false;
                    for (int i = coor_x - 1; i >= 0; i--)
                    {
                        if (PieceManager.piece_array[i, coor_y] != null && first_piece == false)
                        {
                            first_piece = true;
                            continue;
                        }
                        if (first_piece == true && PieceManager.piece_array[i, coor_y] != null && PieceManager.piece_array[i, coor_y].transform.tag == "BlackPiece")
                        {
                            piece_attack_power += Mathf.Abs(PieceManager.piece_array[i, coor_y].GetComponent<Piece>().piece_power);
                            first_piece = false;
                            break;
                        }
                    }
                    first_piece = false;
                    for (int i = coor_y + 1; i < 9; i++)
                    {
                        if (PieceManager.piece_array[coor_x, i] != null && first_piece == false)
                        {
                            first_piece = true;
                            continue;
                        }
                        if (first_piece == true && PieceManager.piece_array[coor_x, i] != null && PieceManager.piece_array[coor_x, i].transform.tag == "BlackPiece")
                        {
                            piece_attack_power += Mathf.Abs(PieceManager.piece_array[coor_x, i].GetComponent<Piece>().piece_power);
                            first_piece = false;
                            break;
                        }
                    }
                    first_piece = false;
                    for (int i = coor_y - 1; i >= 0; i--)
                    {
                        if (PieceManager.piece_array[coor_x, i] != null && first_piece == false)
                        {
                            first_piece = true;
                            continue;
                        }
                        if (first_piece == true && PieceManager.piece_array[coor_x, i] != null && PieceManager.piece_array[coor_x, i].transform.tag == "BlackPiece")
                        {
                            piece_attack_power += Mathf.Abs(PieceManager.piece_array[coor_x, i].GetComponent<Piece>().piece_power);
                            first_piece = false;
                            break;
                        }
                    }
                    break;
                }





            //黑方棋子棋力
            case "black_bing(Clone)":
                {
                    if (coor_x < 5 && PieceManager.piece_array[coor_x + 1, coor_y] != null && PieceManager.piece_array[coor_x + 1, coor_y].transform.tag == "RedPiece")
                    {
                        piece_attack_power += Mathf.Abs(PieceManager.piece_array[coor_x + 1, coor_y].GetComponent<Piece>().piece_power);
                    }

                    if (coor_x >= 5 && coor_x + 1 < 10 &&PieceManager.piece_array[coor_x + 1, coor_y] != null && PieceManager.piece_array[coor_x + 1, coor_y].transform.tag == "RedPiece")
                    {
                        piece_attack_power += Mathf.Abs(PieceManager.piece_array[coor_x + 1, coor_y].GetComponent<Piece>().piece_power);
                    }

                    if (coor_x >= 5 &&  coor_y - 1 > -1 && PieceManager.piece_array[coor_x, coor_y - 1] != null && PieceManager.piece_array[coor_x, coor_y - 1].transform.tag == "RedPiece")
                    {
                        piece_attack_power += Mathf.Abs(PieceManager.piece_array[coor_x, coor_y - 1].GetComponent<Piece>().piece_power);
                    }

                    if (coor_x >= 5 && coor_y + 1 < 9 && PieceManager.piece_array[coor_x, coor_y + 1] != null && PieceManager.piece_array[coor_x, coor_y + 1].transform.tag == "RedPiece")
                    {
                        piece_attack_power += Mathf.Abs(PieceManager.piece_array[coor_x, coor_y + 1].GetComponent<Piece>().piece_power);
                    }
                    break;
                }
            case "black_che(Clone)":
                {
                    for (int i = coor_x + 1; i < 10; i++)
                    {
                        if (PieceManager.piece_array[i, coor_y] != null && PieceManager.piece_array[i, coor_y].transform.tag == "BlackPiece")
                            break;
                        else if (PieceManager.piece_array[i, coor_y] != null && PieceManager.piece_array[i, coor_y].transform.tag == "RedPiece")
                            piece_attack_power += Mathf.Abs(PieceManager.piece_array[i, coor_y].GetComponent<Piece>().piece_power);
                    }

                    for (int i = coor_x - 1; i >= 0; i--)
                    {
                        if (PieceManager.piece_array[i, coor_y] != null && PieceManager.piece_array[i, coor_y].transform.tag == "BlackPiece")
                            break;
                        else if (PieceManager.piece_array[i, coor_y] != null && PieceManager.piece_array[i, coor_y].transform.tag == "RedPiece")
                            piece_attack_power += Mathf.Abs(PieceManager.piece_array[i, coor_y].GetComponent<Piece>().piece_power);
                    }

                    for (int i = coor_y + 1; i < 9; i++)
                    {
                        if (PieceManager.piece_array[coor_x, i] != null && PieceManager.piece_array[coor_x, i].transform.tag == "BlackPiece")
                            break;
                        else if (PieceManager.piece_array[coor_y, i] != null && PieceManager.piece_array[coor_y, i].transform.tag == "RedPiece")
                            piece_attack_power += Mathf.Abs(PieceManager.piece_array[coor_y, i].GetComponent<Piece>().piece_power);
                    }

                    for (int i = coor_y - 1; i >= 0; i--)
                    {
                        if (PieceManager.piece_array[coor_x, i] != null && PieceManager.piece_array[coor_x, i].transform.tag == "BlackPiece")
                            break;
                        else if (PieceManager.piece_array[coor_y, i] != null && PieceManager.piece_array[coor_y, i].transform.tag == "RedPiece")
                            piece_attack_power += Mathf.Abs(PieceManager.piece_array[coor_y, i].GetComponent<Piece>().piece_power);
                    }
                    break;
                }
            case "black_jiang(Clone)":
                {
                    if (coor_x - 1 > -1 && PieceManager.piece_array[coor_x - 1, coor_y] != null && PieceManager.piece_array[coor_x - 1, coor_y].transform.tag == "RedPiece")
                        piece_attack_power += Mathf.Abs(PieceManager.piece_array[coor_x - 1, coor_y].GetComponent<Piece>().piece_power);
                    if (coor_x + 1 < 3 && PieceManager.piece_array[coor_x + 1, coor_y] != null && PieceManager.piece_array[coor_x + 1, coor_y].transform.tag == "RedPiece")
                        piece_attack_power += Mathf.Abs(PieceManager.piece_array[coor_x + 1, coor_y].GetComponent<Piece>().piece_power);
                    if (coor_y - 1 > 2 && PieceManager.piece_array[coor_x, coor_y - 1] != null && PieceManager.piece_array[coor_x, coor_y - 1].transform.tag == "RedPiece")
                        piece_attack_power += Mathf.Abs(PieceManager.piece_array[coor_x, coor_y - 1].GetComponent<Piece>().piece_power);
                    if (coor_y + 1 < 6 && PieceManager.piece_array[coor_x, coor_y + 1] != null && PieceManager.piece_array[coor_x, coor_y + 1].transform.tag == "RedPiece")
                        piece_attack_power += Mathf.Abs(PieceManager.piece_array[coor_x, coor_y + 1].GetComponent<Piece>().piece_power);
                    break;
                }
            case "black_ma(Clone)":
                {
                    if (coor_x - 2 > -1 && coor_y + 1 < 9 && PieceManager.piece_array[coor_x - 1, coor_y] == null && PieceManager.piece_array[coor_x - 2, coor_y + 1] != null && PieceManager.piece_array[coor_x - 2, coor_y + 1].transform.tag == "RedPiece")
                        piece_attack_power += Mathf.Abs(PieceManager.piece_array[coor_x - 2, coor_y + 1].GetComponent<Piece>().piece_power);
                    if (coor_x - 2 > -1 && coor_y - 1 > -1 && PieceManager.piece_array[coor_x - 1, coor_y] == null && PieceManager.piece_array[coor_x - 2, coor_y - 1] != null && PieceManager.piece_array[coor_x - 2, coor_y - 1].transform.tag == "RedPiece")
                        piece_attack_power += Mathf.Abs(PieceManager.piece_array[coor_x - 2, coor_y - 1].GetComponent<Piece>().piece_power);
                    if (coor_x + 2 < 10 && coor_y + 1 < 9 && PieceManager.piece_array[coor_x + 1, coor_y] == null && PieceManager.piece_array[coor_x + 2, coor_y + 1] != null && PieceManager.piece_array[coor_x + 2, coor_y + 1].transform.tag == "RedPiece")
                        piece_attack_power += Mathf.Abs(PieceManager.piece_array[coor_x + 2, coor_y + 1].GetComponent<Piece>().piece_power);
                    if (coor_x + 2 < 10 && coor_y - 1 > -1 && PieceManager.piece_array[coor_x + 1, coor_y] == null && PieceManager.piece_array[coor_x + 2, coor_y - 1] != null && PieceManager.piece_array[coor_x + 2, coor_y - 1].transform.tag == "RedPiece")
                        piece_attack_power += Mathf.Abs(PieceManager.piece_array[coor_x + 2, coor_y + 1].GetComponent<Piece>().piece_power);
                    if (coor_x - 1 > -1 && coor_y + 2 < 9 && PieceManager.piece_array[coor_x, coor_y + 1] == null && PieceManager.piece_array[coor_x - 1, coor_y + 2] != null && PieceManager.piece_array[coor_x - 1, coor_y + 2].transform.tag == "RedPiece")
                        piece_attack_power += Mathf.Abs(PieceManager.piece_array[coor_x - 1, coor_y + 2].GetComponent<Piece>().piece_power);
                    if (coor_x + 1 < 10 && coor_y + 2 < 9 && PieceManager.piece_array[coor_x, coor_y + 1] == null && PieceManager.piece_array[coor_x + 1, coor_y + 2] != null && PieceManager.piece_array[coor_x + 1, coor_y + 2].transform.tag == "RedPiece")
                        piece_attack_power += Mathf.Abs(PieceManager.piece_array[coor_x + 1, coor_y + 2].GetComponent<Piece>().piece_power);
                    if (coor_x - 1 > -1 && coor_y - 2 > -1 && PieceManager.piece_array[coor_x, coor_y - 1] == null && PieceManager.piece_array[coor_x - 1, coor_y - 2] != null && PieceManager.piece_array[coor_x - 1, coor_y - 2].transform.tag == "RedPiece")
                        piece_attack_power += Mathf.Abs(PieceManager.piece_array[coor_x - 1, coor_y - 2].GetComponent<Piece>().piece_power);
                    if (coor_x + 1 < 10 && coor_y - 2 > -1 && PieceManager.piece_array[coor_x, coor_y - 1] == null && PieceManager.piece_array[coor_x + 1, coor_y - 2] != null && PieceManager.piece_array[coor_x + 1, coor_y - 2].transform.tag == "RedPiece")
                        piece_attack_power += Mathf.Abs(PieceManager.piece_array[coor_x + 1, coor_y - 2].GetComponent<Piece>().piece_power);
                    break;
                }
            case "blackshi(Clone)":
                {
                    if (coor_x - 1 > -1 && coor_y - 1 > 2 && PieceManager.piece_array[coor_x - 1, coor_y - 1] != null && PieceManager.piece_array[coor_x - 1, coor_y - 1].transform.tag == "RedPiece")
                    {
                        piece_attack_power += Mathf.Abs(PieceManager.piece_array[coor_x - 1, coor_y - 1].GetComponent<Piece>().piece_power);
                    }
                    if (coor_x - 1 > -1 && coor_y + 1 < 6 && PieceManager.piece_array[coor_x - 1, coor_y + 1] != null && PieceManager.piece_array[coor_x - 1, coor_y + 1].transform.tag == "RedPiece")
                    {
                        piece_attack_power += Mathf.Abs(PieceManager.piece_array[coor_x - 1, coor_y + 1].GetComponent<Piece>().piece_power);
                    }
                    if (coor_x + 1 < 3 && coor_y - 1 > 2 && PieceManager.piece_array[coor_x + 1, coor_y - 1] != null && PieceManager.piece_array[coor_x + 1, coor_y - 1].transform.tag == "RedPiece")
                    {
                        piece_attack_power += Mathf.Abs(PieceManager.piece_array[coor_x + 1, coor_y - 1].GetComponent<Piece>().piece_power);
                    }
                    if (coor_x + 1 < 3 && coor_y + 1 < 6 && PieceManager.piece_array[coor_x + 1, coor_y + 1] != null && PieceManager.piece_array[coor_x + 1, coor_y + 1].transform.tag == "RedPiece")
                    {
                        piece_attack_power += Mathf.Abs(PieceManager.piece_array[coor_x + 1, coor_y + 1].GetComponent<Piece>().piece_power);
                    }
                    break;
                }
            case "black_xiang(Clone)":
                {
                    if (coor_x - 2 > -1 && coor_y - 2 > -1 && PieceManager.piece_array[coor_x - 1, coor_y - 1] == null && PieceManager.piece_array[coor_x - 2, coor_y - 2] != null && PieceManager.piece_array[coor_x - 2, coor_y - 2].transform.tag == "RedPiece")
                    {
                        piece_attack_power += Mathf.Abs(PieceManager.piece_array[coor_x - 2, coor_y - 2].GetComponent<Piece>().piece_power);
                    }

                    if (coor_x - 2 > -1 && coor_y + 2 < 9 && PieceManager.piece_array[coor_x - 1, coor_y + 1] == null && PieceManager.piece_array[coor_x - 2, coor_y + 2] != null && PieceManager.piece_array[coor_x - 2, coor_y + 2].transform.tag == "RedPiece")
                    {
                        piece_attack_power += Mathf.Abs(PieceManager.piece_array[coor_x - 2, coor_y + 2].GetComponent<Piece>().piece_power);
                    }

                    if (coor_x + 2 < 5 && coor_y - 2 > -1 && PieceManager.piece_array[coor_x + 1, coor_y - 1] == null && PieceManager.piece_array[coor_x + 2, coor_y - 2] != null && PieceManager.piece_array[coor_x + 2, coor_y - 2].transform.tag == "RedPiece")
                    {
                        piece_attack_power += Mathf.Abs(PieceManager.piece_array[coor_x + 2, coor_y - 2].GetComponent<Piece>().piece_power);
                    }

                    if (coor_x + 2 < 5 && coor_y + 2 < 9 && PieceManager.piece_array[coor_x + 1, coor_y + 1] == null && PieceManager.piece_array[coor_x + 2, coor_y + 2] != null && PieceManager.piece_array[coor_x + 2, coor_y + 2].transform.tag == "RedPiece")
                    {
                        piece_attack_power += Mathf.Abs(PieceManager.piece_array[coor_x + 2, coor_y + 2].GetComponent<Piece>().piece_power);
                    }
                    break;
                }
            case "black_pao(Clone)":
                {
                    bool first_piece_black = false;
                    for (int i = coor_x + 1; i < 10; i++)
                    {
                        if (PieceManager.piece_array[i, coor_y] != null && first_piece_black == false)
                        {
                            first_piece_black = true;
                            continue;
                        }
                        if (first_piece_black == true && PieceManager.piece_array[i, coor_y] != null && PieceManager.piece_array[i, coor_y].transform.tag == "RedPiece")
                        {
                            piece_attack_power += Mathf.Abs(PieceManager.piece_array[i, coor_y].GetComponent<Piece>().piece_power);
                            first_piece_black = false;
                            break;
                        }
                    }
                    first_piece_black = false;
                    for (int i = coor_x - 1; i >= 0; i--)
                    {
                        if (PieceManager.piece_array[i, coor_y] != null && first_piece_black == false)
                        {
                            first_piece_black = true;
                            continue;
                        }
                        if (first_piece_black == true && PieceManager.piece_array[i, coor_y] != null && PieceManager.piece_array[i, coor_y].transform.tag == "RedPiece")
                        {
                            piece_attack_power += Mathf.Abs(PieceManager.piece_array[i, coor_y].GetComponent<Piece>().piece_power);
                            first_piece_black = false;
                            break;
                        }
                    }
                    first_piece_black = false;
                    for (int i = coor_y + 1; i < 9; i++)
                    {
                        if (PieceManager.piece_array[coor_x, i] != null && first_piece_black == false)
                        {
                            first_piece_black = true;
                            continue;
                        }
                        if (first_piece_black == true && PieceManager.piece_array[coor_x, i] != null && PieceManager.piece_array[coor_x, i].transform.tag == "RedPiece")
                        {
                            piece_attack_power += Mathf.Abs(PieceManager.piece_array[coor_x, i].GetComponent<Piece>().piece_power);
                            first_piece_black = false;
                            break;
                        }
                    }
                    first_piece_black = false;
                    for (int i = coor_y - 1; i >= 0; i--)
                    {
                        if (PieceManager.piece_array[coor_x, i] != null && first_piece_black == false)
                        {
                            first_piece_black = true;
                            continue;
                        }
                        if (first_piece_black == true && PieceManager.piece_array[coor_x, i] != null && PieceManager.piece_array[coor_x, i].transform.tag == "RedPiece")
                        {
                            piece_attack_power += Mathf.Abs(PieceManager.piece_array[coor_x, i].GetComponent<Piece>().piece_power);
                            first_piece_black = false;
                            break;
                        }
                    }
                    break;
                }
            default:
                break;

        }
    }
}
