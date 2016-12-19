using UnityEngine;
using System.Collections;

/**
 * Network variable class.
 * Semua variable yang digunakan selama networking disimpan di sini
 */
public class NetworkVariable : MonoBehaviour
{

    /**
     * Player Session class
     * Menyimpan session detail setiap player
     * */
    [System.Serializable]
    public class playerSession
    {
        public int playerNum; //!< identitas player
        public string playerName; //!< nama player
        public int playerScore; //!< score player
        public string playerGameSession = IDLE; //!< session player JOINED,WAITING,IDLE,FINISH_GAME dsb
        public bool isHost = false; //!< true jika player yang memulai game pertama kali, dan lainnya join

        public static string JOINED = "joined"; //!< state yang menandakan kalau user (client) sudah join ke game host, dipakai saat game state waiting (di lobby)
        public static string WAITING = "waiting"; //!<  state yang menandakan kalau user (host) sedang menunggu
        public static string IDLE = "idle"; //!< state yang dipakai saat user tidak menjalankan session game
        public static string FINISH_GAME = "finish game"; //!<  state yang dipakai saat user selesai memainkan game 
        public static string WAIT_FOR_READY = "wait for ready"; //!<  state yang dipakai saat waktu menunggu sudah selesai, dan berlanjut menunggu untuk user lain konfirmasi ready
        public static string PLAYING = "playing"; //!<  state yang dipakai saat player dalam keadaan sedang bermain game.
        public static string READY = "ready"; //!<  state yang dipakai saat player sudah konfirmasi ready
    }

    public const string GAME_IDLE = "game_idle"; //!<  session game, saat game tidak dimainkan 
    public const string GAME_WAITING = "game_waiting"; //!<  session game, saat host sudah memulai game, dan sedang menunggu client yang lain untuk join
    public const string GAME_WAIT_FOR_READY = "game_wait_for_ready"; //!<  session game, saat sesi menunggu sudah selesai, dan menunggu player untuk konfirmasi ready
    public const string GAME_PLAYING = "game_playing"; //!<  session game, saat game sedang dimulai
    public const string GAME_FINISH = "game_finish"; //!<  session game, saat game sudah selesai

    public string gameSession = GAME_IDLE; //!<  variabel yang digunakan untuk menyimpan session game saat ini */

    public int totalPlayer; //!< total player yang sedang mengikuti game ini (maks player 3)
    public int totalPlayerReady = 0; //!< total player yang sudah ready (menyelesaikan tutorial)
    public int thisPlayer; //!< nomor player user (host) sebagai identitas, dan di set di setting game
    public int waitLobbyTime = 10; //!< total waktu yang diberikan saat menekan menekan play, dan menunggu player lain untuk bergabung
    public int playerJoined = 0; //!< total player yang join game
    public playerSession[] localPlayerSession = new playerSession[3]; //!< session semua player yang disimpan di local dan nantinya akan di sinkronisasikan ke server


    void Awake()
    {
        localPlayerSession[0].playerGameSession = playerSession.IDLE;
        localPlayerSession[1].playerGameSession = playerSession.IDLE;
        localPlayerSession[2].playerGameSession = playerSession.IDLE;
    }
}
