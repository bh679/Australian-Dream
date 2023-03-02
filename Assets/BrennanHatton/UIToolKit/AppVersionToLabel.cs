using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace BrennanHatton.UnityTools
{
	public class AppVersionToLabel : MonoBehaviour
	{
		public UIDocument uiDocument;
		public string id;
		Label label;
		
		void Reset(){
			uiDocument = GetComponent<UIDocument>();
		}
		public void OnEnable()
		{
			label = uiDocument.rootVisualElement.Q(id) as Label;
			label.text = Application.version;
			
		}
	}
}