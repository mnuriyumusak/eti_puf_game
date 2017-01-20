using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour
{
    private const string typeName = "8xgkXLx7b4c2F2EC6JNVHkYP8WWhSpPvWnAxxKazqmpLcUNRRh";
    private const string gameName = "KalmeraGame";

    private bool isRefreshingHostList = false;
    private HostData[] hostList;

    private pyhsicsOfItems poi;
    private myInput myi;
    private groundCollision gro;
    private kaleCollisionController kaleo;

    public GameObject playerPrefab;
    public GameObject Ball;
    void OnGUI()
    {
        if (!Network.isClient && !Network.isServer)
        {
            if (GUI.Button(new Rect(100, 100, 250, 100), "Start Server"))
                StartServer();

            if (GUI.Button(new Rect(100, 250, 250, 100), "Refresh Hosts"))
                RefreshHostList();

            if (hostList != null)
            {
                for (int i = 0; i < hostList.Length; i++)
                {
                    if (GUI.Button(new Rect(400, 100 + (110 * i), 300, 100), hostList[i].gameName))
                        JoinServer(hostList[i]);
                }
            }
        }
    }

    private void StartServer()
    {
       // Network.InitializeServer(5, 25000, !Network.HavePublicAddress());
        Network.InitializeServer(5, 25000, true);
        MasterServer.RegisterHost(typeName, gameName);
        
    }

    void OnServerInitialized()
    {
        SpawnPlayer();

    }

    void Start()
    {
        poi = GameObject.FindGameObjectWithTag("poi").GetComponent<pyhsicsOfItems>();
        myi = GameObject.FindGameObjectWithTag("myin").GetComponent<myInput>();
        gro = GameObject.FindGameObjectWithTag("gro").GetComponent<groundCollision>();
        kaleo = GameObject.FindGameObjectWithTag("kaleo").GetComponent<kaleCollisionController>();
    }
    void Assign(GameObject Player)
    {
        poi.KnowMe();
        myi.KnowMe();
        gro.KnowMe();
        kaleo.KnowMe();
    }
    void Update()
    {
        if (isRefreshingHostList && MasterServer.PollHostList().Length > 0)
        {
            isRefreshingHostList = false;
            hostList = MasterServer.PollHostList();
        }
    }

    private void RefreshHostList()
    {
        if (!isRefreshingHostList)
        {
            isRefreshingHostList = true;
            MasterServer.RequestHostList(typeName);
        }
    }


    private void JoinServer(HostData hostData)
    {
        Network.Connect(hostData);
    }

    void OnConnectedToServer()
    {
        SpawnPlayer();
    }


    private void SpawnPlayer()
    {
        if (Network.isServer)
        {
            GameObject a = (GameObject)Network.Instantiate(playerPrefab, new Vector3(-6, -3.29f, 0), Quaternion.identity, 0);
            a.tag = "pufCharacter";
            //Network.Instantiate(playerPrefab, new Vector3(-6, 0, 0), Quaternion.EulerRotation(0, 0, 0), 0);
            Network.Instantiate(Ball, Vector3.up, Quaternion.identity, 0);
            Assign(a);


        }
        else
        {
            GameObject b = (GameObject)Network.Instantiate(playerPrefab, new Vector3(6, -3.29f, 0), Quaternion.EulerRotation(0, 180, 0), 0);
            b.tag = "pufCharacter";
            Assign(b);

            
        }

       
    }
}
