using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterInput : MonoBehaviour
{
	public InputActionAsset EmulatorActionSet;
	public InputActionReference Left, Right;
	public Animator animator;
	
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
		if(Right.action.IsPressed() && !Left.action.IsPressed())
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
