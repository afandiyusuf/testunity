using UnityEngine;
using System.Collections;

//! portal antar ruangan tidak perlu menekan tombol
public class WarpControl : MonoBehaviour
{
    private Camera cam;
    public bool isInArea; //!< didalam area portal
                          //public Vector3 pos;

    public Transform warpLoc; //!< lokasi perpindahan
    public Transform map; //!< area atau map perpindahan
    public Transform camerastart; //!< posisi start kamera

    //public Vector3 playerpos;
    public Vector3 cameraWarp; //!< posisi perpindahan kamera
    public Vector3 camStartPos; //!< posisi kamera sebelum perpindahan

    public AreaManager DestinationArea;
    public int index;

    private int cameraZ = -40;
    void Start()
    {
        cam = Camera.FindObjectOfType<Camera>();
        AreaManager.PortalsManager dest = DestinationArea.ArrPortalManager[index - 1];
        warpLoc = dest.WarpLoc;
        map = dest.Map;
        camerastart = dest.CameraStart;

        cameraWarp = new Vector3(dest.WarpLoc.position.x, dest.WarpLoc.position.y, cameraZ);
        camStartPos = new Vector3(camerastart.position.x, camerastart.position.y, cameraZ);
    }

    void OnCollisionEnter2D(Collision2D player)
    {
        if (player.gameObject.tag == "Player")
        {
            isInArea = true;


            cam.GetComponent<FollowingCamera>().setPos(map.position, isInArea);
        }
    }

}
