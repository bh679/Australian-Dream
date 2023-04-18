using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGameRandom : MonoBehaviour
{
	public void LoadLevelOrNext(int id){
		//	SceneManager.LoadScene(id);
		StartCoroutine(WaitLoadLevel(id + ((Random.Range(0,100)>50)?0:1)));
	}
		
	IEnumerator WaitLoadLevel(int x)
	{
		yield return new WaitForSeconds(2);
		SceneManager.LoadScene(x);
	}
}
