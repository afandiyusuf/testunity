using UnityEngine;
using UnityEngine.UI;
//! bar meter player dan musuh (menentukan menang atau kalah)
public class BarController : MonoBehaviour {
	public float TotalWidth; /*!<total lebar bar*/
	public RectTransform PanelBar;/*!<panel bar musuh*/
	public RectTransform GoodBar;/*!<bar player*/
	public RectTransform EvilBar;/*!<bar musuh*/
	public RectTransform EvilContent;/*!<image bar musuh*/
	public RectTransform SeparatorObject;/*!<objek pemisah bar player dan bar musuh*/

	void Start()
	{
		TotalWidth = PanelBar.rect.width;

	}
    /**
     * pergesaran bar player dan bar musuh
     * */
	public void ChangeValBar(float val)
	{
		
		GoodBar.sizeDelta = new Vector2(val * TotalWidth,GoodBar.rect.height);
		EvilBar.sizeDelta = new Vector2((1 - val) * TotalWidth,GoodBar.rect.height);
		SeparatorObject.sizeDelta = GoodBar.sizeDelta;
	}
}
