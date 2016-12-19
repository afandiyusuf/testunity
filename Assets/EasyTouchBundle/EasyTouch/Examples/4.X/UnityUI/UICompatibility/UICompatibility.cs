using UnityEngine;
using HedgehogTeam.EasyTouch;

public class UICompatibility : MonoBehaviour {

	public void SetCompatibility(bool value){
		EasyTouch.SetUICompatibily( value);
	}
}
