using UnityEngine;
//! save avatar
public class LocalPrefController : MonoBehaviour {

    void Start()
    {
        LoadAllData();
    }
    /**
     * load avatar yang sudah dibuat
     * */
    public void LoadAllData()
    {
        
        PlayerPrefs.GetInt(AvatarTag.HAIR, 0);
        PlayerPrefs.GetInt(AvatarTag.BODY, 0);
        PlayerPrefs.GetInt(AvatarTag.FACE, 0);
        PlayerPrefs.GetInt(AvatarTag.HEAD, 0);
        PlayerPrefs.GetInt(AvatarTag.GENDER, 0);
    }
    /**
     * variable avatar untuk di save
     * */
    public void SaveVariable(string vars, int val)
    {
        SaveVariable(vars, val);
    }
}
