using UnityEngine;
//! kumpulan sprite
public class SpriteBank : MonoBehaviour {

	[System.Serializable]
    //! kumpulan puzzle
	public class PuzzleBank
	{
		public string name; /*!<string nama puzzle*/
		public Sprite DoneSprite; /*!<sprite puzzle solve/selesai*/
		public Sprite[] Piece; /*!<array potongan puzzle*/
	}

	public PuzzleBank[] puzzle; /*!<kumpulan soal puzzle*/
    /**
     * mengambil puzzle sesuai index yang ada
     * */
	public PuzzleBank getPuzzle(int index)
	{
		return puzzle [index];
	}
}
