using SQLite4Unity3d;
//! table register
public class user_cred {

    [PrimaryKey, AutoIncrement]
    public int id { get; set; }/*!<nomor id*/
    public string username { get; set; }/*!<username login*/
    public string ttl { get; set; }/*!<tempat tanggal lahir*/
    public string jenis_kelamin { get; set; }/*!<jenis kelamin (L(laki-laki)/P(perempuan))*/
    public string alamat { get; set; }/*!<alamat/email*/
    public string password { get; set; }/*!<password login*/
    /**
     * format string table register dan login player
     * */
    public override string ToString()
    {
        return string.Format("[Person: id={0}, username={1},  password={2}, ttl={3}, jenis kelamin={4}, email={5}]", id, username,password,ttl,jenis_kelamin,alamat);
    }

}
