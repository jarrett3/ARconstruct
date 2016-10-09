using UnityEngine;
using System.Collections;
using Vuforia;
using System.Collections.Generic;

public class MonitorTracking : MonoBehaviour {
    public GameObject TrackedObject;
    private bool bIsFound;

	// Use this for initialization
	void Start () {
        bIsFound = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (bIsFound)
            return;

        // Get the Vuforia StateManager
        StateManager sm = TrackerManager.Instance.GetStateManager();

        // Query the StateManager to retrieve the list of
        // currently 'active' trackables 
        //(i.e. the ones currently being tracked by Vuforia)
        IEnumerable<TrackableBehaviour> activeTrackables = sm.GetActiveTrackableBehaviours();

        // Iterate through the list of active trackables
        foreach (TrackableBehaviour tb in activeTrackables)
            bIsFound = true;
        if (bIsFound)
            TrackedObject.transform.parent = null;
    }
}
