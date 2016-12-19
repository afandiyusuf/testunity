using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaManager : MonoBehaviour {

    public class PortalsManager
    {
        public Transform WarpLoc;
        public Transform Map;
        public Transform CameraStart;
    }

    
    public List<Transform> ArrWarpLoc; //!< ArrWarpLoc,  variable array dari portals manager yang isinya warploc. digunakan agar dapat didisplay ke inspector.
    public List<Transform> ArrMap; //!< ArrWarpLoc,  variable array dari portals manager yang isinya Map. digunakan agar dapat didisplay ke inspector.
    public List<Transform> ArrCameraStart; //!< ArrWarpLoc,  variable array dari portals manager yang isinya CameraStart. digunakan agar dapat didisplay ke inspector.

    public List<PortalsManager> ArrPortalManager; //!< ArrPortalManager,  variable array dari portals manager yang component component yang digunakan untuk fungsionalitas portal, variable ini tidak nampak di inspector, oleh karena itu dibuat variable duplikasi. ArrMap, ArrWarpLoc, ArrCameraStart.

    private void Awake()
    {
        ArrPortalManager = new List<PortalsManager>();
        ArrWarpLoc = new List<Transform>();
        ArrMap = new List<Transform>();
        ArrCameraStart = new List<Transform>();
        //Get PortalInstance
        FindPortalAtChild(1);
    }

    //Recursive function untuk mencari Portal
    void FindPortalAtChild(int index)
    {
        //mulai pencarian portal+index (portal1, portal2, dst)
        Transform tempPortalTransform = transform.FindChild("portal" + index);
        //jika portal ditemukan
        if (tempPortalTransform != null)
        {
            //GetWarpLoc
            PortalsManager tempPortalManager = new PortalsManager();

            //get warploc, nama gameobjectnya formsite
            Transform tempWarpLoc = tempPortalTransform.FindChild("fromsite").gameObject.transform;
            if (tempWarpLoc != null)
            {
                tempPortalManager.WarpLoc = tempWarpLoc;
                ArrWarpLoc.Add(tempWarpLoc);
            }
                

            //get camerastart, parent dari portal dengan nama camera start
            Transform cameraStart =  tempPortalTransform.transform.parent.FindChild("cameraStart" + index);
            if (cameraStart != null)
            {
                ArrCameraStart.Add(cameraStart);
                tempPortalManager.CameraStart = cameraStart;
            }


            //get map, parent dari peta
            tempPortalManager.Map = tempPortalTransform.transform.parent;
            ArrMap.Add(tempPortalTransform.transform.parent);

            //add portal manager ke array
            ArrPortalManager.Add(tempPortalManager);

            //lanjutkan pencarian
            FindPortalAtChild(++index);
        }
        else
        {
            //selesaikan operasi jika pencarian gagal;
            return;
        }
    }
}
