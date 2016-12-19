using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/** Button function.
 *  Dipakai saat pemilihan id;
 * */
public class SplashController : MonoBehaviour {

    public NetworkVariable networkVariable; //!< networkVariable 
    public GameObject nextScene; //!< gameObject diaktifkan setelah memilih id

    /**
     * Set id, lalu lanjut ke scren berikutnya.
     * @param id 1 - Host
     * @param id lebih 1 - Client , maksimal 3
     * */
    public void SetIDAndChangeScreen(int id)
    {
        nextScene.SetActive(true);
        networkVariable.gameObject.SetActive(true);
        networkVariable.thisPlayer = id;
        this.gameObject.SetActive(false);
    }
}
