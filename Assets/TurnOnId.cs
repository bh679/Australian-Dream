using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TurnOnId : MonoBehaviour
{
	public UIDocument uiDocument;
	public string id;
	GroupBox groupBox;
		
	void Reset(){
		uiDocument = GetComponent<UIDocument>();
	}
	public void OnEnable()
	{
		groupBox = uiDocument.rootVisualElement.Q(id) as GroupBox;
			
	}
    
	public void TurnOn()
	{
		groupBox.style.display = DisplayStyle.Flex;
	}
    
	public void TurnOff()
	{
		groupBox.style.display = DisplayStyle.None;
	}
}
