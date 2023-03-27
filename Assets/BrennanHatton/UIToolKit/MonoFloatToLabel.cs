
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace BrennanHatton.UnityTools.MonoFloat
{

	public class MonoFloatToLabel : MonoBehaviour
	{
		public UIDocument uiDocument;
		public string id;
		Label label;
		public MonoFloat monoFloat;
		
		void Reset(){
			uiDocument = GetComponent<UIDocument>();
			monoFloat = GameObject.FindAnyObjectByType<MonoFloat>();
		}
		
		public void OnEnable()
		{
			label = uiDocument.rootVisualElement.Q(id) as Label;
			
			if(monoFloat != null)
				label.text = monoFloat.GetFloat().ToString();
			
		}
			
		void Update()
		{
			if(monoFloat != null) label.text = monoFloat.GetFloat().ToString();
		}
	}

}