using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
//! perpindahan scene map
public class SceneMapManager : MonoBehaviour {
	public GameObject button; /*!<objek tombol*/
    public GameObject loadingScreen; /*!<objek/canvas screen loading*/
    public string loadSceneName; /*!<nama dari scene yang dituju*/

    void Start()
	{
		loadingScreen.gameObject.SetActive(false);
		button.gameObject.SetActive(false);
	}
    /**
     * fungsi button yang megaktifkan loading.
     * pindah scene sesuai nama scene yang ada di loadSceneName.
     * */
	public void GoButton()
	{
		loadingScreen.gameObject.SetActive(true);
		SceneManager.LoadScene(loadSceneName);
	}
    /**
     * mengambil id scene dari loadSceneName.
     * */
	public void GetID(string id)
	{
		loadSceneName = id;
	}
}
