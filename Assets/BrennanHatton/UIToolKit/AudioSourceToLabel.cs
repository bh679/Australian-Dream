using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace BrennanHatton.UnityTools
{

	public class AudioSourceToLabel : MonoBehaviour
	{
		public UIDocument uiDocument;
		public string id;//SongTitleGraphicIMGUIContainer
		Label label;
		public AudioSource source;
		
		void Reset(){
			uiDocument = GetComponent<UIDocument>();
			source = this.GetComponent<AudioSource>();
		}
		
		public void OnEnable()
		{
			label = uiDocument.rootVisualElement.Q(id) as Label;
			
			if(source.clip != null)
				label.text = source.clip.name;
			
		}
	
		// Update is called once per frame
		void Update()
		{
			if(source.clip != null && source.clip.name != label.text )
			{
				label.text = source.clip.name;
			}
		}
	}
}
