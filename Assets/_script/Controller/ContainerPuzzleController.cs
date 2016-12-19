using UnityEngine;
//! mengatur container puzzle
public class ContainerPuzzleController : MonoBehaviour {

	public SingleContainerController[] ArrPuzzleController;/*!<mengambil array dari script singleContainerController*/

	void Start()
	{
		//setIndex;
		for(int i=0;i<ArrPuzzleController.Length;i++)
		{
			ArrPuzzleController[i].ThisIndex = i;
		}
	}
    /**
     * reset array dalam script singleContainerController.
     * */
	public void ResetContainer()
	{
		for (int i = 0; i < ArrPuzzleController.Length; i++) {
			ArrPuzzleController [i].NestedGameObject = null;
		}
	}
}
