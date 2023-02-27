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

    // Update is called once per frame
    void Update()
	{
		
		if(Jump.action.IsPressed())
		{
			animator.SetTrigger("Jump");
		}
		else if(Right.action.IsPressed() && !Left.action.IsPressed())
		{
			animator.SetBool("Right", true);
			animator.SetBool("Left", false);
		}
		else if(!Right.action.IsPressed() && Left.action.IsPressed())
		{
			animator.SetBool("Right", false);
			animator.SetBool("Left", true);
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
