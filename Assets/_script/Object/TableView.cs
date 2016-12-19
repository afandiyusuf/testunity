using UnityEngine;
//! pemanpilan tabble
public class TableView : MonoBehaviour {

    void Start()
    {
    }

    void OnGUI()
    {
        float win = Screen.width * 0.6f;
        float w1 = win * 0.35f; float w2 = win * 0.15f; float w3 = win * 0.35f;

                GUILayout.BeginHorizontal();
                GUILayout.Label("asfafasfa", GUILayout.Width(w1));
                GUILayout.Label("asfafasfa", GUILayout.Width(w2));
                GUILayout.Label("asfafasfa", GUILayout.Width(w3));
                GUILayout.EndHorizontal();
        
    } 

}
