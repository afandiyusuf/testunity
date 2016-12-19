using UnityEngine;
using System.Collections;

//! portal menuju pintu harus menekan tombol
public class SpecialWarpControl : MonoBehaviour {

	public Transform specialWarpLoc; //!< posisi karakter ketika pindah ruangan
	public Transform map; //!< area atau map baru yang dituju player
	private ActionButton actButton;

	public Vector3 cameraWarp; //!< posisi kamera saat player pindah/teleport
	public bool needEnlarge; //!< menentukan destinasi portal

    public AreaManager DestinationArea;
    public int index;
    private int cameraZ = -40;
	// Use this for initialization
	void Start () {

        AreaManager.PortalsManager dest = DestinationArea.ArrPortalManager[index - 1];

        //cameraWarp = new Vector3(map.position.x, map.position.y, cameraZ);
        cameraWarp = new Vector3(dest.Map.position.x, dest.Map.position.y, cameraZ);
		actButton = GameObject.FindGameObjectWithTag("actionButton").GetComponent<ActionButton>();
	}

	void OnTriggerEnter2D (Collider2D player)
	{
		if(player.gameObject.tag == "Player")
		{
            actButton.setDest(specialWarpLoc, cameraWarp, needEnlarge);
		}
	}


}