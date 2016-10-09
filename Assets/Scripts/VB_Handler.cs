/*============================================================================== 
 * Copyright (c) 2012-2015 Qualcomm Connected Experiences, Inc. All Rights Reserved. 
 * ==============================================================================*/
using UnityEngine;
using Vuforia;

public class VB_Handler : MonoBehaviour, IVirtualButtonEventHandler {
    public StepManager stepMan;


    public void OnButtonPressed(VirtualButtonAbstractBehaviour vb) {
        Debug.Log("OnButtonPressed");
        stepMan.Advance();
    }
    
    public void OnButtonReleased(VirtualButtonAbstractBehaviour vb) {
        Debug.Log("OnButtonReleased");
    }

    void Start() {
        // Register with the virtual buttons TrackableBehaviour
        VirtualButtonBehaviour vb =
                            GetComponentInChildren<VirtualButtonBehaviour>();
        if (vb)
            vb.RegisterEventHandler(this);
    }
}
