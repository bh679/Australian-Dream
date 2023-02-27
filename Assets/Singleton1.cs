using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton1 : MonoBehaviour
{
	public static Singleton1 Instance { get; private set; }
	private void Awake() 
	{ 
		// If there is an instance, and it's not me, delete myself.
    
		if (Instance != null && Instance != this) 
		{ 
			Destroy(this.gameObject); 
		} 
		else 
		{ 
			Instance = this; 
			DontDestroyOnLoad(this.gameObject);
		} 
	}
}
