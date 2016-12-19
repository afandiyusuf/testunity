using UnityEngine;
using System.Collections;

//! camera mengikuti Player
public class FollowingCamera : MonoBehaviour {

	public Vector2 velocity; //!< acuan pergerakan kamera

    public float smoothX; //!< pergerakan kamera ke posisi X, semakin besar semakin berat
	public float smoothY; //!< pergerakan kamera ke posisi Y, semakin besar semakin berat

    public GameObject player; //!< objek player (target posisi camera)

    public float goX; //!< kamera menuju posisi X yang baru
	public float goY; //!< kamera menuju posisi Y yang baru
    public bool isInArea; //!< mengecek player masih dalam area atau tidak

    public float posX; //!< posisi X kamera saat di dalam area
    public float posY; //!< posisi Y kamera saat di dalam area

    public float shakeTimer; //!< waktu getaran/shake pada kamera
	public float shakeAmount; //!< jumlah getaran/shake pada kamera

	void FixedUpdate () 
	{
		
			goX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref velocity.x, smoothX);
			goY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref velocity.y, smoothY);

			transform.position = new Vector3(goX, goY, transform.position.z);

			if (isInArea == true){
				transform.position = new Vector3(Mathf.Clamp(transform.position.x,posX-36,posX+36),Mathf.Clamp(transform.position.y,posY-10,posY+10), transform.position.z);
			}else{
				transform.position = new Vector3(goX, goY, transform.position.z);
			}
		
		
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.L))
			ShakeCamera(0.1f, 1);

		if(shakeTimer >= 0)
		{
			Vector2 shakePos = Random.insideUnitCircle * shakeAmount;
			transform.position = new Vector3 (transform.position.x + shakePos.x, transform.position.y + shakePos.y, transform.position.z);
			shakeTimer -= Time.deltaTime;
		}
	}

    /** ketika kamera mengalami shake
 * kekuatan getaran
 * waktu atau durasi getaran
 * */
    public void ShakeCamera(float shakePwr, float shakeDur)
	{
		shakeAmount = shakePwr;
		shakeTimer = shakeDur;
	}

    /** pengecekan kamera dalam area
     * cek apakah kamera didalam kamera atau tidak
     * perubahan kamera berubah lebih smooth
     * */
	public void setPos(Vector3 t, bool area)
	{
		posX = t.x;
		posY = t.y;
		isInArea = area;
	}

}
