using UnityEngine;
//! penampilan avatar yang diinginkan
public class SpriteAvatarSelector : MonoBehaviour {

    private SpriteRenderer thisSprite;
    private int CurrentIndex = 0;
    public Sprite[] PilihanSprite; /*!<bagian tubuh yang dipilih player dalam membuat avatarnya sendiri*/
    
    void Awake()
    {
        thisSprite = gameObject.GetComponent<SpriteRenderer>();
    }
    /**
     * pemilihan bentuk avatar berdasarkan indeks yang ada lalu bergeser ke selanjutnya
     * */
    public void Next()
    {
        CurrentIndex++;
        if (CurrentIndex > PilihanSprite.Length - 1)
        {
            CurrentIndex = 0;
        }
        ChangeSprite(CurrentIndex);
    }
    /**
 * pemilihan bentuk avatar berdasarkan indeks yang ada lalu bergeser ke sebelumnya
 * */
    public void Prev()
    {
        CurrentIndex--;
        if (CurrentIndex < 0)
            CurrentIndex = PilihanSprite.Length-1;

        ChangeSprite(CurrentIndex);
    }

    /**
 * pemilihan bentuk avatar berdasarkan indeks yang ada.
 * */
    public void SelectByIndex(int index)
    {
        CurrentIndex = index;
      
        ChangeSprite(CurrentIndex);
    }

    void ChangeSprite(int index)
    {
        thisSprite.sprite = PilihanSprite[index];
    }

    /**
 * mencari dan mengambil indeks yang ada
 * */
    public int GetIndex()
    {
        return CurrentIndex;
    }


}
