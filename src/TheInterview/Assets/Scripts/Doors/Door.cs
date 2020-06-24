using System;
using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour 
{
	[SerializeField] private bool canBeOpened = true;
	public float OpenSpeed = 1;
	public float CloseSpeed = 1;
	public bool isAutomatic = false;
	public bool AutoClose = false;
	public bool DoubleSidesOpen = false;
	public bool OneWay = false;
	public PlayerTrackerZone otherSide;
	public string PlayerHeadTag = "Player";
	public string OpenForwardAnimName = "Door_anim";
	public string OpenBackwardAnimName = "DoorBack_anim";
	[SerializeField] private AudioClip lockedSound;
	[SerializeField] private AudioClip openSound;
	[SerializeField] private AudioClip closeSound;
	[SerializeField] private AudioSource source;
	private string _animName;
	private bool inTrigger = false;
	private bool isOpen = false;
	private Vector3 relativePos;
	private Animation anim;
	private bool _playSounds = true;

	public void SetDoorSoundsEnabled(bool soundsEnabled) => _playSounds = soundsEnabled;
	
	public void SetCanBeOpened(bool state)
	{
		canBeOpened = state;
		Debug.Log($"Door {name} is {(state ? "Unlocked" : "Locked")}");
	}

	void Start() 
	{
		anim = GetComponent<Animation>();
		if (anim != null && anim.clip != null)
			_animName = anim.clip.name;
		else
			Debug.Log($"Door {name} cannot be opened, since it has no animations");
	}
	
	void Update()
	{
		if (!inTrigger && (!OneWay || !otherSide.IsPlayerIn)) 
			return;

		if (OneWay && otherSide.IsPlayerIn)
		{
			SetCanBeOpened(false);
			if (isOpen)
				CloseDoor();
		}
		
		if (InteractionInputs.IsPlayerSignallingInteraction() && !isAutomatic)
		{
			if (!canBeOpened)
			{
				if (source == null || lockedSound == null) 
					return;
				Debug.Log("Door - Locked Sound Effect");
				source.clip = lockedSound;
				source.Play();
			}
			else
			{
				if (!isOpen)
                    OpenDoor();
                else
                    CloseDoor();
            }
		}
	}

	public void ToggleDoor()
	{
		if (isOpen)
			CloseDoor();
		else
			OpenDoor();
	}
	
	public void OpenDoor()
	{
		if (isOpen || anim.isPlaying)
			return;
		
		if (!canBeOpened)
		{
			PlaySound(lockedSound);
			return;
		}

		var isOnWrongSide = OneWay && otherSide.IsPlayerIn;
		if (isOnWrongSide)
			return;
		
		isOpen = true;
		PlaySound(openSound);
		anim[_animName].speed = 1 * OpenSpeed;
		anim[_animName].normalizedTime = 0;
		anim.Play(_animName);
	}

	private void PlaySound(AudioClip c)
	{
		if (c == null || !_playSounds)
			return;
		
		source.Stop();
		source.clip = c;
		source.Play();
	}
	
	public void CloseDoor()
	{
		if (!isOpen || anim.isPlaying)
			return;
		
        isOpen = false;
        StartCoroutine(ExecuteAfterDelay(0.6f, () => PlaySound(closeSound)));
		anim [_animName].speed = -1 * CloseSpeed;
		if (anim [_animName].normalizedTime > 0) {
			anim [_animName].normalizedTime = anim [_animName].normalizedTime;
		} else {
			anim [_animName].normalizedTime = 1;
		}
		anim.Play(_animName);
	}

	private IEnumerator ExecuteAfterDelay(float delay, Action action)
	{
		yield return new WaitForSeconds(delay);
		action();
	}
	
	void OnTriggerEnter(Collider other){
		Debug.Log("Door Trigger Enter");
		if(other.GetComponent<Collider>().tag == PlayerHeadTag){
			if(DoubleSidesOpen){
			relativePos = gameObject.transform.InverseTransformPoint (other.transform.position);
			if (relativePos.z > 0) {
				_animName = OpenForwardAnimName;
			} else {
				_animName = OpenBackwardAnimName;
			}
			}
			if (isAutomatic) {
				OpenDoor ();
			}

			inTrigger = true;
		}
	}
	void OnTriggerExit(Collider other){
		if(other.GetComponent<Collider>().tag == PlayerHeadTag){
			if (isAutomatic) {
				CloseDoor ();
			} else {
				inTrigger = false;
			}
			if (AutoClose && isOpen) {
				CloseDoor ();
				inTrigger = false;
				isOpen = false;
			}
		}
	}
}
