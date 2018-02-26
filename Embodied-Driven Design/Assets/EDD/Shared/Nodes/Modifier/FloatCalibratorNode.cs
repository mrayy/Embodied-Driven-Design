using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Klak.Wiring
{
    [ModelBlock("Transfer/Spatial/Float Calibrator")]
    public class FloatCalibratorNode : BlockBase
    {

        [SerializeField, Outlet]
        public FloatEvent Output = new FloatEvent();

        float _reference = 0;

        bool _calibrate = false;

        [Inlet]
        public float Reference
        {
            set { _reference = value; }
            get { return _reference; }
        }

        [Inlet]
        public float Input
        {
            set
            {
                if (_calibrate)
                    _reference=value;
                Output.Invoke( value- _reference);
            }
        }

        [Inlet]
        public void Calibrate()
        {
            _calibrate = true;
        }
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        protected override void UpdateState()
        {

        }
    }
}