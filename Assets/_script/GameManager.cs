using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
//! game manager (pengaturan gameplay)
public class GameManager : MonoBehaviour {
	public GameObject player; //!< objek player
	public Camera cam; //!< kamera
	public GameObject battleScene; //!< canvas prebattle
	public GameObject loadingScreen; //!< canvas loading
	public DemitSpawner dSpawn; //!< script DemitSpawner (munculnya musuh) 
	public Vector3 lastPos; //!< posisi player sebelum menuju battle scene
	public Vector3 camLastPos; //!< posisi kamera sebelum menuju battle scene
    public Vector3 tempPos; //!< posisi player ketika awal main
	public AudioSource bgm; //!< background music
	public int sceneId; //!< nomor scene pada build in
	string namaTantangan = "Dup_Quiz";

	int firstrun = 0;

	void Awake()
	{
		DisablingLoadingScreen();
	}

	void Start()
	{
		firstrun = PlayerPrefs.GetInt("savedFirstRun");
		LoadPos();

		if (firstrun == 1){
			tempPos = new Vector3(PlayerPrefs.GetFloat("xTemp"),PlayerPrefs.GetFloat("yTemp"), 0);
			player.transform.position = tempPos;
			Invoke("ToSavedPos", 0.05f);
		}

		Invoke("ChangeFirstRun", 2);
		Debug.Log(firstrun);
	}

    /** posisi player di save sebelum menuju battle scene**/
	public void SavePlayerPos()
	{
		PlayerPrefs.SetFloat("xPos", player.gameObject.transform.position.x);
		PlayerPrefs.SetFloat("yPos", player.gameObject.transform.position.y);
		PlayerPrefs.SetFloat("zPos", player.gameObject.transform.position.z);
		loadingScreen.gameObject.SetActive(true);
		SaveCameraPos();
		PlayerPrefs.SetInt("sId", sceneId);
		goBattle();
	}
    /** player memutuskan untuk kabur dari battle**/
	public void CancelBattle()
	{
		dSpawn.backTo();
	}
    /** posisi di load ketika player kembali dari battle scene menuju map scene**/
	public void LoadPos()
	{
		float x = PlayerPrefs.GetFloat("xPos");
		float y = PlayerPrefs.GetFloat("yPos");
		float z = PlayerPrefs.GetFloat("zPos");

		float xc = PlayerPrefs.GetFloat("xCPos");
		float yc = PlayerPrefs.GetFloat("yCPos");
		float zc = PlayerPrefs.GetFloat("zCPos");

		lastPos = new Vector3(x, y, z);
		camLastPos = new Vector3 (xc, yc, zc);

	}
	void SaveCameraPos()
	{
		PlayerPrefs.SetFloat("xCPos", cam.transform.position.x);
		PlayerPrefs.SetFloat("yCPos", cam.transform.position.y);
		PlayerPrefs.SetFloat("zCPos", cam.transform.position.z);
		FollowingCamera fc = cam.GetComponent<FollowingCamera>();
		PlayerPrefs.SetFloat("posX", fc.posX);
		PlayerPrefs.SetFloat("posY", fc.posY);

		PlayerPrefs.SetInt("isInArea", (fc.isInArea)?1:0);


	}

	void DisablingLoadingScreen()
	{
		loadingScreen.gameObject.SetActive(false);
	}

	void ChangeFirstRun()
	{
		PlayerPrefs.SetInt("savedFirstRun", 0);
	}

	void ToSavedPos()
	{
		player.transform.position = lastPos;
		cam.transform.position = camLastPos;

		Debug.Log("saved");
	}

	void goBattle()
	{		
		LoadPos();
		PlayerPrefs.SetInt("savedFirstRun", 1);
		SceneManager.LoadScene(namaTantangan);
	}
    /** ketika player menekan tombol kembali ke peta maka permainan akan kembali ke peta **/
	public void BackToMap()
	{
		loadingScreen.gameObject.SetActive(true);
		SceneManager.LoadScene("Demo_petabesar");
	}
}
