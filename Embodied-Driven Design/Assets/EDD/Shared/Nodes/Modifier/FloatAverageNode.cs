using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Klak.Wiring
{
	[ModelBlock("Transfer/Filter/Float Average")]
	public class FloatAverageNode : BlockBase {

		[SerializeField, Outlet]
		public FloatEvent value = new FloatEvent();

        float[] values = new float[4];

        float CalcAverage()
        {
            if (values.Length == 0)
                return 0;
            float avg = 0;
            foreach (var v in values)
                avg += v;
            return avg / (float)values.Length;
        }

		[Inlet]
		public float Input1
		{
			set {
				if (!enabled) return;
                values[0] = value;
                if (!Active)
                    return;
				this.value.Invoke (CalcAverage());
			}
        }
        [Inlet]
        public float Input2
        {
            set
            {
                if (!enabled) return;
                values[1] = value;
                if (!Active)
                    return;
                this.value.Invoke(CalcAverage());
            }
        }
        [Inlet]
        public float Input3
        {
            set
            {
                if (!enabled) return;
                values[2] = value;
                if (!Active)
                    return;
                this.value.Invoke(CalcAverage());
            }
        }
        [Inlet]
        public float Input4
        {
            set
            {
                if (!enabled) return;
                values[3] = value;
                if (!Active)
                    return;
                this.value.Invoke(CalcAverage());
            }
        }
        // Use this for initialization
        void Start () {
		}
		
		// Update is called once per frame
		void Update () {
		}
	}
}