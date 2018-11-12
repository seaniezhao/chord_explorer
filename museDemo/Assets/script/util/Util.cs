using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note {

    public Note(float stime, int p, int vel = 95)
    {
        start = stime;
        pitch = p;
        velocity = vel;
    }
    public float start = 0;
    float end = 0;
    public int pitch = 0;
    int velocity = 0;

    public KeyControl myKey;

    public float Duration()
    {
        return end - start;
    }

    public void SetEnd(float e)
    {
        end = e;
    }

}

public class KeyInputRef
{
    public int midi;
    public string showText;
    public bool isChordTone;
    public bool isSteadyTone;
    public bool isPentatonic;

}

//每个小节的 和弦，调性，音阶
public class InputRef
{
    public string mainKey;

    public string key;
    public List<string> chord = new List<string>();
    //以c开头的 音阶表示
    public List<int> scale = new List<int>();

    //计算得来
    public List<int> absPitchs = new List<int>();

    public List<KeyInputRef> GetKeyInputRefs(int keynum)
    {
        List<KeyInputRef> pitches = new List<KeyInputRef>();

        int midIndex = keynum / 2 - 1;
        int scaleCount = scale.Count;
        int keyScaleIndex = MuseUtil.PitchValue[key.Replace("m", string.Empty)];
        int pindex = keyScaleIndex - midIndex - 1;

        for (int i = 0; i < keynum; ++i)
        {
            int scaleDiff = Mathf.Abs(pindex) / scaleCount;

            if (pindex < 0)
            {
                scaleDiff = -scaleDiff - 1;
            }

            int index = pindex;
            Debug.Log(index);
            Debug.Log(scaleDiff);
            if (pindex < 0)
            {
                index += scaleCount*Mathf.Abs(scaleDiff);
            }
            else if (pindex > scaleCount-1)
            {
                index -= scaleCount * Mathf.Abs(scaleDiff);
            }


            KeyInputRef kir = new KeyInputRef();

            int midivalue = MuseUtil.NameToMidi("C") + scale[index] + scaleDiff*12;
            kir.midi = midivalue;
            kir.showText = MuseUtil.MidiToName(midivalue);
            kir.isChordTone = IsChordTone(midivalue);
            kir.isSteadyTone = IsSteadyTone(midivalue);
            kir.isPentatonic = IsPentatonic(midivalue);


            pitches.Add(kir);
            pindex++;
        }

        return pitches;
    }


    bool IsChordTone(int midi)
    {
        for (int i = 0; i < chord.Count; ++i)
        {
            int chordpitch=MuseUtil.PitchValue[chord[i]];

            if ((midi - chordpitch - 1) % 12 == 0)
            {
                return true;
            }

        }
      
        return false;
    }

    bool IsSteadyTone(int midi)
    {
        List<int> steadyPitch = new List<int>();
        string keyTone = key.Replace("m", string.Empty);
        int keypitch = MuseUtil.PitchValue[keyTone];
        steadyPitch.Add(keypitch);
        int pitch_2;
        int pitch_3;
        if (!key.Contains("m"))
        {
            pitch_2 = keypitch + 4;
            pitch_3 = keypitch + 7;
        }
        else
        {
            pitch_2 = keypitch + 3;
            pitch_3 = keypitch + 7;
        }
        steadyPitch.Add(pitch_2);
        steadyPitch.Add(pitch_3);
        for (int i = 0; i < steadyPitch.Count; ++i)
        {
            if (steadyPitch[i] > 11)
            {
                steadyPitch[i] -= 12;
            }

            if ((midi - steadyPitch[i] - 1) % 12 == 0)
            {
                return true;
            }
        }


        return false;
    }

    bool IsPentatonic(int midi)
    {
        int[] pPitch = new int[5] {0,0,0,0,0 };
        string keyTone = mainKey.Replace("m", string.Empty);
        pPitch[0] = MuseUtil.PitchValue[keyTone];

        if (!mainKey.Contains("m"))
        {
            pPitch[1] = pPitch[0] + 2;
            pPitch[2] = pPitch[0] + 4;
            pPitch[3] = pPitch[0] + 7;
            pPitch[4] = pPitch[0] + 9;
        }
        else
        {
            pPitch[1] = pPitch[0] + 3;
            pPitch[2] = pPitch[0] + 5;
            pPitch[3] = pPitch[0] + 7;
            pPitch[4] = pPitch[0] + 10;
        }

        for (int i = 0; i < pPitch.Length; ++i)
        {
            if (pPitch[i] > 11)
            {
                pPitch[i] -= 12;
            }

            if ((midi - pPitch[i] - 1) % 12 == 0)
            {
                return true;
            }
        }

        return false;

    }
}
