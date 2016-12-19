using UnityEngine;
using UnityEngine.UI;
//! image puzzle
public class PuzzleImage : MonoBehaviour {

    public Image thisFace;/*!<image dari puzzle*/
    public SpriteRenderer MasterPuzzle;/*!<pengaturan render sprite puzzle*/

    void Awake()
    {
        this.thisFace = this.GetComponent<Image>();
    }
    void Start()
    {
        MasterPuzzle = GameObject.FindGameObjectWithTag("MasterPuzzle").GetComponent<SpriteRenderer>();
    }
    /**
     * mengatur sprite image puzzle
     * */
    public void InitThis(Sprite setSprite)
    {
        thisFace.sprite = setSprite;
    }

    /**
     * mengatur sprite render dari image sprote puzzle
     * */
    public void SetMasterToThis()
    {
        MasterPuzzle.sprite = thisFace.sprite;
    }

}
