using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SoundControl : MonoBehaviour {

    public static int KEY_NUM = 13;

    public GameObject[] keyArray;
    public AudioSource myAs;

    private const float baseNum = 1.05946f;
    private List<KeyControl> keyCtrlList = new List<KeyControl>();
     

    List<Note> allNotes = new List<Note>();
    private float notesStartTime;

    // Use this for initialization
    void Awake () {
        for (int i = 0; i < keyArray.Length; ++i)
        {
            EventTrigger etrigger = keyArray[i].GetComponent<EventTrigger>();
            KeyControl kctrl = keyArray[i].GetComponent<KeyControl>();

            AddTriggers(etrigger, kctrl);

            keyCtrlList.Add(kctrl);

        

        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool HasNotes()
    {
        if (allNotes.Count > 0)
        {
            return true;
        }

        return false;
    }

    public void DeleteAllNotes()
    {
        allNotes.Clear();
    }


    void AddTriggers(EventTrigger trigger, KeyControl kctrl)
    {
        //EventTrigger.Entry entry = new EventTrigger.Entry();
        //entry.eventID = EventTriggerType.PointerEnter;
        //entry.callback.AddListener((eventData) => { kctrl.OnKeyDown(); });
        //trigger.triggers.Add(entry);

        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener((eventData) => { kctrl.OnKeyDown(); });
        trigger.triggers.Add(entry);

        EventTrigger.Entry entry1 = new EventTrigger.Entry();
        entry1.eventID = EventTriggerType.PointerUp;
        entry1.callback.AddListener((eventData) => { kctrl.OnKeyUp(); });
        trigger.triggers.Add(entry1);
    }


    public void SetKeys(InputRef ir)
    {
        List<KeyInputRef> kir = ir.GetKeyInputRefs(KEY_NUM);

        for (int i = 0; i < keyCtrlList.Count; ++i)
        {
            int midi_p = i + 61; //25
            keyCtrlList[i].SetKey(kir[i]);

          
        }
    }

    public void StartRecord()
    {
        for (int i = 0; i < keyCtrlList.Count; ++i)
        {
            keyCtrlList[i].StartRecord();
        }
    }

    public void CollectNotes()
    {

        allNotes.Clear();
        for (int i = 0; i < keyCtrlList.Count; ++i)
        {
            allNotes.AddRange(keyCtrlList[i].GetNotes());
        }

        notesStartTime = SceneManager.Instance.controlPanel.startTime;
        //test
        //Replay();
    }

    public void StopPlay()
    {
        StopAllCoroutines();
    }


    public void Replay()
    {
        for (int i = 0; i < allNotes.Count; ++i)
        {
            StartCoroutine(DoRePlay(allNotes[i])); 
        }
    }

    IEnumerator DoRePlay(Note nt)
    {
        float wtime = nt.start - notesStartTime;

        yield return new WaitForSeconds(wtime);

        nt.myKey.Play(nt);
    }

}



