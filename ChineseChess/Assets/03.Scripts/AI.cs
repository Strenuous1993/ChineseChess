using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AI : MonoBehaviour {

	// Use this for initialization
    public Piece[,] status = new Piece[10, 9];
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(PieceCtrl.exchange == 1)
        {

        }
	}

    //int AlphaBeta()
    //{
    //    //return b;
    //}
}
