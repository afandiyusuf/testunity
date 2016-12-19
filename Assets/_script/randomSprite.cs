using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//! gambar icon soal akan diacak
public class randomSprite : MonoBehaviour {
    int i;
    public randomSpriteSoal rss; /*!<pemanggilan script randomSpriteSoal*/
    public Sprite[] _sprite; /*!<seluruh gambar icon soal yang disediakan*/
    //public GameObject[] _obj;
    // Use this for initialization
    void Start()
    {
        rss = GameObject.Find("CoreGameController").GetComponent<randomSpriteSoal>();
        i = rss.i;
        //o = _obj.Length-1;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Image>().sprite = _sprite[i];
    }
}
