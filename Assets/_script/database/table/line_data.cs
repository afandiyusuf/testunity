using SQLite4Unity3d;
//! table database line_data
public class line_data {

    [PrimaryKey, AutoIncrement]
    public int id { get; set; }/*!<nomor id*/
    public string position { get; set; }/*!<posisi line*/
    public int id_base_soal { get; set; }/*!<nomor id base soal*/
    public string state { get; set; }/*!<kondisi*/
    /**
     * format string pada table
     * */
    public override string ToString()
    {
        return string.Format("[Person: id={0}, position={1},  id_base_soal={2}, state={3}]", id, position,id_base_soal, state);
    }
}
