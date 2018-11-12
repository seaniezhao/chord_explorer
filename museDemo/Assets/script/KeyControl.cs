using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyControl : MonoBehaviour {
    const float MAXTIME = 5.35f;

    private Text text;
    private Button button;
    private AudioSource myas;
    public int pitch;

    private bool volumeDown = false;

    private List<Note> noteList = new List<Note>();
    private Note currentNote = null;
    private bool isStarted = false;
	// Use this for initialization
	void Start () {
        myas = gameObject.GetComponent<AudioSource>();

        text = gameObject.GetComponentInChildren<Text>();
        button = gameObject.GetComponent<Button>();
    }
	
	// Update is called once per frame
	void Update () {
        UpdateVolume();

    }

    public List<Note> GetNotes()
    {
        return noteList;
    }

    public void Play(Note nt)
    {
        float dur = nt.Duration();
        StartCoroutine(DoPlay(dur));
    }

    IEnumerator DoPlay(float dur)
    {
        OnKeyDown();
        yield return new WaitForSeconds(dur);
        OnKeyUp();
    }

    public void StartRecord()
    {
        noteList.Clear();
        isStarted = true;
    }

    public void StopRecord()
    {
        isStarted = false;
    }

    void UpdateVolume()
    {
        if (volumeDown)
        {
            myas.volume -= 0.05f;
        }
        if (myas.volume < 0.01)
        {
            if (myas.isPlaying)
            {
                myas.Stop();
            }
            volumeDown = false;
        }
    }

    public void SetKey(KeyInputRef kir)
    {
        pitch = kir.midi;
        
        text.text = kir.showText;

        Color color = new Color(1, 1, 1,1);
        if (kir.isSteadyTone)
        {
            color = new Color(0, 1, 0, 1);
        }
        else if (kir.isPentatonic)
        {
            color = new Color(0, 0, 1, 1);
        }


        ColorBlock cb = new ColorBlock();
        cb.normalColor = color;
        cb.highlightedColor = color;
        cb.pressedColor = Color.gray;
        cb.disabledColor = Color.black;
        cb.colorMultiplier = 1f;
        button.colors = cb;
    }

    public void OnKeyDown()
    {
        volumeDown = false;
        myas.pitch = MuseUtil.MidiToUnityPitch(pitch);
        myas.volume = 1;
        myas.Stop();
        myas.Play(0);
        print("keydown");

        if (isStarted)
        {
            currentNote = new Note(Time.time, pitch);
            currentNote.myKey = this;
        }
    }

    public void OnKeyUp()
    {
        if (myas.isPlaying)
        {
            //myas.Stop();
            volumeDown = true;
            print("stop");
        }


        if (currentNote != null)
        {
            currentNote.SetEnd(Time.time);
            noteList.Add(currentNote);
            currentNote = null;
        }
    }




}
