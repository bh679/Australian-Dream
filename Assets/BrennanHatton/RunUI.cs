using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
//using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

namespace BrennanHatton.UnityTools.UI
{
	
	[System.Serializable]
	public class UIButton
	{
		public string id;
		public Key key;
		public UnityEvent onPress, onRelease;
		private Button _button;
		public InputEventPtr inputEventPtr;
		//public UnityEngine.UIElements.MouseButton[] mouseButton = { UnityEngine.UIElements.MouseButton.LeftMouse };
		
		Keyboard keyboard;
		KeyboardState pressState, releaseState;
		
		public void OnEnable(UIDocument uiDocument)
		{
			_button = uiDocument.rootVisualElement.Q(id) as Button;
			
			Debug.Log(_button);
			
			_button.RegisterCallback<PointerDownEvent>(MyPointerDownEvent,TrickleDown.TrickleDown);
			_button.RegisterCallback<PointerUpEvent>(MyPointerUpEvent,TrickleDown.TrickleDown);
			
			keyboard = InputSystem.GetDevice<Keyboard>();
			InputSystem.ResetDevice(keyboard);
			pressState = new KeyboardState();
			releaseState = new KeyboardState();
			pressState.Press(key);
			releaseState.Release(key);
 
		}
		
		private void MyPointerDownEvent(PointerDownEvent evt)
		{
			onPress.Invoke();
			InputSystem.QueueStateEvent(keyboard, pressState);
		}
		
		private void MyPointerUpEvent(PointerUpEvent evt)
		{
			onRelease.Invoke();
			InputSystem.QueueStateEvent(keyboard, releaseState);
		}
		
		public void OnDisable()
		{
			_button.UnregisterCallback<PointerDownEvent>(MyPointerDownEvent);
			_button.UnregisterCallback<PointerUpEvent>(MyPointerUpEvent);
		}
	}
	
	public class RunUI : MonoBehaviour
	{
		public UIDocument uiDocument;
		public UIButton[] buttons;
	
		void Reset(){
			uiDocument = GetComponent<UIDocument>();
		}
	
		//Add logic that interacts with the UI controls in the `OnEnable` methods
		private void OnEnable()
		{
			Debug.Log("OnEnable " + buttons.Length);
			for(int i = 0; i < buttons.Length; i++)
			{
				Debug.Log(i);
				buttons[i].OnEnable(uiDocument);
			}
		}
		
		private void OnDisable()
		{
			for(int i = 9; i < buttons.Length; i++)
			{
				buttons[i].OnDisable();
			}
		}
	}
}