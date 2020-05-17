using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Elevator : MonoBehaviour 
{
	private bool nearElevator = false;
	private Rigidbody Player;
	private Transform PlayerCam;

	[SerializeField] private bool DebugLogEnabled = true;
	[SerializeField] private PlayerTrackerZone insideElevator;
	
	[Header("Interaction settings")]
	[SerializeField] private bool canUseControls = true;
	[SerializeField] private bool isUsable = true;
	
	[Tooltip("Type your Player's tag here.")]
	public string PlayerTag = "Player";
	private Animation DoorsAnim;

	[Tooltip("Speed multiplier of the doors opening and closing. 1 is default speed.")]
	public float DoorsAnimSpeed = 1;

	[Tooltip("How fast the elevator 'passes' one floor. The time in seconds.")]
	public float OneFloorTime = 1.5f;

	private float OpenDelay = 1;

	[Tooltip("How long the doors are open. The time in seconds.")]
	public float CloseDelay = 4;
	private bool isOpen = false;
	private string AnimName = "ElevatorDoorsAnim_open";
	private string InputFloor = "";
	private int TargetFloor;

	[Tooltip("The floor, where this elevator is placed. Set an unique value for each elevator.")]
	public int CurrentFloor;

	[SerializeField] private int DestinationFloor = -999; 
	private Animation TargetElvAnim;
	private TextMesh TargetElvTextInside;
	private TextMesh TargetElvTextOutside;
	public TextMesh TextOutside;
	public TextMesh TextInside;

	[Tooltip("If set to true, the Reflection Probe inside the elevator will be updated every frame, when the player near or inside the elevator. Can impact performance.")]
	public bool UpdateReflectionEveryFrame = false;
	private bool hasReflectionProbe = true;
	private MeshRenderer ElevatorOpenButton;
	private MeshRenderer ElevatorGoBtn;
	private List<MeshRenderer> ElevatorNumericButtons = new List<MeshRenderer>();
	private AudioSource SoundFX;
	private bool SpeedUp = false;
	private bool SlowDown = false;
	private static bool Moving = false;
	private bool playerExists;
	private float PlayerHeight;
	private bool isRigidbodyCharacter;
	private ReflectionProbe probe;

	[Header("Sound Effects settings")]

	public AudioClip Bell;
	[Range(0, 1)]
	public float BellVolume = 1;

	public AudioClip DoorsOpen;
	[Range(0, 1)]
	public float DoorsOpenVolume = 1;

	public AudioClip DoorsClose;
	[Range(0, 1)]
	public float DoorsCloseVolume = 1;

	public AudioClip ElevatorMove;
	[Range(0, 1)]
	public float ElevatorMoveVolume = 1;

	public AudioClip ElevatorBtn;
	[Range(0, 1)]
	public float ElevatorBtnVolume = 1;

	public AudioClip ElevatorError;
	[Range(0, 1)]
	public float ElevatorErrorVolume = 1;

	private AudioSource BtnSoundFX;

	private RaycastHit[] raycastHits = new RaycastHit[10];
	private bool Moved = false;
	private Action _onArrival = () => { };
	
	public void DisablePlayerInteraction()
	{
		isUsable = false;
	}

	public void SetCurrentFloor(int currentFloor)
	{
		CurrentFloor = currentFloor;
		TargetFloor = currentFloor;
		UpdateText();
	}
	
	public void SetDestination(int targetFloor)
	{
		DestinationFloor = targetFloor;
	}

	public void SetOnArrivalAction(Action a)
	{
		_onArrival = a;
	}
	
	void Awake() 
	{
		if (GetComponentInChildren<ReflectionProbe> ()) {
			probe = GetComponentInChildren<ReflectionProbe> ();
			probe.refreshMode = ReflectionProbeRefreshMode.OnAwake;
			hasReflectionProbe = true;
		} else {
			hasReflectionProbe = false;
		}

		playerExists = false;
		Moving = false;
		SoundFX = GetComponent<AudioSource>();
		BtnSoundFX = SoundFX;
		
//		//SoundFX initialisation
//		SoundFX = new GameObject ().AddComponent<AudioSource>();
//		SoundFX.transform.parent = gameObject.transform;
//		SoundFX.transform.position = gameObject.transform.position + new Vector3(0, 2.2f, 0);
//		SoundFX.outputAudioMixerGroup = BtnSoundFX.outputAudioMixerGroup;
//		SoundFX.gameObject.name = "SoundFX";
//		SoundFX.playOnAwake = false;
//		SoundFX.spatialBlend = 1;
//		SoundFX.minDistance = 0.1f;
//		SoundFX.maxDistance = 20;
//		SoundFX.rolloffMode = AudioRolloffMode.Linear;
//		SoundFX.priority = 256;

		DoorsAnim = gameObject.GetComponent<Animation>();
		AnimName = DoorsAnim.clip.name;
		
		if (GameObject.FindGameObjectWithTag(PlayerTag)) {
			Player = GameObject.FindGameObjectWithTag (PlayerTag).GetComponent<Rigidbody>();
			if(Player.gameObject.GetComponent<CapsuleCollider>()){
				PlayerHeight = Player.gameObject.GetComponent<CapsuleCollider> ().height/2;
				isRigidbodyCharacter = true;
				playerExists = true;
			}else if(Player.gameObject.GetComponent<CharacterController>()){
				PlayerHeight = Player.gameObject.GetComponent<CharacterController> ().height/2 + Player.gameObject.GetComponent<CharacterController> ().skinWidth;
				isRigidbodyCharacter = false;
				playerExists = true;
			}
		} else {
			Debug.LogWarning("Elevator: Can't find Player. Please, check that your Player object has 'Player' tag.");
			enabled = false;
			playerExists = false;
		}

		if(playerExists){
			if (Player.GetComponentInChildren<Camera>().transform) {
				PlayerCam = Player.GetComponentInChildren<Camera>().transform;
			} else {
				Debug.LogWarning("Elevator: Can't find Player's camera. Please, check that your Player have a camera parented to it.");
				enabled = false;
			}
		}
	}
	
	private void Update() 
	{
		if (!isUsable || !nearElevator) 
			return;
		
		UpdateInputs();

		if(SpeedUp){
			if (SoundFX.volume < ElevatorMoveVolume) {
				SoundFX.volume += 0.9f * Time.deltaTime;
			} else {
				SpeedUp = false;
			}
			if(SoundFX.pitch < 1){
				SoundFX.pitch += 0.9f * Time.deltaTime;
			}
		}

		if(SlowDown){
			if (SoundFX.volume > 0) {
				SoundFX.volume -= 0.9f * Time.deltaTime;
			} else {
				SlowDown = false;
			}
			if(SoundFX.pitch > 0){
				SoundFX.pitch -= 0.9f * Time.deltaTime;
			}
		}
	}

	private IEnumerator ExecuteAfterDelay(Action action, float delay)
	{
		yield return new WaitForSeconds(delay);
		action();
	}

	private void UpdateInputs()
	{
		if (!InteractionInputs.IsPlayerSignallingInteraction()) 
			return;
		
		var numHits = Physics.RaycastNonAlloc(PlayerCam.position, PlayerCam.forward, raycastHits, maxDistance: 3);

		for(var i = 0; i < numHits; i++)
		{
			var hit = raycastHits[i];

			if (!Moving && !isOpen && hit.transform.CompareTag("ElevatorDoor"))
			{
				DebugLog("Open Doors Interact");
				
				OpenDoors();
			}

			if (hit.transform.CompareTag("RaycastInteract") && insideElevator.IsPlayerIn)
			{
				DebugLog("Use Control Panel");
				
				Message.Publish(new ElevatorControlButtonsPressed());
				BtnSoundFX.clip = ElevatorBtn;
				BtnSoundFX.volume = ElevatorBtnVolume;
				BtnSoundFX.Play();

				if (DestinationFloor > -999) 
					TravelTo(DestinationFloor);
				else
					DebugLog("No Destination Floor Set");
			}

			if (hit.transform.CompareTag("ElevatorButtonOpen") && !isOpen)
			{
//				ElevatorOpenButton = hit.transform.GetComponent<MeshRenderer>();
//				ElevatorOpenButton.enabled = true;

				OpenDoors();
			}

			if (canUseControls && hit.transform.CompareTag("ElevatorNumericButton") && !Moving)
			{
				Message.Publish(new ElevatorControlButtonsPressed(GetInstanceID()));
				InputFloor += hit.transform.name;
				DebugLog($"Target Floor: {InputFloor}");
//				hit.transform.GetComponent<MeshRenderer>().enabled = true;
//				ElevatorNumericButtons.Add(hit.transform.GetComponent<MeshRenderer>());
				PlayButtonSound();
			}

			if (canUseControls && hit.transform.CompareTag("ElevatorGoButton") && !Moving)
			{
				DebugLog("Player Presses Go");
				Message.Publish(new ElevatorControlButtonsPressed(GetInstanceID()));
				
				if (InputFloor != "" && InputFloor.Length < 4)
				{
					if (InputFloor == "0-1")
					{
						InputFloor = "-99";
					}

					TargetFloor = int.Parse(InputFloor);
					TravelTo(TargetFloor);
					InputFloor = "";
				}
				else
				{
					ResetButtons();
					InputFloor = "";
					BtnSoundFX.clip = ElevatorError;
					BtnSoundFX.volume = ElevatorErrorVolume;
					BtnSoundFX.Play();
				}

				if (TargetFloor != CurrentFloor)
				{
					CloseDoors();
				}
				if (TargetFloor == CurrentFloor && !isOpen)
				{
					OpenPhysicalDoorsImmediately();
				}
			}
		}
	}

	private void PlayButtonSound()
	{
		BtnSoundFX.clip = ElevatorBtn;
		BtnSoundFX.volume = ElevatorBtnVolume;
		BtnSoundFX.Play();
	}

	public void TravelTo(int targetFloor)
	{
		TargetFloor = targetFloor;

		if (CurrentFloor == TargetFloor)
		{
			OpenPhysicalDoorsImmediately();
			return;
		}

		DebugLog($"Started Traveling to Floor {TargetFloor} from Floor {CurrentFloor}");
		TargetElvAnim = DoorsAnim;
		TargetElvTextInside = TextInside;
		TargetElvTextOutside = TextOutside;

		if (hasReflectionProbe && UpdateReflectionEveryFrame)
			probe.RenderProbe();

		StartCoroutine(ExecuteAfterDelay(BeginElevatorRide, 1));
		Moving = true;
	}

	private void OpenDoors()
	{
		isOpen = true;
		StartCoroutine(ExecuteAfterDelay(OpenPhysicalDoorsImmediately, OneFloorTime * Mathf.Abs(CurrentFloor - TargetFloor) + OpenDelay));
		StartCoroutine(SummonElevatorFromOutside());
	}

	private void BeginElevatorRide() => StartCoroutine(RideElevatorInside());

	void SlowDownStart() => SlowDown = true;

	private IEnumerator RideElevatorInside()
	{
		while (isOpen)
			yield return new WaitForSeconds(0.1f);
		
		StartElevatorMovingSoundFx();

		while(true) 
		{
				
			UpdateText();
			if (TargetFloor == CurrentFloor) 
			{
				DebugLog("Has Arrived");
				yield break;
			}
			
			if (TargetFloor - CurrentFloor == 1 || CurrentFloor - TargetFloor == 1)
				Invoke ("SlowDownStart", OneFloorTime/2);


			yield return new WaitForSeconds(OneFloorTime);
			if (CurrentFloor < TargetFloor)
				CurrentFloor++;
			if (CurrentFloor > TargetFloor)
				CurrentFloor--;
			
			DebugLog($"Floor {CurrentFloor}");

			if (CurrentFloor == TargetFloor) 
			{
				DebugLog($"Has Arrived At {TargetFloor}");
				//Moving = false;
				SoundFX.Stop();
				BellSoundPlay();

				if (!isRigidbodyCharacter) {
					Player.isKinematic = false;
				}

				Player.transform.position = new Vector3 (Player.transform.position.x, TargetElvAnim.transform.position.y + PlayerHeight, Player.transform.position.z);

				if(hasReflectionProbe){
					if(UpdateReflectionEveryFrame){
						probe.refreshMode = ReflectionProbeRefreshMode.OnAwake;
						probe.RenderProbe ();
					}
				}

				if(!isRigidbodyCharacter){
					Player.isKinematic = true;
				}

				Invoke ("TargetElvOpening", OpenDelay);
				_onArrival();
			}
		}
	}

	private void StartElevatorMovingSoundFx()
	{
		DebugLog("Playing Moving Sounds");
		SoundFX.clip = ElevatorMove;
		SoundFX.loop = true;
		SoundFX.volume = 0.5f;
		SoundFX.pitch = 0.5f;
		SpeedUp = true;
		SoundFX.Play();
	}

	private IEnumerator SummonElevatorFromOutside()
	{
		while(true) 
		{
			UpdateText();

			if(CurrentFloor == TargetFloor){
				if (Moved)
					BellSoundPlay();
				yield break;
			}

			yield return new WaitForSeconds (OneFloorTime);
			if (CurrentFloor < TargetFloor) {
				CurrentFloor--;
			}
			if (CurrentFloor > TargetFloor) {
				CurrentFloor++;
			}
		}
	}

	void DoorsClosingSoundPlay(){
		if(DoorsAnim[AnimName].speed < 0){
			SoundFX.clip = DoorsClose;
			SoundFX.loop = false;
			SoundFX.volume = DoorsCloseVolume;
			SoundFX.pitch = 1;
			SoundFX.Play ();
		}
	}

	void DoorsOpeningSoundPlay(){
		if(DoorsAnim[AnimName].speed > 0){
			SoundFX.clip = DoorsOpen;
			SoundFX.volume = DoorsOpenVolume;
			SoundFX.pitch = 1;
			SoundFX.Play ();
		}
	}

	void BellSoundPlay()
	{
		DebugLog("Play Bell Sound");
		SoundFX.clip = Bell;
		SoundFX.loop = false;
		SoundFX.volume = BellVolume;
		SoundFX.pitch = 1;
		SoundFX.Play ();
		UpdateText();
	}

	void UpdateText()
	{
		TextInside.text = CurrentFloor.ToString();
		TextOutside.text = CurrentFloor.ToString();
	}

	void ResetButtons()
	{
		foreach (MeshRenderer MR in ElevatorNumericButtons) 
			MR.enabled = false;
		if (ElevatorGoBtn != null)
			ElevatorGoBtn.enabled = false;
	}

	void TargetElvOpening(){

		UpdateText ();

		TargetElvAnim [AnimName].normalizedTime = 0;
		TargetElvAnim [AnimName].speed = DoorsAnimSpeed;
		TargetElvAnim.Play ();
		ResetButtons ();

		//isOpen = true;
	}

	public void OpenPhysicalDoorsImmediately()
	{
		isOpen = true;
		TargetFloor = 0;
		UpdateText();
		DoorsAnim[AnimName].normalizedTime = 0;
		DoorsAnim[AnimName].speed = DoorsAnimSpeed;
		DoorsAnim.Play();
		ResetButtons();
	}
	void DoorsClosingTimer(){
		if(DoorsAnim[AnimName].speed > 0){
			Invoke ("CloseDoors", CloseDelay);
			isOpen = true;
			Moving = false;
		}
	}
	
	public void CloseDoors()
	{
		DoorsAnim[AnimName].normalizedTime = 1;
		DoorsAnim[AnimName].speed = -DoorsAnimSpeed;
		DoorsAnim.Play();
		if (ElevatorOpenButton != null)
			ElevatorOpenButton.enabled = false;

		StartCoroutine(ExecuteAfterDelay(() => isOpen = false, 1f));
	}

	private void OnTriggerEnter(Collider other)
	{
		DebugLog("Elevator Trigger Entered");
		if (other.gameObject != Player.gameObject) 
			return;
		
		DebugLog("Player Is Near Elevator");
		nearElevator = true;

		HotUpdateReflectionProbe();
	}

	private void HotUpdateReflectionProbe()
	{
		if (!hasReflectionProbe || !UpdateReflectionEveryFrame)
			return;

		probe.refreshMode = nearElevator 
			? ReflectionProbeRefreshMode.EveryFrame 
			: ReflectionProbeRefreshMode.OnAwake;
		probe.RenderProbe();
	}

	private void OnTriggerExit(Collider other)
	{
		DebugLog("Elevator Trigger Exited");
		if (other.gameObject != Player.gameObject) 
			return;
		
		nearElevator = false;
		HotUpdateReflectionProbe();
	}

	private void DebugLog(string msg)
	{
		if (DebugLogEnabled)
			Debug.Log($"Elevator - {msg}", gameObject);
	}
}
