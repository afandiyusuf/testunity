using UnityEngine;
//! -(belum paham)-
public abstract class BaseRayButton : MonoBehaviour {

	protected bool isDebug = DebugClass.IS_DEBUG;
	protected float curretTimeScale;
	protected float waitTime;

    // Update is called once per frame
    /**
     * text
     * */
    public void RayTo(Vector3 directions,string expectGameObjectName, int layerMask = 999)
	{
		Vector3 fwd = gameObject.transform.TransformDirection(directions);
		if(isDebug)
			Debug.DrawRay(gameObject.transform.position, fwd * 1000, Color.red);
		
		RaycastHit objectHit;
		bool isRay;
		if(layerMask == 999){
			isRay = Physics.Raycast (gameObject.transform.position, fwd, out objectHit, 1000);
		}else{
			isRay = Physics.Raycast (gameObject.transform.position, fwd, out objectHit, 1000,layerMask);
		}
		if (isRay) {

			//Debug.Log (objectHit.collider.gameObject.name);
			//do something if hit object ie
			if (objectHit.collider.gameObject.name == expectGameObjectName) {
				curretTimeScale += Time.deltaTime;

				if(curretTimeScale > waitTime){
					curretTimeScale = 0;
					onHitExpected();
				}
			} else {
				curretTimeScale = 0;
				onHitAnother();
			}
		} else {
			curretTimeScale = 0;
			onLostRay();
		}
	}
    /**
     * text
     * */
    public Vector3 GetRayCoordinat(Vector3 direction, int layerMask)
	{
		RaycastHit hit;
		if(Physics.Raycast(Camera.main.transform.position, direction, out hit, 1000,layerMask)) {
			return hit.point;
			//Do something with the local coordinates
		}else{
			return Vector3.zero;
		}
	}

    /**
     * text
     * */
    public string GetRayName(Vector3 directions,int layerMask = 999)
	{
		Vector3 fwd = gameObject.transform.TransformDirection(directions);
		if(isDebug)
			Debug.DrawRay(gameObject.transform.position, fwd * 1000, Color.red);
		
		RaycastHit objectHit;
		bool isRay;
		if(layerMask == 999){
			isRay = Physics.Raycast (gameObject.transform.position, fwd, out objectHit, 1000);
		}else{
			isRay = Physics.Raycast (gameObject.transform.position, fwd, out objectHit, 1000,layerMask);
		}
		if (isRay) {
			return objectHit.collider.gameObject.name;
		} else {
			return "NONAME";
		}
	}
    /**
     * text
     * */
    public GameObject GetRayGO(Vector3 directions,int layerMask = 999)
	{

		Vector3 fwd = gameObject.transform.TransformDirection(directions);
		if(isDebug)
			Debug.DrawRay(gameObject.transform.position, fwd * 1000, Color.red);
		
		RaycastHit objectHit;
		bool isRay;
		if(layerMask == 999){
			isRay = Physics.Raycast (gameObject.transform.position, fwd, out objectHit, 1000);
		}else{
			isRay = Physics.Raycast (gameObject.transform.position, fwd, out objectHit, 1000,layerMask);
		}
		if (isRay) {
			return objectHit.collider.gameObject;

		}
		return null;
	}
    /**
     * text
     * */
    public virtual void onHitExpected()
	{
		return;
	}
    /**
     * text
     * */
    public virtual void onHitAnother()
	{
		return;
	}
    /**
     * text
     * */
    public virtual void onLostRay()
	{
		return;
	}
}
