using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace BrennanHatton.UnityTools
{
	[System.Serializable]
	public class ClipSpriteData
	{
		public AudioClip clip;
		public Sprite sprite;
	}
	
	public class AudioClipToImage : MonoBehaviour
	{
		public UIDocument uiDocument;
		public string id;
		IMGUIContainer img;
		public AudioSource source;
		public ClipSpriteData[] data;
		
			
		void Reset(){
			uiDocument = GetComponent<UIDocument>();
			source = this.GetComponent<AudioSource>();
		}
			
		public void OnEnable()
		{
			img = uiDocument.rootVisualElement.Q(id) as IMGUIContainer;
				
			if(source.clip != null)
				img.style.backgroundImage = GetImage(source.clip);
				
			lastClip = source.clip;
				
		}
		
		AudioClip lastClip;
		
		// Update is called once per frame
		void Update()
		{
			if(source.clip != null && lastClip != source.clip)
			{
			
				StyleBackground background = GetImage(source.clip);
				lastClip = source.clip;
			
				if(background!= img.style.backgroundImage)
				{
					img.style.backgroundImage = background;
				}
			}
		}
		
		public StyleBackground GetImage(AudioClip clip)
		{
			for(int i = 0; i < data.Length; i++)
			{
				if(data[i].clip == clip)
					return new StyleBackground(data[i].sprite);
			}
			
			return null;
		}
	}
}