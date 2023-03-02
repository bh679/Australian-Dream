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
			
			_button.RegisterCallback<MouseDownEvent>(MyMouseDownEvent,TrickleDown.TrickleDown);
			_button.RegisterCallback<MouseUpEvent>(MyMouseUpEvent,TrickleDown.TrickleDown);
			
			keyboard = InputSystem.GetDevice<Keyboard>();
			pressState = new KeyboardState();
			releaseState = new KeyboardState();
			pressState.Press(key);
			releaseState.Release(key);
 
		}
		
		private void MyMouseDownEvent(MouseDownEvent evt)
		{
			/*bool leftMouseButtonPressed = 0 != (evt.pressedButtons & (1 << (int)UnityEngine.UIElements.MouseButton.LeftMouse));
			bool rightMouseButtonPressed = 0 != (evt.pressedButtons & (1 << (int)UnityEngine.UIElements.MouseButton.RightMouse));
			bool middleMouseButtonPressed = 0 != (evt.pressedButtons & (1 << (int)UnityEngine.UIElements.MouseButton.MiddleMouse));
			
			Debug.Log($"Mouse Down event. Triggered by {(UnityEngine.UIElements.MouseButton)evt.button}.");
			*/
			onPress.Invoke();
			InputSystem.QueueStateEvent(keyboard, pressState);
		}
		
		private void MyMouseUpEvent(MouseUpEvent evt)
		{
			/*bool leftMouseButtonPressed = 0 != (evt.pressedButtons & (1 << (int)UnityEngine.UIElements.MouseButton.LeftMouse));
			bool rightMouseButtonPressed = 0 != (evt.pressedButtons & (1 << (int)UnityEngine.UIElements.MouseButton.RightMouse));
			bool middleMouseButtonPressed = 0 != (evt.pressedButtons & (1 << (int)UnityEngine.UIElements.MouseButton.MiddleMouse));
			
			Debug.Log($"Mouse Up event. Triggered by {(UnityEngine.UIElements.MouseButton)evt.button}.");
			*/
			onRelease.Invoke();
			InputSystem.QueueStateEvent(keyboard, releaseState);
		}
		
		public void OnDisable()
		{
			_button.UnregisterCallback<MouseDownEvent>(MyMouseDownEvent);
			_button.UnregisterCallback<MouseUpEvent>(MyMouseUpEvent);
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