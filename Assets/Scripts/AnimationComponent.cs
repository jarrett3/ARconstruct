using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnimationComponent : MonoBehaviour {
    [System.Serializable]
    public struct AnimComponent {
        public Vector3 translation;
        public Vector3 rotation;
        public float speed;
    };
    
    public AnimComponent Anim;

    private List<bool> bIsRunning;
    private int maxSteps;
    private List<int> curStep;
    private List<Vector3> initPos;
    private List<Quaternion> initRot;

    private bool bHasInitialized = false;
    private int frame = 0;

	// Use this for initialization
	void Start () {
        if (!bHasInitialized)
            Awake();
    }

    void Awake() {
        if (bHasInitialized)
            return;

        bIsRunning = new List<bool>();
        curStep = new List<int>();
        initPos = new List<Vector3>();
        initRot = new List<Quaternion>();

        initPos.Clear();
        initRot.Clear();
        curStep.Clear();
        bIsRunning.Clear();

        for (int i = 0; i < transform.childCount; i++) {
            Transform child = transform.GetChild(i);
            initPos.Add(child.localPosition);
            initRot.Add(child.localRotation);

            maxSteps = (int)(200 / Anim.speed);
            curStep.Add(0);

            child.localPosition = Anim.translation + initPos[i];
            Vector3 rot = Anim.rotation + initRot[i].eulerAngles;
            child.localRotation = Quaternion.Euler(rot);
            bIsRunning.Add(true);
        }
        bHasInitialized = true;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        for (int i = 0; i < transform.childCount; i++) {
            if (!bIsRunning[i])
                continue;

            if (frame > 20 * i) {
                Transform child = transform.GetChild(i);
                child.Translate(-Anim.translation * 0.00041f * 0.005f * Anim.speed, transform);
                child.Rotate(Anim.rotation * 0.005f * Anim.speed);

                if (curStep[i] == maxSteps + 1)
                    StartCoroutine(PauseAtEnd(i));

                curStep[i]++;
            }
        }
        if(frame < transform.childCount*20)
            frame++;
    }

    public void StopAnimation() {
        for (int i = 0; i < transform.childCount; i++) {
            bIsRunning[i] = false;
            StopAllCoroutines();

            Transform child = transform.GetChild(i);
            child.localPosition = initPos[i];
            child.localRotation = initRot[i];
        }
    }

    public void StartAnimation() {
        StopAllCoroutines();
        for (int i = 0; i < transform.childCount; i++) {
            Transform child = transform.GetChild(i);
            child.localPosition = Anim.translation + initPos[i];
            Vector3 rot = Anim.rotation + initRot[i].eulerAngles;
            child.localRotation = Quaternion.Euler(rot);
            curStep[i] = 0;

            bIsRunning[i] = true;
        }
        frame = 0;
    }

    IEnumerator PauseAtEnd(int i) {
        bIsRunning[i] = false;
        yield return new WaitForSeconds(1.0f);
        
        if(i + 1 == transform.childCount) {
            for (int j = 0; j < transform.childCount; j++) {
                Transform child = transform.GetChild(j);
                child.localPosition = Anim.translation + initPos[j];
                Vector3 rot = Anim.rotation + initRot[j].eulerAngles;
                child.localRotation = Quaternion.Euler(rot);
                curStep[j] = 0;

                bIsRunning[j] = true;
            }
            frame = 0;
        }
    }
}
