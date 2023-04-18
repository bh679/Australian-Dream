using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Leaderboard
{

	public class InputFeildToName : MonoBehaviour
	{
		
		public TMP_InputField inputField;
		public GetLeaderboard laederboard;
		public string[] firstName, secondName;
		
		public bool onStartGenerateName = true;
		
		void Start()
		{
			if(onStartGenerateName)
			{
				inputField.text = laederboard.score.name = firstName[Random.Range(0,firstName.Length)] + " " + secondName[Random.Range(0,secondName.Length)];
			}
		}
		
		public void SetName()
		{
			if(string.IsNullOrEmpty(inputField.text))
			{
				/*if(onStartGenerateName)
					laederboard.score.name = inputField.placeholder;
				else	*/
					laederboard.score.name = firstName[Random.Range(0,firstName.Length)] + " " + secondName[Random.Range(0,secondName.Length)];
			}else
				laederboard.score.name = inputField.text;
				
			laederboard.gameObject.SetActive(true);
		}
	}

}