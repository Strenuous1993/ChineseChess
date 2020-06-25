using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkMgr : MonoBehaviour {
    //连接IP
    private const string ip = "127.0.0.1";
    //连接Port
    private const int port = 30000;
    //是否使用NAT功能
    private bool _useNat = false;
    // Use this for initialization
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
    }

    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //棋子初始化；
    public void InitialPiece()
    {
        foreach (KeyValuePair<string, GameObject> kvp in openWith)
        {
            //获得父级Object
            //GameObject obj1 = GameObject.Find("Coordinates");
            GameObject zuobiao = GameObject.Find(kvp.Key);
            //在相应坐标未知生成物体
            GameObject chess;
            chess = (GameObject)Network.Instantiate(kvp.Value, zuobiao.transform.position, Quaternion.identity, 0);
            //Debug.Log(zuobiao.name);
            //设置棋盘为父物体，方便实现棋子的移动逻辑；
            chess.transform.parent = chess_board.transform;
        }
    }
    //游戏服务器初始化正常完结时创建棋子
    void OnServerInitialized()
    {
        InitialPiece();
    }
    //有玩家连接游戏服务器是调用
    void OnConnetedInitialized()
    {
        InitialPiece();
    }

    void OnGUI()
    {   //判断玩家当前是否连接网络
        if(Network.peerType == NetworkPeerType.Disconnected)
        {
            //生成启动游戏服务器按钮
            if(GUI.Button(new Rect(20, 20, 200, 25), "Start Server"))
            {
                //初始化服务器（连接数，端口号，是否使用NAT）
                Network.InitializeServer(20, port, _useNat);
            }
            //生成连接服务器按钮
            if(GUI.Button(new Rect(20, 50, 200, 25), "Connet to Server"))
            {
                Network.Connect(ip, port);
            }
        }
        else
        {
            if(Network.peerType == NetworkPeerType.Server)
            {
                GUI.Label(new Rect(20, 20, 200, 25), "Initialization Server...");
                GUI.Label(new Rect(20, 50, 200, 25), "Client Count = " + Network.connections.Length.ToString());
            }
            if(Network.peerType == NetworkPeerType.Client)
            {
                GUI.Label(new Rect(20, 20, 200, 25), "Conneted to Server");
            }
        }
            

    }
}
