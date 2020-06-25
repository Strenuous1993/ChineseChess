using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;


public class RedCtrl : MonoBehaviour
{

    private int flag;
    private RaycastHit hitInfo;
    private RaycastHit hitInfo1;
    private GameObject gameObj;
    public GameObject chessBoard;
    public int exchange = 0;
    void Update()
    {
        Red_Move();
    }
    //兵的移动逻辑
    void Red_BingMove(GameObject chess, RaycastHit hit)
    {
        //获得移动的目的地和需要移动棋子的局部坐标
        float chess_x = chess.transform.localPosition.x;
        float chess_z = chess.transform.localPosition.z;
        float destination_x = hit.collider.transform.localPosition.x;
        float destination_z = hit.collider.transform.localPosition.z;
        //计算坐标差的绝对值
        float dis_x = Mathf.Abs(chess_x - destination_x);
        float dis_z = Mathf.Abs(chess_z - destination_z);
        //Debug.Log("x坐标" + chess_x);
        //Debug.Log("z坐标" + chess_z);
        if (chess_z > -1.48 && chess_z < 0.39 && chess_x == destination_x && chess_z > destination_z)
        {
            chess.transform.position = new Vector3(chess.transform.position.x, chess.transform.position.y, hit.collider.gameObject.transform.position.z);
        }
        else if (((dis_x < 1.3 && dis_x > 0.1) ^ (dis_z < 1.3 && dis_z > 0.1)) && destination_z < -1.46 && chess_z >= destination_z)
        {
            //物体移动到鼠标获取的坐标物体的位置
            chess.transform.position = hit.collider.gameObject.transform.position;


        }
    }


    //士的移动逻辑
    void Red_ShiMove(GameObject chess, RaycastHit hit)
    {

        float chess_x = chess.transform.localPosition.x;
        float chess_z = chess.transform.localPosition.z;
        float destination_x = hit.collider.transform.localPosition.x;
        float destination_z = hit.collider.transform.localPosition.z;
        float dis_x = Mathf.Abs(chess_x - destination_x);
        float dis_z = Mathf.Abs(chess_z - destination_z);
        //Debug.Log("x坐标" + chess_x);
        //Debug.Log("z坐标" + chess_z);
        if ((dis_x < 1.2 && dis_x > 0.1) && (dis_z < 1.2 && dis_z > 0.1) && (destination_z > 1.32 && destination_z < 3.2) && (destination_x > -0.19 && destination_x < 1.8))
        {
            //物体移动到鼠标获取的坐标物体的位置
            chess.transform.position = hit.collider.gameObject.transform.position;


        }
    }


    //马的移动逻辑
    void Red_MaMove(GameObject chess, RaycastHit hit)
    {

        float chess_x = chess.transform.localPosition.x;
        float chess_z = chess.transform.localPosition.z;
        float destination_x = hit.collider.transform.localPosition.x;
        float destination_z = hit.collider.transform.localPosition.z;
        float dis_x = Mathf.Abs(chess_x - destination_x);
        float dis_z = Mathf.Abs(chess_z - destination_z);
        //Debug.Log("x坐标" + chess_x);
        //Debug.Log("z坐标" + chess_z);

        if (((dis_x < 1.1 && dis_x > 0.9) && (dis_z < 2.0 && dis_z > 1.8)) ^ ((dis_x < 2.3 && dis_x > 1.9) && (dis_z < 1.2 && dis_z > 0.7)))
        {
            if (dis_x > dis_z)
            {
                //计算射线发射的方向
                Vector3 offSet = new Vector3(hit.collider.gameObject.transform.position.x, chess.transform.position.y, chess.transform.position.z) - chess.transform.position;
                //定义射线
                Ray ray1 = new Ray(chess.transform.position, offSet);
                //发射射线
                Physics.Raycast(ray1, out hitInfo1);
                //Debug.DrawRay(chess.transform.position,offSet,Color.red, 10000,false);
                //Debug.Log(hitInfo1.collider.gameObject.name);
                if (hitInfo1.collider.gameObject.tag != "RedPiece" && hitInfo1.collider.gameObject.tag != "BlackPiece")
                {
                    //物体移动到鼠标获取的坐标物体的位置
                    chess.transform.position = hit.collider.gameObject.transform.position;
                }

            }
            if (dis_x < dis_z)
            {
                Vector3 offSet = new Vector3(chess.transform.position.x, chess.transform.position.y, hit.collider.gameObject.transform.position.z) - chess.transform.position;
                Ray ray1 = new Ray(chess.transform.position, offSet);
                Physics.Raycast(ray1, out hitInfo1);
                Debug.DrawRay(chess.transform.position, offSet, Color.red, 10000, false);
                Debug.Log(hitInfo1.collider.gameObject.name);
                if (hitInfo1.collider.gameObject.tag != "RedPiece" && hitInfo1.collider.gameObject.tag != "BlackPiece")
                {
                    //Debug.Log(1);

                    //物体移动到鼠标获取的坐标物体的位置
                    chess.transform.position = hit.collider.gameObject.transform.position;
                }

            }

        }
    }


    //象的移动逻辑
    void Red_XiangMove(GameObject chess, RaycastHit hit)
    {
        //获得移动的目的地和需要移动棋子的局部坐标
        float chess_x = chess.transform.localPosition.x;
        float chess_z = chess.transform.localPosition.z;
        float destination_x = hit.collider.transform.localPosition.x;
        float destination_z = hit.collider.transform.localPosition.z;
        //计算坐标差的绝对值
        float dis_x = Mathf.Abs(chess_x - destination_x);
        float dis_z = Mathf.Abs(chess_z - destination_z);
        //Debug.Log("x坐标" + chess_x);
        //Debug.Log("z坐标" + chess_z);
        if ((dis_x < 2.3 && dis_x > 1.9) && (dis_z < 2.0 && dis_z > 1.8) && destination_z > -1.47)
        {
            Vector3 offSet = hit.collider.gameObject.transform.position - chess.transform.position;
            Ray ray1 = new Ray(chess.transform.position, offSet);
            Physics.Raycast(ray1, out hitInfo1);
            if (hitInfo1.collider.gameObject.tag != "RedPiece" && hitInfo1.collider.gameObject.tag != "BlackPiece")
            {
                //物体移动到鼠标获取的坐标物体的位置
                chess.transform.position = hit.collider.gameObject.transform.position;

            }
        }
    }


    //车的移动逻辑
    void Red_CheMove(GameObject chess, RaycastHit hit)
    {
        //获得移动的目的地和需要移动棋子的局部坐标
        float chess_x = chess.transform.localPosition.x;
        float chess_z = chess.transform.localPosition.z;
        float destination_x = hit.collider.transform.localPosition.x;
        float destination_z = hit.collider.transform.localPosition.z;
        float dis_x = Mathf.Abs(chess_x - destination_x);
        float dis_z = Mathf.Abs(chess_z - destination_z);
        //Debug.Log("x坐标" + chess_x);
        //Debug.Log("z坐标" + chess_z);
        Debug.Log("第一个条件："+(chess_x == destination_x));
        Debug.Log("第二个条件："+(chess_x != destination_x && chess_z == destination_z));
        Debug.Log("c_x="+chess_x);
        Debug.Log("d_x="+destination_x);
        Debug.Log("c_z=" + chess_z);
        Debug.Log("d_z=" + destination_z);
        if (( dis_x<0.1&& chess_z != destination_z) ^ (chess_x != destination_x && dis_z<0.1))
        {
            chess.transform.position = hit.collider.gameObject.transform.position;

        }
    }


    //将的移动逻辑
    void Red_JiangMove(GameObject chess, RaycastHit hit)
    {
        //获得移动的目的地和需要移动棋子的局部坐标
        float chess_x = chess.transform.localPosition.x;
        float chess_z = chess.transform.localPosition.z;
        float destination_x = hit.collider.transform.localPosition.x;
        float destination_z = hit.collider.transform.localPosition.z;
        //计算坐标差的绝对值
        float dis_x = Mathf.Abs(chess_x - destination_x);
        float dis_z = Mathf.Abs(chess_z - destination_z);
        //Debug.Log("x坐标" + chess_x);
        //Debug.Log("z坐标" + chess_z);
        if (((dis_x < 1.2 && dis_x > 0.1) ^ (dis_z < 1.2 && dis_z > 0.1)))
        {
            if ((destination_x < 1.8 && destination_x > -0.19) && (destination_z > 1.3 && destination_z < 3.2))
            {//物体移动到鼠标获取的坐标物体的位置
                chess.transform.position = hit.collider.gameObject.transform.position;
            }
        }
    }

    //炮的移动逻辑
    void Red_PaoMove(GameObject chess, RaycastHit hit)
    {
        //获得移动的目的地和需要移动棋子的局部坐标
        float chess_x = chess.transform.localPosition.x;
        float chess_z = chess.transform.localPosition.z;
        float destination_x = hit.collider.transform.localPosition.x;
        float destination_z = hit.collider.transform.localPosition.z;
        float dis_x = Mathf.Abs(chess_x - destination_x);
        float dis_z = Mathf.Abs(chess_z - destination_z);
        //Debug.Log("x坐标" + chess_x);
        //Debug.Log("z坐标" + chess_z);
        //这里因为移动后物体的坐标存在很小的误差，所以判断相同时用到坐标差的绝对值
        if ((dis_x < 0.1 && chess_z != destination_z) ^ (chess_x != destination_x && dis_z < 0.1))
        {
            chess.transform.position = hit.collider.gameObject.transform.position;
        }
    }
    //移动棋子的脚本


    void Red_Move()
    {   //判断鼠标输入
        if (Input.GetMouseButtonUp(0))
        {   //定义射线从屏幕到鼠标位置
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hitInfo))
            {   //存放射线检测的物体的标签
                string ob_tag = hitInfo.collider.gameObject.tag;
                switch (ob_tag)
                {
                    //如果标签为红色的话，储存存放棋子的gameobject
                    case "RedPiece":
                        //存放要移动的棋子
                        gameObj = hitInfo.collider.gameObject;
                        flag = 0;
                        break;
                    //如果为棋盘坐标，移动棋子
                    case "Coordinate":
                        if (flag == 0)
                        {
                            flag = 1;
                            string piece_name = gameObj.name;
                            switch (piece_name)
                            {
                                case "red_bing(Clone)":
                                    Red_BingMove(gameObj, hitInfo);
                                    //Debug.Log(piece_name);
                                    break;
                                case "red_shi(Clone)":
                                    Red_ShiMove(gameObj, hitInfo);
                                    //Debug.Log(piece_name);
                                    break;
                                case "red_ma(Clone)":
                                    Red_MaMove(gameObj, hitInfo);
                                    //Debug.Log(piece_name);
                                    break;
                                case "red_xiang(Clone)":
                                    Red_XiangMove(gameObj, hitInfo);
                                    //Debug.Log(piece_name);
                                    break;
                                case "red_che(Clone)":
                                    Vector3 offSet_che = hitInfo.collider.gameObject.transform.position - gameObj.transform.position;
                                    Ray ray_che = new Ray(gameObj.transform.position, offSet_che);
                                    RaycastHit hitInfo_che;
                                    Physics.Raycast(ray_che, out hitInfo_che);
                                    //用射线检测棋子到目的坐标中是否有别的棋子
                                    while (hitInfo_che.collider.gameObject.tag == "Coordinate" && hitInfo.collider.gameObject.name != hitInfo_che.collider.gameObject.name)
                                    {
                                        ray_che = new Ray(hitInfo_che.collider.gameObject.transform.position, offSet_che);
                                        Physics.Raycast(ray_che, out hitInfo_che);
                                    }
                                    //如果没有则移动
                                    if (hitInfo_che.collider.gameObject.name == hitInfo.collider.gameObject.name)
                                    {

                                        Red_CheMove(gameObj, hitInfo);
                                        //Debug.Log(piece_name);
                                    }
                                    break;
                                case "red_jiang(Clone)":
                                    Red_JiangMove(gameObj, hitInfo);
                                    //Debug.Log(piece_name);
                                    break;
                                case "red_pao(Clone)":
                                    Vector3 offSet_pao = hitInfo.collider.gameObject.transform.position - gameObj.transform.position;
                                    Ray ray_pao = new Ray(gameObj.transform.position, offSet_pao);
                                    RaycastHit hitInfo_pao;
                                    Physics.Raycast(ray_pao, out hitInfo_pao);
                                    
                                    while (hitInfo_pao.collider.gameObject.tag == "Coordinate"&&hitInfo.collider.gameObject.name!=hitInfo_pao.collider.gameObject.name)
                                    {
                                        ray_pao = new Ray(hitInfo_pao.collider.gameObject.transform.position, offSet_pao);
                                        Physics.Raycast(ray_pao, out hitInfo_pao);
                                    }
                                    
                                    if (hitInfo_pao.collider.gameObject.name==hitInfo.collider.gameObject.name)
                                    {

                                        Red_PaoMove(gameObj, hitInfo);
                                        //Debug.Log(piece_name);
                                    }
                                    break;
                            }
                        }
                        break;
                        //如果为敌对棋子，Destroy棋子然后移动
                    case "BlackPiece":
                        if (flag == 0)
                        {
                            flag = 1;
                            string piece_name = gameObj.name;

                            switch (piece_name)
                            {
                                case "red_bing(Clone)":
                                    Destroy(hitInfo.collider.gameObject);
                                    Red_BingMove(gameObj, hitInfo);
                                    //Debug.Log(piece_name);
                                    break;
                                case "red_shi(Clone)":
                                    Destroy(hitInfo.collider.gameObject);
                                    Red_ShiMove(gameObj, hitInfo);
                                    //Debug.Log(piece_name);
                                    break;
                                case "red_ma(Clone)":
                                    Destroy(hitInfo.collider.gameObject);
                                    Red_MaMove(gameObj, hitInfo);
                                    //Debug.Log(piece_name);
                                    break;
                                case "red_xiang(Clone)":
                                    Destroy(hitInfo.collider.gameObject);
                                    Red_XiangMove(gameObj, hitInfo);
                                    //Debug.Log(piece_name);
                                    break;
                                case "red_che(Clone)":
                                    Vector3 offSet_che = hitInfo.collider.gameObject.transform.position - gameObj.transform.position;
                                    Ray ray_che = new Ray(gameObj.transform.position, offSet_che);
                                    RaycastHit hitInfo_che;
                                    Physics.Raycast(ray_che, out hitInfo_che);
                                    //用射线检测棋子到目的坐标中是否有别的棋子
                                    while (hitInfo_che.collider.gameObject.tag == "Coordinate" && hitInfo.collider.gameObject.name != hitInfo_che.collider.gameObject.name)
                                    {
                                        ray_che = new Ray(hitInfo_che.collider.gameObject.transform.position, offSet_che);
                                        Physics.Raycast(ray_che, out hitInfo_che);
                                    }
                                    //如果没有则移动
                                    if (hitInfo_che.collider.gameObject.name == hitInfo.collider.gameObject.name)
                                    {
                                        Destroy(hitInfo.collider.gameObject);
                                        Red_CheMove(gameObj, hitInfo);
                                        //Debug.Log(piece_name);
                                    }
                                    break;
                                case "red_jiang(Clone)":
                                    Destroy(hitInfo.collider.gameObject);
                                    Red_JiangMove(gameObj, hitInfo);
                                    //Debug.Log(piece_name);
                                    break;
                                case "red_pao(Clone)":
                                    Vector3 offSet_pao = hitInfo.collider.gameObject.transform.position - gameObj.transform.position;
                                    Ray ray_pao = new Ray(gameObj.transform.position, offSet_pao);
                                    RaycastHit hitInfo_pao;
                                    Physics.Raycast(ray_pao, out hitInfo_pao);

                                    while (hitInfo_pao.collider.gameObject.tag == "Coordinate")
                                    {
                                        ray_pao = new Ray(hitInfo_pao.collider.gameObject.transform.position, offSet_pao);
                                        Physics.Raycast(ray_pao, out hitInfo_pao);
                                    }

                                    if (hitInfo_pao.collider.gameObject.transform.position != hitInfo.collider.gameObject.transform.position)
                                    {
                                        Destroy(hitInfo.collider.gameObject);
                                        Red_PaoMove(gameObj, hitInfo);
                                        //Debug.Log(piece_name);
                                    }
                                    break;
                            }
                        }
                        break;
                }
                //Debug.Log(flag);
            }

        }

    }


    void Start()

    {

    }
}