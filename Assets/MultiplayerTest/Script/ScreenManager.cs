using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/**
 * temporary class, hanya digunakan untuk demo.
 */
public class ScreenManager : MonoBehaviour {

    public Text[] mainText;
    public Text[] secondText;

    public Text thisMainText;
    public Text thisSecondText;

    private NetworkVariable networkVar;
    public Text mainGameSession;
   
    void Start()
    {
        networkVar = GetComponent<NetworkVariable>();
    }

    public void SetAllText()
    {

        for(int i=0;i<mainText.Length;i++)
        {
            mainText[i].text = networkVar.localPlayerSession[i].playerGameSession;
        }

    }
    public void SetMainSession(string MainGameSession)
    {
        mainGameSession.text = MainGameSession;
    }
}
