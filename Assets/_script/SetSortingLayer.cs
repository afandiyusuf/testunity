using UnityEngine;
//! pengaturan layer
public class SetSortingLayer : BaseRayButton {
	public int sortingLayer;/*!<angka layer*/
	
	void Start()
	{
		
		Debug.Log (gameObject.layer);
	}
}
