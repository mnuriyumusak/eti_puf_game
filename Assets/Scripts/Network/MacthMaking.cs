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

    private pyhsicsOfItems poi;
    private myInput myi;
    private groundCollision gro;
    private kaleCollisionController kaleo;

    void OnGUI()
    {
        if(!_gameStarted)
        if (GUI.Button(new Rect(100, 100, 200, 200), "Macth"))
            CheckGame();

    }

    void CheckGame()
    {
       
        if (_host.Length == 0)
            StartGame();
        else
        {
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
        print("Game Started. Waiting For a Connection");
        
    }
    void JoinGame(HostData Host)
    {
        print("Joining "+Host.gameName+"'s Game On: "+Host.ip);
        Network.Connect(Host);
    }
    void OnPlayerConnected(NetworkPlayer player)
    {
        _startTimer = false;
        _checkGame = false;
        _gameStarted = true;
        MasterServer.UnregisterHost();
        print(player.ipAddress+" Connected to The Server");
        SceneManager.LoadScene("PlayGround");
      
    }
    void OnConnectedToServer()
    {
        print("Player Connected to"+_host[0].ip);
        _checkGame = false;
        _gameStarted = true;
        SceneManager.LoadScene("PlayGround");
      
    }

    void Start() { NetworkManager.DontDestroyOnLoad(gameObject);
    SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }

    void Assign(GameObject Player)
    {
        poi = GameObject.FindGameObjectWithTag("poi").GetComponent<pyhsicsOfItems>();
        myi = GameObject.FindGameObjectWithTag("myin").GetComponent<myInput>();
        gro = GameObject.FindGameObjectWithTag("gro").GetComponent<groundCollision>();
        kaleo = GameObject.FindGameObjectWithTag("kaleo").GetComponent<kaleCollisionController>();

        poi.KnowMe();
        myi.KnowMe();
        gro.KnowMe();
        kaleo.KnowMe();
    }

    void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        SpawnPlayer();
 
    }
    private void SpawnPlayer()
    {
        if (Network.isServer)
        {
            GameObject a = (GameObject)Network.Instantiate(PlayerPrefab, new Vector3(-6, 0, 0), Quaternion.EulerRotation(0, 0, 0), 0);
            //Network.Instantiate(playerPrefab, new Vector3(-6, 0, 0), Quaternion.EulerRotation(0, 0, 0), 0);
            Network.Instantiate(Ball, Vector3.up, Quaternion.identity, 0);
            Assign(a);

        }
        else
        {
            GameObject b = (GameObject)Network.Instantiate(PlayerPrefab, new Vector3(6, 0, 0), Quaternion.EulerRotation(0, 180, 0), 0);
            Assign(b);
        }

    }
    void OnDestroy()//Bunu sil Sadece debug
    {
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

    void Disconnect()
    {
        
        Network.Disconnect();
        MasterServer.UnregisterHost();
        print("No Game Found");
        _gameStarted = false;
      
    }

    void Read(BitStream XML)
    {

    }
    void ModifyPlayer()
    {

    }
}
