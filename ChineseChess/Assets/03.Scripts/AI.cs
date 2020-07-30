using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AI : MonoBehaviour {

	// Use this for initialization
    static private List<GameObject[,]> all_next_move = new List<GameObject[,]>();
    static public GameObject[,] best_move = new GameObject[10, 9];
	void Start () {
        ABMaxMin(PieceManager.piece_array, 3, 3, double.NegativeInfinity, double.PositiveInfinity);

    }
	
	// Update is called once per frame
	void Update () {
        if(PieceCtrl.exchange == 1)
        {

        }
	}

    static private double ABMaxMin(GameObject [,] piece_array, int depth, int maxdepth, double alpha, double beta)
    {
        if (GameOver(piece_array) == false || depth == 0)
            return PieceManager.PiecePower();

        PieceManager.GetAllMove(piece_array, all_next_move);

        GameObject[,] move = new GameObject[10, 9];
        foreach(GameObject[,] gameObjects in all_next_move)
        {
            double value = -ABMaxMin(gameObjects, depth - 1, maxdepth, -beta, -alpha);
            if ( depth == maxdepth && value > alpha)
            {
                best_move = gameObjects; //若此时时最顶层，则记录最佳走法，贪心策略
            }
            if (value >= alpha)
            {
                alpha = value;
            }
            if (alpha >= beta)
                break;
        }
        return alpha;

    }

    //判断当前数组的棋局是否结束
    static private bool GameOver(GameObject [,] piece_array)
    {
        bool black_jiang_exist = false;
        bool red_jiang_exist = false;
        foreach (GameObject gameObject in piece_array)
        {
            if(gameObject.transform.name == "black_jiang(Clone)")
            {
                black_jiang_exist = true;
            }
            else if(gameObject.transform.name == "red_jiang(Clone)")
            {
                red_jiang_exist = true;
            }
        }
        return !(black_jiang_exist && red_jiang_exist);
    }
    //int AlphaBeta()
    //{
    //    //return b;
    //}
}
