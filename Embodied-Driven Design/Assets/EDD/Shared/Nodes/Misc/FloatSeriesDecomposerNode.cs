using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Klak.Wiring;
#if UNITY_EDITOR
using UnityEditor;
#endif

[ModelBlock("Transfer/Misc/Float Series Decomposer", "", 130)]
public class FloatSeriesDecomposerNode : BlockBase
{
    [Inlet]
    public List<float> Array
    {
        set
        {
            foreach (var v in value)
            {
                Output.Invoke(v);
            }
        }
    }

    [SerializeField, Outlet]
    FloatEvent Output = new FloatEvent();

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    protected override void UpdateState()
    {

    }
}
