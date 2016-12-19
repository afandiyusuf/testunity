using UnityEngine;
//! pengaturan avatar
public class MasterAvatarController : MonoBehaviour {

    public SpriteAvatarSelector[] RambutLaki; /*!<mengambil sprite rambut laki-laki dari array scrip SpriteAvatarSelector*/
    public SpriteAvatarSelector[] RambutPerempuan; /*!<mengambil sprite rambut perempuan dari array scrip SpriteAvatarSelector*/

    public Sprite[] ThumbnailRambutLaki; /*!<array rambut laki-laki*/
    public Sprite[] ThumbnailRambutPerempuan; /*!<array rambut perempuan*/
    public MasterButtonState ButtonRambut; /*!<pemanggilan script MasterButtonState*/

    public GameObject MasterRambutPerempuan; /*!<kumpulan rambut perempuan*/

    public SpriteAvatarSelector Muka; /*!<mengambil sprite wajah dari scrip SpriteAvatarSelector*/
    public SpriteAvatarSelector Kepala; /*!<mengambil sprite kepala dari scrip SpriteAvatarSelector*/

    public SpriteAvatarSelector BadanLaki; /*!<mengambil sprite badan/baju laki-laki dari scrip SpriteAvatarSelector*/
    public SpriteAvatarSelector BadanPerempuan; /*!<mengambil sprite badan/naju perempuan dari scrip SpriteAvatarSelector*/

    public bool isLaki = true; /*!<pengecekan jenis kelamin avatar: bila false perempuan dan bila true laki-laki*/

    /**
     * diawal program berjalan set default jenis kelamin avatar ke perempuan.
     * */
    public void Start()
    {
        SetPerempuan();
    }

    /**
     * pergantian rambut ke pilihan selanjutnya.
     * */
    public void NextRambut()
    {
        
        for (int i = 0; i < RambutLaki.Length; i++)
        {
            RambutLaki[i].Next();
            RambutPerempuan[i].Next();
        }

        MasterRambutPerempuan.SetActive(!isLaki);
    }
    /**
     * pergantian rambut ke pilihan sebelumnya.
     * */
    public void PrevRambut()
    {
        for (int i = 0; i < RambutLaki.Length; i++)
        {
            RambutLaki[i].Prev();
            RambutPerempuan[i].Prev();
        }
        
        MasterRambutPerempuan.SetActive(!isLaki);
    }

    /** pergantian jenis kelamin.
* jika laki-laki munculkan laki-laki.
* jika perempuan munculkan perempuan
**/
    public void ToggleKelamin()
    {
        isLaki = !isLaki;
        BadanLaki.gameObject.SetActive(isLaki);
        BadanPerempuan.gameObject.SetActive(!isLaki);
        MasterRambutPerempuan.SetActive(!isLaki);

    }

    /**
* jika jenis kelamin laki-laki maka tampilan berubah menjadi avatar laki-laki
* */
    public void SetLaki()
    {
        ButtonRambut.Thumbnails = ThumbnailRambutLaki;
        SetGender(true);
        
    }
    /**
* jika jenis kelamin perempuan maka tampilan berubah menjadi avatar perempuan
* */
    public void SetPerempuan()
    {
        ButtonRambut.Thumbnails = ThumbnailRambutPerempuan;
        SetGender(false);
       
    }
    /**
* pergantian jenis kelamin.
* jika laki-laki munculkan laki-laki.
* jika perempuan munculkan perempuan
* */
    public void SetGender(bool isLaki)
    {
        this.isLaki = isLaki;
        BadanLaki.gameObject.SetActive(isLaki);
        BadanPerempuan.gameObject.SetActive(!isLaki);
        MasterRambutPerempuan.SetActive(!isLaki);
        ButtonRambut.SetAllSpriteButton();
    }
}
