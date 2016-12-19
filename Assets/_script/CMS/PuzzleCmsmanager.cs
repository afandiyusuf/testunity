using UnityEngine;
using System.Collections.Generic;
//! puzzle menulis huruf manager
public class PuzzleCmsmanager : MonoBehaviour {

    public GameObject TrueDotPrefabs; /*!<objek dot(titik) yang tepat menulis huruf*/
    public GameObject FalseDotPrefabs; /*!<objek dot(titik) yang salah dalam menulis huruf*/

    public List<GameObject> arrTrueDot = new List<GameObject>(); /*!<array dari dot(titik) yang benar*/
    public List<GameObject> arrFalseDot = new List<GameObject>(); /*!<array dari dot(titik) yang salah*/
    public Sprite[] AllImagePuzzle;/*!<seluruh sprite image puzzle huruf*/
    public GameObject PrefabsPuzzle;/*!<pemanggilan objrk perfab puzzle*/
    public GameObject Container;/*!<objek contaner puzzle*/

    public GameObject parentPuzzle;/*!<objek parent puzzle*/
    public GameObject AllImageContainer;/*!<objek container seluruh image*/

    void Start()
    {
        GenerateThumbnailPuzzle();
    }
    private void GenerateThumbnailPuzzle()
    {
        for (int i = 0; i < AllImagePuzzle.Length; i++)
        {
            GameObject temp = (GameObject)Instantiate(PrefabsPuzzle);
            temp.GetComponent<PuzzleImage>().InitThis(AllImagePuzzle[i]);
            temp.GetComponent<Transform>().SetParent(Container.GetComponent<Transform>());
            temp.GetComponent<Transform>().position = Vector3.zero;
            temp.GetComponent<Transform>().localScale = Vector3.one;
        }
    }

    /**
     * memunculkan seluruh dot yang benar(dot didalam huruf yang akan ditulis)
     * */
    public void CallTrueDot()
    {
        GameObject temp = (GameObject)Instantiate(TrueDotPrefabs);
        temp.transform.SetParent(parentPuzzle.transform);
        temp.transform.localPosition = new Vector3(Random.Range(0f,1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        temp.transform.localScale = Vector3.one;
        arrTrueDot.Add(temp);
    }

    /**
     * memunculkan seluruh dot yang salah(dot diluar huruf yang akan ditulis)
     * */
    public void CallFalseDot()
    {
        GameObject temp = (GameObject)Instantiate(FalseDotPrefabs);
        temp.transform.SetParent(parentPuzzle.transform);
        temp.transform.localPosition = new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        temp.transform.localScale = Vector3.one;
        arrFalseDot.Add(temp);
    }

    /**
     * dot yang sudah terklik/terpilih akan hancur dan akan ter-reset
     * */
    public void ResetAllDot()
    {
        int i;
        for(i=0;i<arrTrueDot.Count;i++)
        {
            Destroy(arrTrueDot[i]);
        }
        arrTrueDot = new List<GameObject>();

        for(i=0;i<arrFalseDot.Count;i++)
        {
            Destroy(arrFalseDot[i]);
        }
        arrFalseDot = new List<GameObject>();
    }
    /**
     * image container akan aktif satu dan yang lain di non-aktifkan
     * */
    public void ToggleImageContainer()
    {
       
        AllImageContainer.SetActive(!AllImageContainer.activeSelf);
    }
    


}
