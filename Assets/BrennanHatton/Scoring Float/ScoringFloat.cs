using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace BrennanHatton.Scoring
{
	
	public class ScoringFloat : MonoBehaviour
	{
		[SerializeField] 
		protected float value;
		public bool isStatic;
		protected static float staticValue;
		
		
		public virtual void UpdateScore(float scoreUpdate)
		{
			if(isStatic)
			{
				staticValue += scoreUpdate;
				value = staticValue;
			}
			else
				value += scoreUpdate;
		}
		
		public static virtual void UpdateScore(float scoreUpdate)
		{
			staticValue += scoreUpdate;
		}
		
		public virtual void UpdateScoreDeltaTime(float scoreUpdate)
		{
			UpdateScore(scoreUpdate*Time.deltaTime);
		}
		
		public virtual void SetScore(float newScore)
		{
			if(isStatic)
			{
				staticValue = newScore;
				value = staticValue;
			}
			else
				value = newScore;
		}
		
		public virtual float GetScore()
		{
			if(isStatic)
			{
				return staticValue;
			}
			else
				return value;
		}
	}
}