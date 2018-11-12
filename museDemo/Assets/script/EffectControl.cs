using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectControl : MonoBehaviour {

    public GameObject maskGo;

    private Image mask;
    private bool effectStarted = true;

    private float dps;
    private int scanCount;

    private int effectLoopCount = 0;
    // Use this for initialization
    void Start () {

        SceneManager.Instance.chordPanel.SetPanel(SceneManager.Instance.inputRefList, 0);
    }
	
	// Update is called once per frame
	//void Update () {
 //       if (effectStarted)
 //       {
 //           Vector3 scale = maskGo.transform.localScale;
 //           scale.x += dps*Time.deltaTime;
 //           maskGo.transform.localScale = scale;
 //       }
	//}

    void FixedUpdate()
    {
        if (effectStarted)
        {
            Vector3 scale = maskGo.transform.localScale;
            scale.x += dps * Time.fixedDeltaTime;
            maskGo.transform.localScale = scale;

            if (maskGo.transform.localScale.x > 1)
            {
                maskGo.transform.localScale = new Vector3(0, 1, 1);
                effectLoopCount++;

                SceneManager.Instance.chordPanel.SetPanel(SceneManager.Instance.inputRefList, effectLoopCount*4-1);
                //展示逻辑
            }
        }
    }


    public void StartEffect()
    {
        effectStarted = true;
        maskGo.transform.localScale = new  Vector3(0, 1, 1);

        int numChord = SceneManager.Instance.inputRefList.Count;
        scanCount = numChord / 4;
        dps = scanCount / SceneManager.Instance.totalTime;

    }

    public void StopEffect()
    {

        effectStarted = false;
    }

    public void ResetEffect()
    {
        maskGo.transform.localScale = new Vector3(0, 1, 1);

        SceneManager.Instance.chordPanel.SetPanel(SceneManager.Instance.inputRefList, 0);
        effectLoopCount = 0;
    }

}
