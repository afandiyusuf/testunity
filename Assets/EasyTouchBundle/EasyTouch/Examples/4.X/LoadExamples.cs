using UnityEngine;

public class LoadExamples : MonoBehaviour {

	public void LoadExample(string level){
#if UNITY_5_3
#else
		Application.LoadLevel( level );
#endif
	}
}
