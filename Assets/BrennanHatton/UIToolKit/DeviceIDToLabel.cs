using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Leaderboard;

namespace BrennanHatton.UnityTools
{
	public class DeviceIDToLabel : MonoBehaviour
	{
		public UIDocument uiDocument;
		public string id;
		Label label;
		
		void Reset(){
			uiDocument = GetComponent<UIDocument>();
		}
		bool ready = true;
		public void Update()
		{
			if(string.IsNullOrEmpty(GetLeaderboard.Instance.score.deviceId))
			{
				ready = true;
			}
			else if(ready)
			{
				ready = false;
				label = uiDocument.rootVisualElement.Q(id) as Label;
				label.text = GetLeaderboard.Instance.score.deviceId;
			}
			
		}
	}
}