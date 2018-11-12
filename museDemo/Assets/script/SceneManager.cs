using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour {

    public static SceneManager Instance;

    public SoundControl soundControl;
    public ControlPanel controlPanel;
    public EffectControl effectControl;
    public ChordPanel chordPanel;

    //config
    public float totalTime = 10f; //46.8f;


    //sound
    public AudioClip bgmClip = null;
    public AudioClip readyClip = null;

    public AudioClip songClip = null;

    //input info
    public List<InputRef> inputRefList = new List<InputRef>();

    void Awake()
    {
        Instance = this;

        //init inputreflist
        inputRefList = Config.Instance.GetInputRef();
    }

	// Use this for initialization
	void Start () {
        totalTime = bgmClip.length;
	}
	
	// Update is called once per frame
	void Update () {
		
	}





}
