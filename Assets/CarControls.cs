using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarControls : MonoBehaviour
{
	public InputActionAsset EmulatorActionSet;
	public InputActionReference Left, Right, Horn;
	public Transform model;
	
	public float Speed = 20f, SideSpeed = 10f;
	float speed;
	float angle;
	public float speedBoostTime = 5f;
	float startSpeed = 0;
	public ParticleSystem flames;
	public float speedDistanceMutli = 0.05f;
	
	public Vector3 turnAngle;
	
    // Start is called before the first frame update
    void Start()
    {
	    speed = 0;
	    startSpeed = Speed;
    }
    
	public void SpeedBoost(float boost)
	{
		Speed += boost;
		speed += boost/2f;
		
		flames.gameObject.SetActive(true);
		flames.loop = true;
		flames.Play();
		
		StartCoroutine(reduceBoostAfter(speedBoostTime, boost));
	}


	IEnumerator reduceBoostAfter(float time, float speedBoost)
	{
		yield return new WaitForSeconds(time);
		
		Speed -= speedBoost;
		if(Speed <= startSpeed)
			flames.loop = false;
		
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
	
	
    // Update is called once per frame
    void Update()
	{
		if(speed < Speed)
			speed += (Time.deltaTime+1*speed);
		else
			speed = Speed;
			
		if(Right.action.IsPressed() && !Left.action.IsPressed())
		{
			this.transform.position += Vector3.right*SideSpeed*Time.deltaTime;
			model.localEulerAngles = turnAngle;
		}
		else if(!Right.action.IsPressed() && Left.action.IsPressed())
		{
			this.transform.position += Vector3.left*SideSpeed*Time.deltaTime;
			model.localEulerAngles = -turnAngle;
		}else 
			model.localEulerAngles = Vector3.zero;
		
		model.transform.localPosition = Vector3.forward * speed* speedDistanceMutli;
	    this.transform.position += Vector3.forward*speed*Time.deltaTime;
    }
}
