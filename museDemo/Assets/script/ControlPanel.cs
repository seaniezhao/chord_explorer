using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlPanel : MonoBehaviour {

    public GameObject playGo;
    public GameObject delGo;
    public GameObject makeGo;

    private AudioSource myAs;
    private AudioSource songAs;

    private bool isPlaying = false;
    private bool disableButton = false;

    private bool isPlayingSong = false;

    public float startTime;

    public float playDur;
	// Use this for initialization
	void Start () {
        myAs = gameObject.AddComponent<AudioSource>();
        songAs = gameObject.AddComponent<AudioSource>();

        initState();
    }
	
	// Update is called once per frame
	void Update () {
        if (isPlaying)
        {
            if (Time.time - startTime >= playDur)
            {
                StopPlay();
                SwitchBtn();
            }
        }
		
	}

    public void OnMakeBtn()
    {
        if (!SceneManager.Instance.soundControl.HasNotes())
            return;

        if (!isPlayingSong)
        {
            isPlayingSong = true;
            disableButton = true;
            songAs.clip = SceneManager.Instance.songClip;
            songAs.Play();

            Invoke("StartReplay", 48);

            Color color = Color.red;

            ColorBlock cb = new ColorBlock();
            cb.normalColor = color;
            cb.highlightedColor = color;
            cb.pressedColor = Color.blue;
            cb.disabledColor = Color.black;
            cb.colorMultiplier = 1f;
            makeGo.GetComponent<Button>().colors = cb;
        }
        else
        {
            isPlayingSong = false;
            disableButton = false;
            CancelInvoke("StartReplay");
            songAs.Stop();
            if (isPlaying)
                StopPlay();

            Color color = Color.green;

            ColorBlock cb = new ColorBlock();
            cb.normalColor = color;
            cb.highlightedColor = color;
            cb.pressedColor = Color.blue;
            cb.disabledColor = Color.black;
            cb.colorMultiplier = 1f;
            makeGo.GetComponent<Button>().colors = cb;
        }
    }

    public void OnDelBtn()
    {
        if (!isPlaying&&!isPlayingSong)
        {
            SceneManager.Instance.soundControl.DeleteAllNotes();
        }
            
    }

    public void OnPlayBtn()
    {
        if (disableButton)
            return;

        SwitchPlay();


    }

    public void SwitchPlay()
    {
        if (isPlaying)
        {

            StopPlay();
           
        }
        else {
            if (SceneManager.Instance.soundControl.HasNotes())
                StartReplay();
            else
                StartCoroutine(StartPlay());
      
        }

        SwitchBtn();
    }

    void SwitchBtn()
    {
        Color color = Color.green;
        if (isPlaying)
            color = Color.red;

        ColorBlock cb = new ColorBlock();
        cb.normalColor = color;
        cb.highlightedColor = color;
        cb.pressedColor = Color.blue;
        cb.disabledColor = Color.black;
        cb.colorMultiplier = 1f;
        playGo.GetComponent<Button>().colors = cb;
    }

    IEnumerator StartPlay()
    {
        initState();

        disableButton = true;

        SceneManager.Instance.effectControl.ResetEffect();

        myAs.clip = SceneManager.Instance.readyClip;
        float wSecond = myAs.clip.length;
        myAs.Play();
        yield return new WaitForSeconds(wSecond);
        myAs.Stop();
        disableButton = false;
        RealStartPlay();

        SwitchBtn();

       
    }

    void RealStartPlay()
    {
        isPlaying = true;
        startTime = Time.time;
        playDur = SceneManager.Instance.totalTime;

        myAs.clip = SceneManager.Instance.bgmClip;
        myAs.Play();

        SceneManager.Instance.soundControl.StartRecord();
        SceneManager.Instance.effectControl.StartEffect();

        KeyControlStart();
    }

    void StartReplay()
    {
        isPlaying = true;
        startTime = Time.time;
        playDur = SceneManager.Instance.totalTime;

        myAs.clip = SceneManager.Instance.bgmClip;
        myAs.Play();

        SceneManager.Instance.soundControl.Replay();
        SceneManager.Instance.effectControl.ResetEffect();
        SceneManager.Instance.effectControl.StartEffect();

        initState();
        KeyControlStart();
    }


    void StopPlay()
    {
        StopAllCoroutines();

        if (!SceneManager.Instance.soundControl.HasNotes())
            SceneManager.Instance.soundControl.CollectNotes();

 
        myAs.Stop();

        SceneManager.Instance.soundControl.StopPlay();
        SceneManager.Instance.effectControl.StopEffect();

        isPlaying = false;
    }


    void initState()
    {
        SetkeyValue(SceneManager.Instance.inputRefList[0]);
    }


    void KeyControlStart()
    {
        List<InputRef> inputRefList = SceneManager.Instance.inputRefList;
        int chordnum = inputRefList.Count;

        float sChordTime = playDur / chordnum;

        for (int i = 1; i < inputRefList.Count; i++)
        {
            InputRef ir = inputRefList[i];
            float ttime = i * sChordTime;

            StartCoroutine(delaySetKey(ir,ttime));
        }
    }

    IEnumerator delaySetKey(InputRef ir, float sec)
    {
        yield return new WaitForSeconds(sec);
        SetkeyValue(ir);
    }

    void SetkeyValue(InputRef ir)
    {
        SceneManager.Instance.soundControl.SetKeys(ir);

    }


}
