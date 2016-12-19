using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

//! Core class yang mengatur segala macam networking package ini
public class DiscoverNetwork:NetworkDiscovery  {
    public float LongCheckHost = 2; //!< lama client cek host sebelum berubah jadi host SetClient(hybrid true)

    NetworkClient myClient;
    private bool isStartCheckHost;
    private string addressFound;
    
    private NetworkVariable networkVariable;
    private ScreenManager screenManager;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        networkVariable = GetComponent<NetworkVariable>();
        screenManager = GetComponent<ScreenManager>();

        if (networkVariable.thisPlayer == 1)
            HostStart();
        else
            ClientStart(false);
    }

    
    /** Digunakan untuk host game.
     * Host bersifat statis, jadi siapapun player yang memulai, host tetap pada pada mesin tersebut (index setting 1)
     * */
    public void HostStart()
    {
        this.Initialize();
        this.StartAsServer();
        this.SetupServer();
    }

    /** Digunakan untuk client game.
     * Host bersifat statis, jadi siapapun player yang memulai, host tetap pada pada mesin tersebut (index setting 1).
     * @param Hybrid - boolean - jika true akan otomatis jadi host saat waktu tunggu selesai namun tidak menemukan host. false - menunggu forever
     * */
    public void ClientStart(bool Hybrid)
    {

#if UNITY_EDITOR
        Debug.Log("Initialize searching host as client");
#endif

        this.Initialize();
        this.StartAsClient();
        this.isStartCheckHost = true;

        if (Hybrid)
            Invoke("Inv_FinishCheck",LongCheckHost);

    }

    void Inv_FinishCheck()
    {
        //this.StopBroadcast();
        this.Initialize();
        this.StartAsServer();
        isStartCheckHost = false;
        this.SetupServer();
#if UNITY_EDITOR
        Debug.Log("host not found, start initializing as server");
#endif
    }

    /**
     * Saat host ditemukan
     * */
    public override void OnReceivedBroadcast(string fromAddress, string data)
    {
        base.OnReceivedBroadcast(fromAddress, data);
           
        //still searching and host not found before
        if (isStartCheckHost)
        {
            addressFound = fromAddress;
            //CancelInvoke("Inv_FinishCheck");
            
            isStartCheckHost = false;
#if UNITY_EDITOR
            Debug.Log("host found at "+addressFound+", start as client");
#endif
            this.SetupClient();

            
        }
    }

    /** 
     * Register dispacth event saat berkomunikasi
     * */
    void RegisterAllClientEvent()
    {
        myClient.RegisterHandler(MyNetworkMessageClass.CREATE_GAME, gameCreated_client);
        myClient.RegisterHandler(MyNetworkMessageClass.WAIT_FOR_READY, waitForReady_client);
        myClient.RegisterHandler(MyNetworkMessageClass.CONFIRM_READY, confirm_ready_client);
        myClient.RegisterHandler(MyNetworkMessageClass.FINISH_GAME, finish_client);
        myClient.RegisterHandler(MyNetworkMessageClass.BACK_TO_IDLE, back_to_Idle);
    }

    void SetupClient()
    {
        myClient = new NetworkClient();
        myClient.Connect(addressFound, this.broadcastPort + 3);

        RegisterAllClientEvent();
    }

    void SetupServer()
    {
        NetworkServer.Listen(this.broadcastPort+3);
        NetworkServer.RegisterHandler(MyNetworkMessageClass.CREATE_GAME, blastToAll);
        NetworkServer.RegisterHandler(MyNetworkMessageClass.WAIT_FOR_READY, blastToAll);
        NetworkServer.RegisterHandler(MyNetworkMessageClass.CONFIRM_READY, blastToAll);
        NetworkServer.RegisterHandler(MyNetworkMessageClass.FINISH_GAME, blastToAll);
        NetworkServer.RegisterHandler(MyNetworkMessageClass.BACK_TO_IDLE, blastToAll);

        myClient = ClientScene.ConnectLocalServer();
        RegisterAllClientEvent();

        Debug.Log("Server started at port " + (broadcastPort + 3));
    }
    
    void SyncSession(NetworkMessage message,bool forceAll = false)
    {
        MyNetworkMessageClass myMess = message.ReadMessage<MyNetworkMessageClass>();
        networkVariable.totalPlayer = 0;

        for (int i = 0; i < 3; i++)
        {
            if (networkVariable.localPlayerSession[i].playerGameSession != NetworkVariable.playerSession.IDLE)
                networkVariable.totalPlayer++;

            if (i == networkVariable.thisPlayer - 1 && !forceAll)
                continue;

            networkVariable.localPlayerSession[i] = myMess.syncPlayerSession[i];
        }
    }

    void gameCreated_client(NetworkMessage message)
    {

        SyncSession(message);

        networkVariable.gameSession = NetworkVariable.GAME_WAITING;
        screenManager.SetMainSession(NetworkVariable.GAME_WAITING);
        screenManager.SetAllText();
   }

    void waitForReady_client(NetworkMessage message)
    {
        SyncSession(message,true);

        networkVariable.gameSession = NetworkVariable.GAME_WAIT_FOR_READY;
        screenManager.SetMainSession(NetworkVariable.GAME_WAIT_FOR_READY);
        screenManager.SetAllText();
    }

    void back_to_Idle(NetworkMessage message)
    {
        SyncSession(message, true);

        networkVariable.gameSession = NetworkVariable.GAME_IDLE;
        screenManager.SetMainSession(NetworkVariable.GAME_IDLE);

        screenManager.SetAllText();
    }

    void confirm_ready_client(NetworkMessage message)
    {
        SyncSession(message);
        networkVariable.totalPlayerReady = 0;
        for(int i=0;i<networkVariable.localPlayerSession.Length;i++)
        {
            if (networkVariable.localPlayerSession[i].playerGameSession == NetworkVariable.playerSession.READY)
                networkVariable.totalPlayerReady++;
        }

        if (networkVariable.totalPlayerReady == networkVariable.totalPlayer)
        {
            networkVariable.gameSession = NetworkVariable.GAME_PLAYING;
            screenManager.SetMainSession(NetworkVariable.GAME_PLAYING);
        }

        screenManager.SetAllText();
    }

    void finish_client(NetworkMessage message)
    {
        SyncSession(message);

        int PlayerFinish = 0;
        int i = 0;
        for (i=0;i<networkVariable.localPlayerSession.Length;i++)
        {
            if (networkVariable.localPlayerSession[i].playerGameSession == NetworkVariable.playerSession.FINISH_GAME)
                PlayerFinish++;
        }

        if(networkVariable.totalPlayerReady == PlayerFinish)
        {
            networkVariable.gameSession = NetworkVariable.GAME_FINISH;
            float maxVal = 0;
            int noPlayer = 0;
            //cek max score
            for(i=0;i<networkVariable.localPlayerSession.Length;i++)
            {
                if(maxVal < networkVariable.localPlayerSession[i].playerScore)
                {
                    maxVal = networkVariable.localPlayerSession[i].playerScore;
                    noPlayer = (i + 1);
                }
            }

            screenManager.mainGameSession.text = "THE WINNER IS PLAYER " + noPlayer.ToString() + " SCORE:" + maxVal.ToString();


            MyNetworkMessageClass myMess = new MyNetworkMessageClass();

            for(i=0;i< networkVariable.localPlayerSession.Length;i++)
            {
                networkVariable.localPlayerSession[i].playerGameSession = NetworkVariable.playerSession.IDLE;
                networkVariable.localPlayerSession[i].isHost = false;
            }
            
            myMess.syncPlayerSession = networkVariable.localPlayerSession;
            myMess.fromPlayer = networkVariable.thisPlayer;

            myClient.Send(MyNetworkMessageClass.BACK_TO_IDLE, myMess);
        }

        screenManager.SetAllText();
    }



    void blastToAll(NetworkMessage message)
    {

        MyNetworkMessageClass myMess = message.ReadMessage<MyNetworkMessageClass>();

        if (message.msgType == MyNetworkMessageClass.CREATE_GAME)
            networkVariable.gameSession = NetworkVariable.GAME_WAITING;
        else if (message.msgType == MyNetworkMessageClass.WAIT_FOR_READY)
            networkVariable.gameSession = NetworkVariable.GAME_WAIT_FOR_READY;
       
        NetworkServer.SendToAll(message.msgType, myMess);
    }

    void WaitForReady()
    {
        MyNetworkMessageClass myMess = new MyNetworkMessageClass();

        for (int i=0;i<networkVariable.localPlayerSession.Length;i++)
        {
            if (networkVariable.localPlayerSession[i].playerGameSession == NetworkVariable.playerSession.IDLE)
                continue;

            networkVariable.localPlayerSession[i].playerGameSession = NetworkVariable.playerSession.WAIT_FOR_READY;
        }
        myMess.syncPlayerSession = networkVariable.localPlayerSession;

        screenManager.SetAllText();
        myClient.Send(MyNetworkMessageClass.WAIT_FOR_READY, myMess);
    }


    /*
     * 
     * ======== PUBLIC FUNCTION ===========
     * 
     * 
     * */

    /**
     * Fungsi playGames bersifat hybrid.
     * Jika belum ada player yang memulai, dia akan menjadi host (hanya status)
     * Jika di panggil saat host menunggu player lain, maka dia akan join
     * Jika di panggil saat game sedang berlangsung, tidak akan terjadi apa-apa
     *  */
    public void PlayGames()
    {
        MyNetworkMessageClass myMess = new MyNetworkMessageClass();

        if (networkVariable.gameSession == NetworkVariable.GAME_IDLE)
        {
            networkVariable.localPlayerSession[(networkVariable.thisPlayer - 1)].playerGameSession = NetworkVariable.playerSession.WAITING;
            networkVariable.localPlayerSession[(networkVariable.thisPlayer - 1)].isHost = true;

          

            myMess.syncPlayerSession = networkVariable.localPlayerSession;
            myMess.fromPlayer = networkVariable.thisPlayer;

            Invoke("WaitForReady", networkVariable.waitLobbyTime); // panggil wait for ready setelah waktu menunggu selesai

            myClient.Send(MyNetworkMessageClass.CREATE_GAME, myMess);

        }
        else if (networkVariable.gameSession == NetworkVariable.GAME_WAITING)
        {
            if (!networkVariable.localPlayerSession[(networkVariable.thisPlayer - 1)].isHost)
            {
                networkVariable.localPlayerSession[(networkVariable.thisPlayer - 1)].playerGameSession = NetworkVariable.playerSession.JOINED;

                myMess.syncPlayerSession = networkVariable.localPlayerSession;
                myMess.fromPlayer = networkVariable.thisPlayer;
                myClient.Send(MyNetworkMessageClass.CREATE_GAME, myMess);
            }
            else
            {
                Debug.Log("You already as host");
            }
        }
    }

    /**
     * User selesai mengikuti tutorial, dan sudah ready untuk bermain game
     * */
    public void ConfirmReady()
    {
        MyNetworkMessageClass myMess = new MyNetworkMessageClass();
        networkVariable.localPlayerSession[(networkVariable.thisPlayer - 1)].playerGameSession = NetworkVariable.playerSession.READY;
        myMess.syncPlayerSession = networkVariable.localPlayerSession;
        myMess.fromPlayer = networkVariable.thisPlayer;

        myClient.Send(MyNetworkMessageClass.CONFIRM_READY, myMess);
    }

    /** 
     * User konfirmasi kalau game sudah selesai, disini proses pengiriman score dimulai
     * */
    public void FinishGame()
    {
        MyNetworkMessageClass myMess = new MyNetworkMessageClass();
        networkVariable.localPlayerSession[(networkVariable.thisPlayer - 1)].playerGameSession = NetworkVariable.playerSession.FINISH_GAME;
        networkVariable.localPlayerSession[(networkVariable.thisPlayer - 1)].playerScore = Random.Range(1000, 5000);
        myMess.syncPlayerSession = networkVariable.localPlayerSession;
        myMess.fromPlayer = networkVariable.thisPlayer;

        myClient.Send(MyNetworkMessageClass.FINISH_GAME, myMess);
    }

}

/**
 * MyNetworkMessageClass - Base message untuk komunikasi antar client
 * */
public class MyNetworkMessageClass : MessageBase
{
    public static short CREATE_GAME = 1001; //!< saat player menekan tombol play (create,wait)
    public static short WAIT_FOR_READY = 1002; //!< saat waktu tunggu sudah selesai,
    public static short PLAYING = 1003; //!< saat game sudah dimulai;
    public static short CONFIRM_READY = 1004; //!< saat user konfirmasi sudah ready
    public static short FINISH_GAME = 1005; //!< saat user sudah menyelesaikan game dan mengirimkan score
    public static short BACK_TO_IDLE = 1006; //!< saat semua player sudah menyelesaikan game dan game kembali idle

    public int fromPlayer; //!< index player pengirim 1-3
    public NetworkVariable.playerSession[] syncPlayerSession = new NetworkVariable.playerSession[3]; //!< lokal session player pengirim, yang nantinya disinkronkan dengan player lainnya
}
