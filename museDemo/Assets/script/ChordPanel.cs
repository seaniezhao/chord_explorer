using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChordPanel : MonoBehaviour {

    public GameObject[] chordObjs;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void SetPanel(List<InputRef> ir , int offset)
    {
        if (chordObjs.Length + offset >= ir.Count)
            return;

        for (int i = 0; i < chordObjs.Length; ++i)
        {
            Text txt = chordObjs[i].GetComponentInChildren<Text>();
            print(i + offset);
            txt.text = ir[i + offset].key;
        }
    }
}
