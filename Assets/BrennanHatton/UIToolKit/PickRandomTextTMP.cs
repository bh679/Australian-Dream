using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace BrennanHatton.UnityTools.LegacyUI
{

	public class PickRandomTextTMP : MonoBehaviour {
		
		public List<string> TextOptions = new List<string>();
		
		public bool OnStart = true;
		
		public TMP_Text TextBox;
		
		void Reset()
		{
			TextBox = this.GetComponent<TMP_Text>();
		}
		
		// Use this for initialization
		void Start () {
			if (OnStart)
				RandomText ();
		}
		
		// Update is called once per frame
		void Update () {
			
		}
		
		public void RandomText()
		{
			if(TextOptions.Count == 0)
				return;
			
			TextBox.text = TextOptions[Random.Range(0,TextOptions.Count-1)];
		}
	}

}