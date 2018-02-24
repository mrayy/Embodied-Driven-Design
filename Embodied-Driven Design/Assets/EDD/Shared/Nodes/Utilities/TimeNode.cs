using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Klak.Wiring;

[ModelBlock("Transfer/Spatial/Time")]
public class TimeNode : BlockBase
{
	[SerializeField, Outlet]
	FloatEvent _timeEvent = new FloatEvent ();
	[SerializeField, Outlet]
	FloatEvent _dtEvent = new FloatEvent ();

	[SerializeField,Outlet]
	VoidEvent _timerEvent=new VoidEvent();

	public float TimerTrigger=0;

	float _time=0;


	protected void _Invoke()
	{
		_timeEvent.Invoke (Time.time);
		_dtEvent.Invoke (Time.deltaTime);
	}



	protected override void UpdateState()
	{
		_Invoke();

		if (TimerTrigger > 0) {
			_time += Time.deltaTime;
			if (_time > TimerTrigger) {
				_time = 0;
				_timerEvent.Invoke ();
			}
		}
	}
}
