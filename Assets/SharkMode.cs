using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkMode : MonoBehaviour
{
	public Animator animator;
	public GameObject[] Sharks;
	public float time = 5f;
	public CharacterInput inputSystem;
	
	public void StartSharkMode()
	{
		animator.SetBool("SharkMode", true);
		animator.SetTrigger("BeShark");
		
		for(int i = 0; i < Sharks.Length; i++)
			Sharks[i].SetActive(true);
		
		
		
		StartCoroutine(_endSharkMode());
	}
	
	IEnumerator _endSharkMode()
	{
		yield return new WaitForSeconds(time);
		
		animator.SetBool("SharkMode", false);
		
		for(int i = 0; i < Sharks.Length; i++)
			Sharks[i].SetActive(false);
			
		inputSystem.ResetDirectionAfterTime(0.5f);
		
		
	}
}
