using UnityEngine;
using UnityEngine.UI;
//! potongan puzzle controller
public class PiecePuzzleController : BaseRayButton {
	private Vector3 initPosition;
	private Vector3 thisLocalScale;
	private Transform thisTransform;
	private GameObject nestedObject;
	private PuzzleGameManager puzzleGameManager;
	public SpriteRenderer skin; /*!<pemanggilan script spriteRenderer*/

	public int thisIndex; /*!<index dari tiap potongan puzzle*/

	public Vector3 scaleTo; /*!<ukuran potongan puzzle*/

	void Awake()
	{
		
		thisTransform = GetComponent<Transform>();
		thisLocalScale = thisTransform.localScale;
		initPosition = transform.transform.position;
	}
	void Start()
	{
		puzzleGameManager = GameObject.FindGameObjectWithTag(HashTag.QUIZ_ENTITY).GetComponent<PuzzleGameManager> ();
	}

	void BackToInitPos()
	{
		thisTransform.position = initPosition;
		thisTransform.localScale = thisLocalScale;
	}
    /**
     * menentukan index dan sprite puzzle
     * */
	public void initPuzzle(int index,Sprite skin)
	{
		this.thisIndex = index;
		this.skin.sprite = skin;
		BackToInitPos ();
		nestedObject = null;
	}

	//----------------- FUNCTION CALLED BY EASY TOUCH QUICK ------------------ //
    /**
     * fungsi ketika drag potongan puzzle
     * */
	public void OnDrag()
	{
		if(nestedObject != null)
		{
			SingleContainerController containerPuzzle = nestedObject.GetComponent<SingleContainerController>();

			if(containerPuzzle != null)
				nestedObject.GetComponent<SingleContainerController>().NestedGameObject = null;
		}

		this.transform.localScale = scaleTo;

	}


    /**
     * fungsi ketika drop potongan puzzle.
     * jika benar akan terpasang.
     * jika salah akan kembali ke posisi semula.
     * */
	public void OnDrop(bool fromTouch)
	{

		if(fromTouch)
			return;

		nestedObject =  GetRayGO(Vector3.forward,1<<9);

		if(nestedObject == null)
		{
			//back to init pos
			BackToInitPos();
			return;
		}else{

			SingleContainerController puzzleContainer = nestedObject.GetComponent<SingleContainerController>();
			//nesting object
			if(puzzleContainer == null)
			{
				BackToInitPos();
			}else{
				if(puzzleContainer.NestedGameObject != null)
				{
					BackToInitPos();
				}else{
					thisTransform.position  = nestedObject.transform.position;
					thisTransform.localScale = nestedObject.transform.localScale;
					puzzleContainer.NestedGameObject = this.gameObject;
					puzzleGameManager.setArrBool(thisIndex, puzzleContainer.isFit (this.thisIndex));
				}
			}

		}

	}
}
