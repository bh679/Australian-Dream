using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace BrennanHatton.UnityTools.MonoFloat
{

	public class UIElementFader : MonoBehaviour
	{
		public UIDocument uiDocument;
		public string defaultId;
		public float defaultTime = 2f, defaultTarget = 0f;
		
		VisualElement defaultElement;
		
		void Reset(){
			uiDocument = GetComponent<UIDocument>();
		}
		
		public void OnEnable()
		{
			defaultElement = uiDocument.rootVisualElement.Q(defaultId);
		}
		
		/*public void StartFadeToBlack(float time)
		{
			StartCoroutine(FadeToBlack(time));
		}
		
		IEnumerator FadeToBlack(float time)
		{
			for(int i = 0; i < time*25; i++)
			{
				yield return new WaitForSeconds((time/25f) * Time.deltaTime);
				
				image.color = new Color(0,0,0,i/(time*25));
			}
			image.color = new Color(0,0,0,255);
		}*/
		
		
		public void StartFadeToOpaque(float time)
		{
			StartCoroutine(FadeToOpaque(defaultElement,time));
		}
		
		public void FadeToTarget(string id)
		{
			StartCoroutine(FadeToTarget(uiDocument.rootVisualElement.Q(id),defaultTarget,defaultTarget));
		}
		
		
		IEnumerator FadeToTarget(VisualElement element, float time, float target)
		{
			float startOpacity = element.style.opacity.value;
			float dif;
			
			if(startOpacity < target)
			{
				
				dif = target - startOpacity;
				
				for(int i = 0; i < time*10; i++)
				{
					yield return new WaitForSeconds((time/10f) * Time.deltaTime);
					
					element.style.opacity = new StyleFloat(
						startOpacity+(i/(time*10))*dif
					);
				}
			}else if(startOpacity > target)
			{
				
				dif = startOpacity - target;
				
				for(int i = 0; i < time*10; i++)
				{
					yield return new WaitForSeconds((time/10f) * Time.deltaTime);
					
					element.style.opacity = new StyleFloat(
						startOpacity-(i/(time*10))*dif
					);
				}
			}
			
			element.style.opacity = new StyleFloat(target);
			
			yield return null;
		}
		
		IEnumerator FadeToOpaque(VisualElement element, float time)
		{
			float opacity = element.style.opacity.value;
			
			for(int i = 0; i < time*10; i++)
			{
				yield return new WaitForSeconds((time/10f) * Time.deltaTime);
				
				//image.color = new Color(255,255,255,i/(time*25));
				opacity = i/(time*10);
				element.style.opacity = new StyleFloat(opacity);
			}
			element.style.opacity = new StyleFloat(100);
		}
		
		public void StartFadeToClear(float time)
		{
			StartCoroutine(FadeToClear(defaultElement,time));
		}
		
		IEnumerator FadeToClear(VisualElement element, float time)
		{
			float opacity = element.style.opacity.value;
			
			for(int i = 0; i < time*10; i++)
			{
				yield return new WaitForSeconds((time/10f) * Time.deltaTime);
				
				opacity = 1-i/(time*10);
				element.style.opacity = new StyleFloat(opacity);
			}
			element.style.opacity = new StyleFloat(0f);
		}
	}

}