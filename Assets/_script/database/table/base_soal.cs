using SQLite4Unity3d;
//! table databese soal
public class base_soal  {

    [PrimaryKey, AutoIncrement]
    public int id { get; set; }/*!<nomor id*/
    public string nama { get; set; }/*!<nama soal*/
    public string last_update { get; set; }/*!<tanggal update terakhir*/
    /**
     * format string pada table
     * */
    public override string ToString()
    {
        return string.Format("[Person: id={0}, nama={1},  last_update={2}]", id, nama, last_update);
    }
}
