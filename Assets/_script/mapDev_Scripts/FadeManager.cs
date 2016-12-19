using UnityEngine;
using UnityEngine.UI;
using System.Collections;
//! animasi fade
public class FadeManager : MonoBehaviour {
	public static FadeManager Instance{set;get;}/*!<save dan load script*/
	public GameObject player;/*!<objek karakter(player)*/

    public Image fadeImage;/*!<image animasi fade*/
    private bool isInTransition;
	private float transition;
	private bool isShowing;
	private float  duration;

	private void Awake()
	{
		Instance = this;
	}
    /**
     * munculnya animasi fade dan durasi pemutarannya
     * */
	public void Fade(bool showing, float duration)
	{
		isShowing = showing;
		isInTransition = true;
		this.duration = duration;
		transition = (isShowing) ? 0 : 1;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.M))
			Fade(true, 1);
		
		if (Input.GetKeyDown(KeyCode.N))
			Fade(false, 1);

		if (!isInTransition)
			return;

		if (isShowing == false)
			fadeImage.gameObject.SetActive(false);
		else 
			fadeImage.gameObject.SetActive(true);
			
			
		transition += (isShowing) ? Time.deltaTime * (1/duration) : -Time.deltaTime * (1/duration);
		fadeImage.color = Color.Lerp(new Color(0.12f,0.12f,0.12f,0), new Color(0.12f,0.12f,0.12f,1), transition);

		if (transition > 1 || transition < 0)
			isInTransition = false;
	}

}
