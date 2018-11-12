using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Config
{
    private static Config instance;
    private Config() { }
    public static Config Instance
    {
        get
        {
            if (null == instance)
            {
                instance = new Config();
            }
            return instance;
        }
    }


    public List<InputRef> GetInputRef()
    {


        List<InputRef> inputRefList = new List<InputRef>();


        //init inputreflist
        for (int i = 0; i < 8; i++)
        {
            InputRef ir = new InputRef();
            ir.mainKey = i>3?"Gm":"Bb";
            ir.key = "Cm";


            ir.scale.Add(0);
            ir.scale.Add(1);
            ir.scale.Add(2);
            ir.scale.Add(3);
            ir.scale.Add(5);
            ir.scale.Add(7);
            ir.scale.Add(9);
            ir.scale.Add(10);

            inputRefList.Add(ir);
        }
        inputRefList[0].key = "Gm";
        inputRefList[0].chord.Add("G");
        inputRefList[0].chord.Add("A#");
        inputRefList[0].chord.Add("D");

        inputRefList[1].key = "Cm";
        inputRefList[1].chord.Add("C");
        inputRefList[1].chord.Add("G");
        inputRefList[1].chord.Add("D#");

        inputRefList[2].key = "F";
        inputRefList[2].chord.Add("F");
        inputRefList[2].chord.Add("C");
        inputRefList[2].chord.Add("D");

        inputRefList[3].key = "Bb";
        inputRefList[3].chord.Add("F");
        inputRefList[3].chord.Add("D");
        inputRefList[3].chord.Add("A#");

        inputRefList[4].key = "Gm";
        inputRefList[4].chord.Add("G");
        inputRefList[4].chord.Add("A#");
        inputRefList[4].chord.Add("D");

        inputRefList[5].key = "Cm";
        inputRefList[5].chord.Add("C");
        inputRefList[5].chord.Add("D#");
        inputRefList[5].chord.Add("G");

        inputRefList[6].key = "Cm";
        inputRefList[6].chord.Add("C");
        inputRefList[6].chord.Add("D#");
        inputRefList[6].chord.Add("G");

        inputRefList[7].key = "Gm";



        return inputRefList;
     }
}