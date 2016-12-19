using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

//!  script database. 
/*!
  database untuk menyimpan data pemain yang sudah register sebelumnya.
*/
public class S_DataModel : MonoBehaviour,IDBModel
{
    public static S_DataModel instance; /*!< menandai gameObject ini sebagai gameObject utama */
    //public Text DebugText;
    private DataService ds;

    //!  fungsi awal program aktif. 
    /*!
      object ini akan terus ada sampai scene manapun dan akan menghapus object dengan nama dan fungsi yang sama.
    */
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);

        initDb();

    }

    //!  save database. 
    /*!
      bila ada perubahan pada data base akan langsung di save pada database yang sudah ada.
    */
    void initDb()
    {        
        ds = new DataService("api.db");
    }

    //!  membuat akun baru. 
    /*!
      bila player membuat akun baru maka akan langsung disimpan di database.
    */
    public void insert_user(string _username,string _password,string _ttl,string _jenis_kelamin,string _alamat)
    {
        ds.create_user(_username, _password, _ttl, _jenis_kelamin, _alamat);
    }

    //!  cek username. 
    /*!
      pengecekan username yang tersedia dan bila ada yang memasukkan username yang sama dengan player lain maka player baru wajib mengganti usernamenya.
    */
    public int CheckUsername(string _username)
    {
       return ds.CheckUsername(_username);
    }

    //!  login. 
    /*!
      ketika login maka akan di cek apakah username dan password sudah sesuai.
    */
    public int Login(string _username, string _password)
    {
        // 1 for success and 0 for error
        return ds.Login(_username, _password);
   }
}