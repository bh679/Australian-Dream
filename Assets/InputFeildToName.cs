using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Leaderboard
{

	public class InputFeildToName : MonoBehaviour
	{
		
		public TMP_InputField inputField;
		
		public string[] firstName, secondName;
		bool newPBBool;
		
		public bool onStartGenerateName = true;
		
		void Start()
		{
			if(GetLeaderboard.Instance.myBest == null || string.IsNullOrEmpty(GetLeaderboard.Instance.myBest.name))
			{
			
				if(onStartGenerateName)
				{
					inputField.text = firstName[Random.Range(0,firstName.Length)] + " " + secondName[Random.Range(0,secondName.Length)];
				}
				
			}else
				inputField.text = GetLeaderboard.Instance.score.name = GetLeaderboard.Instance.myBest.name;
			
		}
		
		public void SetPlaceholderName(string newName){
			inputField.text = newName;
		}
		
		/*public void SetPlaceholderName()
		{
			if(GetLeaderboard.Instance.myBest == null || string.IsNullOrEmpty(GetLeaderboard.Instance.myBest.name))
			{
				inputField.text = firstName[Random.Range(0,firstName.Length)] + " " + secondName[Random.Range(0,secondName.Length)];
				
			}else
				inputField.text = GetLeaderboard.Instance.myBest.name;
			
			//bool newPBBool = (GetLeaderboard.Instance.score.score > GetLeaderboard.Instance.myBest.score);
			
			if(!GetLeaderboard.Instance.newBPBool)
			{
				this.gameObject.transform.parent.gameObject.SetActive(false);
				GetLeaderboard.Instance.gameObject.SetActive(true);
			}
			
		}*/
		
		public void SetName()
		{
			if(string.IsNullOrEmpty(inputField.text))
			{
				/*if(onStartGenerateName)
					laederboard.score.name = inputField.placeholder;
				else	*/
					GetLeaderboard.Instance.score.name = firstName[Random.Range(0,firstName.Length)] + " " + secondName[Random.Range(0,secondName.Length)];
			}else
				GetLeaderboard.Instance.score.name = inputField.text;
				
			GetLeaderboard.Instance.gameObject.SetActive(true);
		}
	}

}