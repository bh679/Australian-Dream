using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace BrennanHatton.UnityTools
{

	public class AudioSourceToText : MonoBehaviour
	{
		public TMP_Text text;
		public AudioSource source;
		
		
		void Reset()
		{
			text = this.GetComponent<TMP_Text>();
			source = this.GetComponent<AudioSource>();
		}
		
	    // Start is called before the first frame update
	    void Start()
		{
			if(source.clip != null)
				text.text = source.clip.name;
	    }
	
	    // Update is called once per frame
	    void Update()
	    {
		    if(source.clip != null && source.clip.name != text.text )
		    {
		    	text.text = source.clip.name;
		    }
	    }
	}
}
