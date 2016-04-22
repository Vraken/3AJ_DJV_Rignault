using UnityEngine;
using System.Collections;

public class SCRIPT_HUD : MonoBehaviour {
    
    void OnGui()
    {
        GUI.Box(new Rect(10, 10, 100, 90), "Loader Menu");
    }
}
