using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGameRandom : MonoBehaviour
{
	int nextLevelincrease = 0;
	void Start()
	{
		nextLevelincrease = ((Random.Range(0,100)>60)?0:1);
	}
	
	public void LoadLevelOrNext(int id){
		
		id += nextLevelincrease;
		Debug.Log(id);
		
		//	SceneManager.LoadScene(id);
		StartCoroutine(WaitLoadLevel(id));
	}
		
	IEnumerator WaitLoadLevel(int x)
	{
		yield return new WaitForSeconds(2);
		SceneManager.LoadScene(x);
	}
}
