using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Clock : MonoBehaviour {
	private const float
		hoursToDegrees = 360 / 12,
		minutesToDegrees = 360/60,
		secondsToDegrees = 360/60;
	public Transform hours, minutes, seconds;
	public bool analog = true;

	// Use this for initialization
	void Start () {
		
	}


	void Update(){
		if (analog) {
			TimeSpan timespan = DateTime.Now.TimeOfDay;
			hours.localRotation = Quaternion.Euler (0, 0, (float)timespan.TotalHours * hoursToDegrees);
			minutes.localRotation = Quaternion.Euler (0, 0, (float)timespan.TotalMinutes * minutesToDegrees);
			seconds.localRotation = Quaternion.Euler (0, 0, (float)timespan.TotalSeconds * secondsToDegrees);
		} else {
			DateTime time = DateTime.Now;
			hours.localRotation = Quaternion.Euler (0, 0, time.Hour * hoursToDegrees);
			minutes.localRotation = Quaternion.Euler (0, 0, time.Minute * minutesToDegrees);
			seconds.localRotation = Quaternion.Euler (0, 0, time.Second * secondsToDegrees);
		}
	}
}

