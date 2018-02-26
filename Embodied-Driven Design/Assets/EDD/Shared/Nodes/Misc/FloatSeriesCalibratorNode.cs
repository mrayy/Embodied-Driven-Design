using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Klak.Wiring;
#if UNITY_EDITOR
using UnityEditor;
#endif

[ModelBlock("Transfer/Misc/Float Series Calibrator", "", 160)]
public class FloatSeriesCalibratorNode : BlockBase
{
    [SerializeField]
    float _reference = 0;

    public bool AutoCalibrate = false;
    [SerializeField]
    bool _calibrate = false;

    [Inlet]
    public List<float> Array
    {
        set
        {
            List<float> ret = new List<float>();
            if (_calibrate && value.Count > 0)
            {
                float sum = 0;
                foreach (var v in value)
                    sum += v;
                sum /= value.Count;
                _reference = sum;
                _calibrate = false;
            }
            foreach (var v in value)
            {
                ret.Add(v - _reference);
            }
            Output.Invoke(ret);
        }
    }

    [Inlet]
    public float Reference
    {
        set
        {
            _reference = value;
        }
        get
        {
            return _reference;
        }
    }

    [Inlet]
    public void Calibrate()
    {
        _calibrate = true;
    }
    [SerializeField, Outlet]
    FloatArrayEvent Output = new FloatArrayEvent();

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    protected override void UpdateState()
    {

    }
    public override void OnNodeGUI()
    {
        base.OnNodeGUI();

        GUILayout.Label("Ref: " + _reference.ToString());
    }
}
