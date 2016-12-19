using SQLite4Unity3d;
//! table database graphic
public class graphic_soal {

    [PrimaryKey, AutoIncrement]
    public int id { get; set; }/*!<nomor id*/
    public string role { get; set; }/*!<aturan*/
    public string sprite_name { get; set; }/*!<nama sprite soal*/
    public int id_base_soal { get; set; }/*!<nomor id base soal*/
    /**
     * format string pada table
     * */
    public override string ToString()
    {
        return string.Format("[Person: id={0}, role={1},  sprite_name={2} , id_base_soal={3}]", id, role, sprite_name,id_base_soal);
    }
}
