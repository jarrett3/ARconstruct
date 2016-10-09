using UnityEngine;
using System.Collections;

public class StepManager : MonoBehaviour {
    public GameObject[] ItemList;
    public GameObject baseObj;
    public UnityEngine.UI.Button NextButton;
    public UnityEngine.UI.Button PrevButton;

    private int curIndex = 0;

	// Use this for initialization
	void Start () {
        PrevButton.gameObject.SetActive(false);
        foreach (GameObject obj in ItemList) {
            obj.SetActive(false);
        }
        ItemList[curIndex].SetActive(true);
        foreach (MeshRenderer renderer in baseObj.GetComponentsInChildren<MeshRenderer>()) {
            SetTransparent(renderer.material, true);
            renderer.material.color *= 0.85f;
        }
        
        foreach (MeshRenderer renderer in ItemList[curIndex].GetComponentsInChildren<MeshRenderer>())
            SetTransparent(renderer.material, false);
    }
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyDown(KeyCode.Space)) {
            Advance();
        }
	}

    public void Advance() {
        ItemList[curIndex].GetComponent<AnimationComponent>().StopAnimation();
        foreach (MeshRenderer renderer in ItemList[curIndex].GetComponentsInChildren<MeshRenderer>()) {
            SetTransparent(renderer.material, true);
            renderer.material.color *= 0.85f;
        }

        if (curIndex + 1 >= ItemList.Length) {

        }
        else {
            PrevButton.gameObject.SetActive(true);
            ItemList[++curIndex].SetActive(true);
            ItemList[curIndex].GetComponent<AnimationComponent>().StartAnimation();
            foreach (MeshRenderer renderer in ItemList[curIndex].GetComponentsInChildren<MeshRenderer>())
                SetTransparent(renderer.material, false);

            if (curIndex + 1 == ItemList.Length)
                NextButton.gameObject.SetActive(false);
        }
    }

    public void Previous() {
        ItemList[curIndex].GetComponent<AnimationComponent>().StopAnimation();
        foreach (MeshRenderer renderer in ItemList[curIndex].GetComponentsInChildren<MeshRenderer>())
            SetTransparent(renderer.material, false);
        ItemList[curIndex].SetActive(false);

        if (curIndex <= 0) {

        }
        else {
            NextButton.gameObject.SetActive(true);
            ItemList[--curIndex].SetActive(true);
            ItemList[curIndex].GetComponent<AnimationComponent>().StartAnimation();
            foreach (MeshRenderer renderer in ItemList[curIndex].GetComponentsInChildren<MeshRenderer>()) {
                SetTransparent(renderer.material, false);
                renderer.material.color /= .85f;
            }

            if (curIndex == 0)
                PrevButton.gameObject.SetActive(false);
        }
    }

    private void SetTransparent(Material mat, bool bTransparent) {
        if (bTransparent) {
            mat.SetFloat("_Mode", 2);
            mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            mat.SetInt("_ZWrite", 0);
            mat.DisableKeyword("_ALPHATEST_ON");
            mat.EnableKeyword("_ALPHABLEND_ON");
            mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            mat.renderQueue = 3000;
        }
        else {
            mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
            mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
            mat.SetInt("_ZWrite", 1);
            mat.DisableKeyword("_ALPHATEST_ON");
            mat.DisableKeyword("_ALPHABLEND_ON");
            mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            mat.renderQueue = -1;
        }
    }
}
