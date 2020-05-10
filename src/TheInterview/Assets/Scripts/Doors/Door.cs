using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour 
{
	[SerializeField] private bool canBeOpened = true;
	public float OpenSpeed = 1;
	public float CloseSpeed = 1;
	public bool isAutomatic = false;
	public bool AutoClose = false;
	public bool DoubleSidesOpen = false;
	public string PlayerHeadTag = "MainCamera";
	public string OpenForwardAnimName = "Door_anim";
	public string OpenBackwardAnimName = "DoorBack_anim";
	private string _animName;
	private bool inTrigger = false;
	private bool isOpen = false;
	private Vector3 relativePos;
	private Animation anim;

	public void SetCanBeOpened(bool state) => canBeOpened = state;
	
	void Start () 
	{
		anim = GetComponent<Animation>();
		if (anim != null && anim.clip != null)
			_animName = anim.clip.name;
		else
			Debug.Log($"Door {name} cannot be opened, since it has no animations");
	}
	
	void Update ()
	{
		if (!canBeOpened || !inTrigger) 
			return;
		
		if(Input.GetKeyDown(KeyCode.E) && !isAutomatic)
		{
			if (!isOpen) 
			{
				isOpen = true;
				OpenDoor();
			} 
			else 
			{
				isOpen = false;
				CloseDoor();
			}
		}
	}
	void OpenDoor()
	{
		Debug.Log("Opening Door");
		anim [_animName].speed = 1 * OpenSpeed;
		anim [_animName].normalizedTime = 0;
		anim.Play (_animName);
	}
	
	void CloseDoor()
	{
		Debug.Log("Closing Door");
		anim [_animName].speed = -1 * CloseSpeed;
		if (anim [_animName].normalizedTime > 0) {
			anim [_animName].normalizedTime = anim [_animName].normalizedTime;
		} else {
			anim [_animName].normalizedTime = 1;
		}
		anim.Play (_animName);
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
