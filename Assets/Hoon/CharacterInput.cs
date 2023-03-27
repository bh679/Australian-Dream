using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterInput : MonoBehaviour
{
	public InputActionAsset EmulatorActionSet;
	public InputActionReference Left, Right, Jump, Roll;
	public Animator animator;
	public float rollSpeed = 1f;
	public float rollstartTime = 1f, rollStopTime = 1f;
	bool isRolling = false;
	public Collider defaultCollider, rollCollider, jumpCollider;
	public float startDelay;
	Vector3 startDirection;
	public Transform camera;
	Vector3 cameraPos, cameraAng;
	public ParticleSystem boostParticles;
	
	void Start()
	{
		startDirection = this.transform.eulerAngles;
		cameraPos = camera.transform.localPosition;
		cameraAng = camera.transform.localEulerAngles;
		camera.SetParent(transform.parent);
	}
	
    // Start is called before the first frame update
	void OnEnable()
    {
        
	    if (EmulatorActionSet != null) {
		    foreach (var map in EmulatorActionSet.actionMaps) {
			    foreach (var action in map) {
				    if(action != null) {
					    action.Enable();
				    }
			    }
		    }
	    }
	    
    }
	
	IEnumerator startAfterDelay(float time)
	{
		yield return new WaitForSeconds(time);
		StartRunning();
		
	}
    
	public float speedBoost = 2f, boostTime = 4f;
	public void SpeedBoost()
	{
		boostParticles.gameObject.SetActive(true);
		boostParticles.Play();
		boostParticles.loop = true;
		
		animator.speed = 2f;
		if(_boostTime <= 0)
		{
			StartCoroutine(returnSpeedAfterDelay());
		}
		_boostTime = boostTime;
	}
	
	float _boostTime = 0;
	
	IEnumerator returnSpeedAfterDelay()
	{
		_boostTime = boostTime;
		while(_boostTime > 0)
		{
			yield return new WaitForSeconds(0.1f);
			_boostTime -= 0.1f;
		}
		boostParticles.loop = false;
		_boostTime = 0;
		animator.speed = 1f;
		
	}
	
	bool started = false;
	void StartRunning()
	{
		
		if(started)
			return;
		started = true;
		
		Debug.Log(startDirection);
		animator.SetBool("Started", true);
		this.transform.eulerAngles= startDirection;
		camera.SetParent(this.transform);
		camera.transform.localPosition = cameraPos;
		camera.transform.localEulerAngles = cameraAng;
		ResetDirectionAfterTime(0.75f);
	}
	
	public void ResetDirectionAfterTime(float time)
	{
		
		StartCoroutine(directionAfterDelay(time));
	}
	
	IEnumerator directionAfterDelay(float time)
	{
		while(time > 0)
		{
			yield return new WaitForSeconds(0.05f);
			time-= 0.05f;
			this.transform.eulerAngles = startDirection;
		}
		
		this.transform.eulerAngles = startDirection;
		
		yield return null;
	}
	
	

    // Update is called once per frame
    void Update()
	{
		
		if(Jump.action.IsPressed())
		{
			animator.SetTrigger("Jump");
			StartRunning();
		}
		else if(Right.action.IsPressed() && !Left.action.IsPressed())
		{
			animator.SetBool("Right", true);
			animator.SetBool("Left", false);
			StartRunning();
		}
		else if(!Right.action.IsPressed() && Left.action.IsPressed())
		{
			animator.SetBool("Right", false);
			animator.SetBool("Left", true);
			StartRunning();
		}else
		{
			animator.SetBool("Right", false);
			animator.SetBool("Left", false);
		}
		
		animator.SetBool("Roll",Roll.action.IsPressed());
		
		if(isRolling)
		{
			this.transform.position += this.transform.forward*rollSpeed*Time.deltaTime;
		
			if(!changingRollState && !Roll.action.IsPressed())
			{
				StartCoroutine(stopRollAfterTime(rollStopTime));
			}
			
		}
		else
		{
			if(Roll.action.IsPressed())
			{
				this.transform.position += this.transform.forward*rollSpeed*Time.deltaTime;
		
				isRolling = true;
				SetRollCollider(true);
				StartRunning();
			}
			
		}
	}
	
	public void TurnAroundAfter(float time)
	{
		StartCoroutine(turnAroundAfter(time));
	}
	
	IEnumerator turnAroundAfter(float time)
	{
		for(int i = 0; i < 180; i++)
		{
			this.transform.RotateAround(this.transform.position,Vector3.up,1);
			yield return new WaitForSeconds(time/180f);
		}
	}
	
	void SetRollCollider(bool isOn)
	{
		defaultCollider.enabled = !isOn;
		rollCollider.enabled = isOn;
	}
	
	bool changingRollState = false;
	
	IEnumerator stopRollAfterTime(float time)
	{
		changingRollState = true;
		yield return new WaitForSeconds(time);
		SetRollCollider(false);
		isRolling = false;
		changingRollState = false;
	}
    
	void OnDisable() {

		// Disable Input Actions
		if (EmulatorActionSet != null) {
			foreach (var map in EmulatorActionSet.actionMaps) {
				foreach (var action in map) {
					if (action != null) {
						action.Disable();
					}
				}
			}
		}
	}
}
