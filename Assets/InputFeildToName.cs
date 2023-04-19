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
		bool newPBBool;
		
		public bool onStartGenerateName = true;
		
		void Start()
		{
			laederboard.Setup();
			laederboard.GetLeaderboardPlz(false);
			
			if(laederboard.myBest == null || string.IsNullOrEmpty(laederboard.myBest.name))
			{
			
				if(onStartGenerateName)
				{
					inputField.text = laederboard.score.name = firstName[Random.Range(0,firstName.Length)] + " " + secondName[Random.Range(0,secondName.Length)];
				}
				
			}else
				inputField.text = laederboard.myBest.name;
			
		}
		
		public void SetPlaceholderName()
		{
			if(laederboard.myBest == null || string.IsNullOrEmpty(laederboard.myBest.name))
			{
				inputField.text = firstName[Random.Range(0,firstName.Length)] + " " + secondName[Random.Range(0,secondName.Length)];
				
			}else
				inputField.text = laederboard.myBest.name;
			
			bool newPBBool = (laederboard.score.score > laederboard.myBest.score);
			
			if(!newPBBool)
			{
				this.gameObject.transform.parent.gameObject.SetActive(false);
				laederboard.gameObject.SetActive(true);
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