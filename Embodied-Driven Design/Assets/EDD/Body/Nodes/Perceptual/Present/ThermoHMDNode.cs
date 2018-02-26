using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Klak.Wiring;
using System.Threading;

[ModelBlock("Perceptual/Sensory/Thermo HMD", "")]
public class ThermoHMDNode : BlockBase
{
    public string COMPort;

    SerialHandler _serial;


    public float[] Actuators = new float[4];
    public float[] RefValues = new float[4];

    float[] _lastData = new float[4];

    float timeOut = 0;
    public float TimeOutInterval = 100;

    public float MaximumTemp = 37;
    public float MinimumTemp = 26;

    public float SkinTemp = 30;
    public int SamplesCount = 10;

    public float ScalingFactor = 1;

    public bool _calibrating = false;

    int _samplesCount = 0;
    List<float>[] _samples = new List<float>[] { new List<float>(), new List<float>(), new List<float>(), new List<float>() };

    MovingAverageF[] _average=new MovingAverageF[4];

    float CheckTemp(float t)
    {
        return Mathf.Clamp(t, MinimumTemp, MaximumTemp);
    }
    [Inlet]
    public float Actuator1
    {
        set
        {
            Actuators[0] = (value);
        }
    }
    [Inlet]
    public float Actuator2
    {
        set
        {
            Actuators[1] = (value);
        }
    }
    [Inlet]
    public float Actuator3
    {
        set
        {
            Actuators[2] = (value);
        }
    }
    [Inlet]
    public float Actuator4
    {
        set
        {
            Actuators[3] = (value);
        }
    }
//     [Inlet]
//     public float Actuator5
//     {
//         set
//         {
//             Actuators[4] = CheckTemp(value);
//         }
//     }

    void Start()
    {
        _serial = new SerialHandler();
        _serial.portName = COMPort;
        _serial.baudRate = 115200;
        _serial.OnDataReceived += OnDataReceived;
        _serial.Open(true);
        Thread.Sleep(500);

        for (int i=0;i<4;++i)
            _average[i] = new MovingAverageF(SamplesCount);
    }

    void OnDataReceived(string msg)
    {
        Debug.Log(msg);
    }

    void OnDestroy()
    {
        _serial.Write("@stop#");
        _serial.Close();
    }

    bool _isChanged()
    {

        for (int i = 0; i < 5; ++i)
        {
            if (Actuators[i] != _lastData[i])
            {
                return true;
            }
        }
        return false;
    }

    int[] indicies = new int[] { 0, 1, 2, 4 };
    public float[] vals = new float[] { 0, 0, 0, 0, 0 };
    bool SendValue()
    {
        //         if (!_isChanged())
        //             return false;


        if(_calibrating)
        {
            for (int i = 0; i < 4; ++i)
            {
                _samples[i].Add(Actuators[i]);
            }
            for (int i = 0; i < 4; ++i)
            {
                RefValues[i] = 0;
                for(int j=0;j<_samples[i].Count;++j)
                {
                    RefValues[i] += _samples[i][j];
                }
                RefValues[i] /= (float)_samples[i].Count;
            }
            ++_samplesCount;
            if (_samplesCount > SamplesCount)
            {
                _calibrating = false;
            }
        }

        string cmd = "@e";
        for (int i = 0; i < 4; ++i)
        {
            _average[i].Add(Actuators[i], 1);
         //   RefValues[i] = _average[i].Value();
            vals[indicies[i]] = CheckTemp(SkinTemp + ScalingFactor*(Actuators[i] - (RefValues[i])));
            _lastData[i] = Actuators[i];
        }
        for(int i = 0; i < vals.Length; ++i) { 
            //e225 e[1-5][temp]
            float v = vals[i] ;
            cmd +=  v.ToString("0.00");
            if (i < 4)
                cmd += ",";
        }

        cmd += "#\n";
        _serial.Write(cmd);
        return true;
    }

    protected override void UpdateState()
    {
        timeOut += Time.deltaTime * 1000.0f;
        if (timeOut > TimeOutInterval) {

            SendValue();
            timeOut = 0;
        }

        if(Input.GetKeyDown(KeyCode.C))
        {
            _calibrating = true;
            for (int i = 0; i < 4; ++i)
                _samples[i].Clear();
        }

        _serial.Update();
    }
}
