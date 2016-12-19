using UnityEngine;
//! interface dari model database
public interface IDBModel {
    void insert_user(string _username,string _password,string _ttl,string _jenis_kelamin,string _alamat);
	int Login(string _username, string _password);
    int CheckUsername(string _username);
}
