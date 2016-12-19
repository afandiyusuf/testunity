using UnityEngine;
//! show or hiden screen/canvas
public abstract class ABaseSubScreen : MonoBehaviour{
    abstract public void ShowScreen(); /*!<memunculkan screen*/
    abstract public void HideScreen(); /*!<menyembunyikan screen*/
}
