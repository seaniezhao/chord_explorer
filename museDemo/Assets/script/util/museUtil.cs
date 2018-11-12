using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MuseUtil
{

    public static float MidiToUnityPitch(int midi)
    {

        int pitch = midi - 61;

        return Mathf.Pow(1.05946f, pitch);
    }

    public static Dictionary<string, int> PitchValue = new Dictionary<string, int>()
    {
        {"C", 0},

        {"C#", 1},
        {"Db", 1},

        {"D", 2},

        {"D#", 3},
        {"Eb", 3},

        {"E", 4},

        {"F", 5},

        {"F#", 6},
        {"Gb", 6},

        {"G", 7},

        {"G#", 8},
        {"Ab", 8},

        {"A", 9},

        {"A#", 10},
        {"Bb", 10},

        {"B", 11},
    };
   

    public static int NameToMidi(string name)
    {
        switch (name)
        {
            case "C": return 61;
            case "D": return 63;
            case "E": return 65;
            case "F": return 66;
            case "G": return 68;
            case "A": return 70;
            case "B": return 71;
        }

        return 0;
    }



    public static string MidiToName(int midi)
    {
        int pitchClass = midi % 12;
        
        switch(pitchClass)
        {
            case 0: return "B";
            case 1: return "C";
            case 2: return "C#";
            case 3: return "D";
            case 4: return "D#";
            case 5: return "E";
            case 6: return "F";
            case 7: return "F#";
            case 8: return "G";
            case 9: return "G#";
            case 10: return "A";
            case 11: return "A#";
        } 


        return "NULL";
    }

}


