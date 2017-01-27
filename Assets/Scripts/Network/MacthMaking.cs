using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MacthMaking : MonoBehaviour {
    public string PlayerName = "Kalmera";
    public GameObject PlayerPrefab;
    public GameObject Ball;
    public float WaitTime = 60f;

    private HostData[] _host;
    private const string _typeName = "8xgkXLx7b4c2F2EC6JNVHkYP8WWhSpPvWnAxxKazqmpLcUNRRh";

    private string _gameName;
    private bool _gameStarted = false;
    private bool _checkGame = true;
    private bool _startTimer = false;
    private NetworkView _netView;
    private NetworkPlayer _netPlay;
    private myInput myi,myi2;
    private groundCollision gro,gro2;
    private kaleCollisionController kaleo;
    private pyhsicsOfItems poi;

    void OnGUI()
    {
       /* if(!_gameStarted)
        if (GUI.Button(new Rect(100, 100, 200, 200), "Macth"))
            CheckGame();*/

    }

   public void CheckGame()
    {
        Debug.Log("Checking Available Games...");
        if (_host.Length == 0){         
            Debug.Log("No Availale Games, Creating One...");
            StartGame();
        }
        else
        {
            Debug.Log("A Game Has Found, Connecting...");
            JoinGame(_host[0]);
        }

    }

    void StartGame()
    {

        _gameName =  PlayerName + "'s Game";
    //    Network.InitializeServer(2, 25000, !Network.HavePublicAddress()); 
       Network.InitializeServer(5, 25000, true); //Nat olup olmadığını kontrol edip ona göre ayarla ture değerini
        MasterServer.RegisterHost(_typeName, _gameName);
        _startTimer = true;
        Debug.Log("Game Started. Waiting For a Connection");
        
    }
    void JoinGame(HostData Host)
    {
        Debug.Log("Joining "+Host.gameName);
        Network.Connect(Host);

    }
    void OnPlayerConnected(NetworkPlayer player)
    {
        _startTimer = false;
        _checkGame = false;
        _gameStarted = true;
        MasterServer.UnregisterHost();
        Debug.Log(player.ipAddress+" Connected to The Server");
        SceneManager.LoadScene("PlayGround");//Wil change to a RPC function later
      
    }
    void OnConnectedToServer()
    {
        Debug.Log("Connected to"+_host[0].gameName);
        _checkGame = false;
        _gameStarted = true;
        SceneManager.LoadScene("PlayGround");//Wil change to a RPC function later
      
    }

    void Start() {
        _netView = GetComponent<NetworkView>();
   //     _netPlay = GetComponent<NetworkPlayer>();
        MacthMaking.DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }

    void Assign(GameObject Player)
    {
            myi = GameObject.FindGameObjectWithTag("myin").GetComponent<myInput>();
            myi.KnowMe(Player);
            gro = GameObject.FindGameObjectWithTag("gro").GetComponent<groundCollision>();
            gro.KnowMe(Player,myi);
            poi = GameObject.FindGameObjectWithTag("poi").GetComponent<pyhsicsOfItems>();
            poi.knowMe(Player, gro.gameObject);
            Player.GetComponent<MyCharacterController>().poi = poi;

    }

    void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)//Wil change to a RPC function later
    {
      
            SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        if (Network.isServer)
        {
            GameObject a = (GameObject)Network.Instantiate(PlayerPrefab, new Vector3(-6, -3.290302f, 0), Quaternion.Euler(0, 0, 0), 0);
            Network.Instantiate(Ball, Vector3.up, Quaternion.identity, 0);
            Assign(a);
        }
        else
        {
            GameObject b = (GameObject)Network.Instantiate(PlayerPrefab, new Vector3(6, -3.290302f, 0), Quaternion.Euler(0, 180, 0), 0);
            StartCoroutine(Wait1SecondForDetectBall(b));
        }

    }

    void OnDestroy()//for an unxpected close before the closing the connection
    {
        if (Network.isServer)
            Disconnect();
    }

    void Update()
    {
        if (_checkGame) {
            MasterServer.RequestHostList(_typeName);
            _host = MasterServer.PollHostList();
        }

        if(_startTimer && !(WaitTime <= 0.0f))
        Timer();
    }

    void Timer()
    {
        WaitTime -= Time.deltaTime;
        if(WaitTime<0.0f)
            Disconnect();
    }

   public void Disconnect()
    {
        
        Network.Disconnect(1000);      
        MasterServer.UnregisterHost();
        _gameStarted = false;
        SceneManager.LoadScene(0);
      
    }

    void Read(BitStream XML)
    {

    }
    void ModifyPlayer()
    {

    }


    // every 2 seconds perform the print()
    //But why tho?
    private IEnumerator Wait1SecondForDetectBall(GameObject b)
    {
        yield return new WaitForSeconds(1f);
        Assign(b);
    }
    void OnPlayerDisconnected(NetworkPlayer player)
    {
        Debug.Log("Clean up after player " + player);
        Network.RemoveRPCs(player);
        Network.DestroyPlayerObjects(player);
        Disconnect();
      //  Destroy(GameObject.FindGameObjectWithTag("Menu"));
       // Destroy(gameObject);     
        SceneManager.UnloadScene("PlayGround");
        SceneManager.LoadScene(0);

    }
   
}
