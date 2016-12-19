using SQLite4Unity3d;
using UnityEngine;
using System;

#if !UNITY_EDITOR
using System.Collections;
using System.IO;
#endif
using System.Collections.Generic;
//! data service (database)
public class DataService
{

    private SQLiteConnection _connection;

    public DataService(string DatabaseName)/*!<nama dari database dan letak dari filenya*/
    {

#if UNITY_EDITOR
        var dbPath = string.Format(@"Assets/StreamingAssets/{0}", DatabaseName);
#else
        // check if file exists in Application.persistentDataPath
        var filepath = string.Format("{0}/{1}", Application.persistentDataPath, DatabaseName);

        if (!File.Exists(filepath))
        {
            Debug.Log("Database not in Persistent path");
            // if it doesn't ->
            // open StreamingAssets directory and load the db ->

#if UNITY_ANDROID
            var loadDb = new WWW("jar:file://" + Application.dataPath + "!/assets/" + DatabaseName);  // this is the path to your StreamingAssets in android
            while (!loadDb.isDone) { }  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check
            // then save to Application.persistentDataPath
            File.WriteAllBytes(filepath, loadDb.bytes);
#elif UNITY_IOS
                 var loadDb = Application.dataPath + "/Raw/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
                // then save to Application.persistentDataPath
                File.Copy(loadDb, filepath);
#elif UNITY_WP8
                var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
                // then save to Application.persistentDataPath
                File.Copy(loadDb, filepath);

#elif UNITY_WINRT
		var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
		// then save to Application.persistentDataPath
		File.Copy(loadDb, filepath);
#else
	var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
	// then save to Application.persistentDataPath
	File.Copy(loadDb, filepath);

#endif

            Debug.Log("Database written");
        }

        var dbPath = filepath;
#endif
        _connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
        Debug.Log("Final PATH: " + dbPath);

    }
    


    /*USER AREA*/
    /**
     * membuat tabel di database sesuai data yang ada
     * */
    public void create_user(string _username,string _password,string _ttl,string _jenis_kelamin,string _alamat)
    {
        var temp = new user_cred
        {
            username = _username,
            password = _password,
            ttl = _ttl,
            jenis_kelamin = _jenis_kelamin,
            alamat = _alamat
        };

        _connection.Insert(temp);
    }

    /**
    * will return 1 for success dan return 0 for login error
    **/
    public int Login(string _username,string _password)
    {
        int temp;
        temp =  _connection.Table<user_cred>().Where(x => x.username == _username && x.password == _password).Count();
        return temp;
    }
    /**
     * cek username yang digunakan sudah ada atau belum
     * */
    public int CheckUsername(string _username)
    {
        int temp;
        temp = _connection.Table<user_cred>().Where(x=> x.username == _username).Count();
        return temp;
    }
    /* END USER AREA */

        /**
         * insert soal yang baru
         * */
    public base_soal insert_new_soal(string nama_soal)
    {
        var b = new base_soal
        {

            nama = nama_soal,
            last_update = DateTime.UtcNow.ToString()
        };
        _connection.Insert(b);
        return b;
    }
    /**
     * delet soal berdaasrkan id dari line_data dalam kumpulan line_datas
     * */
    public bool delete_soal_by_id(int id)
    {
        _connection.Delete<base_soal>(id);
        IEnumerable<line_data> line_datas = GetAllLineByIdSoal(id);

        foreach (var line_data in line_datas)
        {
            _connection.Delete<base_soal>(line_data.id);
        }
        IEnumerable<graphic_soal> graphic_soals = GetGraphicByIdSoal(id);
        foreach (var graphic in graphic_soals)
        {
            _connection.Delete<graphic_soal>(graphic.id);
        }

        return true;
    }
    /**
     * menentukan isi dari line_data
     * */
    public line_data insert_new_line_data(Vector3 local_pos,int _id_base_soal,bool state)
    {
        string PosString = local_pos.x.ToString() + "," + local_pos.y.ToString() + "," + local_pos.z.ToString();
        string stateString = (state == true) ? "true" : "false";
        var a = new line_data
        {
            position = PosString,
            id_base_soal = _id_base_soal,
            state = stateString
        };
        _connection.Insert(a);
        return a;
    }
    /**
     * menentukan isi dari graphic_soal
     * */
    public graphic_soal insert_graphic_soal(string _role,string _sprite_name,int _id_base_soal)
    {
        var a = new graphic_soal
        {
            role = _role,
            sprite_name = _sprite_name,
            id_base_soal = _id_base_soal
        };
        _connection.Insert(a);
        return a;
    }
    /**
     * table database tentang soal
     * */
    public IEnumerable<base_soal> GetAllSoal()
    {
        return _connection.Table<base_soal>();
    }
    /**
 * table database tentang line_data
 * */
    public IEnumerable<line_data> GetAllLine()
    {
        return _connection.Table<line_data>();
    }
    /**
 * table database tentang graphic
 * */
    public IEnumerable<graphic_soal> GetAllGraphic()
    {
        return _connection.Table<graphic_soal>();
    }
    /**
 * table database tentang graphic berdasarkan id soal
 * */
    public IEnumerable<graphic_soal>GetGraphicByIdSoal(int id)
    {
        return _connection.Table<graphic_soal>().Where(x => x.id_base_soal == id);
    }
    /**
 * table database tentang line_data dari seluruh line_datas
 * */
    public IEnumerable<line_data>GetAllLineData()
    {
        return _connection.Table<line_data>();
    }
    /**
 * table database tentang line_data berdasarkan id soal
 * */
    public IEnumerable<line_data>GetAllLineByIdSoal(int id)
    {
        return _connection.Table<line_data>().Where(x => x.id_base_soal == id);
    }
}