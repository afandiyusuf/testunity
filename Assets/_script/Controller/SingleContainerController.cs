using UnityEngine;
//! container puzzle controller
public class SingleContainerController : MonoBehaviour {

	private int thisIndex;
	private GameObject nestedGameObject;
    /**
     * get dan set integer index
     * */
	public int ThisIndex{

		get{
			return this.thisIndex;
		}

		set{

			this.thisIndex = value;

		}
	}
    /**
     * get dan set objek
     * */
	public GameObject NestedGameObject{
		get{
			return this.nestedGameObject;
		}
		set{

			this.nestedGameObject = value;

		}
	}
    /**
     * jika puzzle cocok maka index akan sama dengan index puzzle
     * */
	public bool isFit(int puzzleIndex)
	{
		return (this.thisIndex == puzzleIndex);
	}


}
