using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//! gambar icon jawaban akan diacak
public class randomSpriteSoal : MonoBehaviour {
    public int i; /*!<nomor gambar icon jawaban yang sudah diacak*/
    public Sprite[] _sprite;/*!<seluruh gambar icon jawaban yang disediakan*/
    public GameObject[] _obj;/*!<objek yang akan diganti gambar icon nya*/

	void Start () {
        i = Random.Range(0, _sprite.Length);
        //o = _obj.Length-1;
    }

    // Update is called once per frame
    void Update () {
        //gameObject.GetComponent<Image>().sprite = _sprite[i];
        foreach (GameObject obj in _obj)
        {
            obj.GetComponent<Image>().sprite = _sprite[i];
        }        
    }
    /**
     * gambar icon akan diacak ulang bila player sudah menjawab soal.
     * */
    public void ChangeRandom()
    {
        i = Random.Range(0, _sprite.Length);
    }
}
