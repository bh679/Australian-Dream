using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BrennanHatton.UnityTools.MonoFloat;

namespace BrennanHatton.Discord
{
	public class DiscordLogMonoFloat : MonoBehaviour
	{
		public string preMessage, postMessage;
		public MonoFloat monoFloat;
		
		public void SendFloatMessage(string message)
		{
			DiscordLogManager.Instance.SendMessage(preMessage + message+ monoFloat.GetFloat().ToString() + postMessage);
		}
    }
}
