using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BrennanHatton.UnityTools.MonoFloat;

namespace Leaderboard
{

	public class GetLeaderboard : MonoBehaviour
	{
		public TextAsset GetLeaderboardEndPoint, AddScoreEndPoint;
		
		public string GETFunctionURL,
			POSTFunctionURL;
		public LeaderboardUI UI;
		public InputFeildToName nameGenerator;
		
		public LeaderboardSingle score, myBest;
		public MonoFloat monoFloat;
		
		public bool newBPBool;
		
		public void Setup()
		{
			GETFunctionURL = GetLeaderboardEndPoint.text;
			POSTFunctionURL = AddScoreEndPoint.text;
			score.deviceId = GetDeviceID();
			score.score = (int)monoFloat.GetFloat();
			score.timeStamp = System.DateTime.Now.Ticks;
		}
	    // Start is called before the first frame update
	    void Start()
		{
			Setup();
			
			//score.score = (int)monoFloat.GetFloat();
			//score.timeStamp = System.DateTime.Now.Ticks;
			
			if(newBPBool)
			{
				myBest = score;
				PostToLeaderbaord(myBest);
			}
			else
			{
				GetLeaderboardPlz(true);
			}
		   
		}
	    
		const string DEVICEIDKEY = "DEVICEID";
		public string GetDeviceID()
		{
			string id = null;//SystemInfo.deviceUniqueIdentifier;
			
			if(PlayerPrefs.HasKey(DEVICEIDKEY))
			{
				id = PlayerPrefs.GetString(DEVICEIDKEY);
				return id;
			}else
				id = GetRandomString(32);
				
			
		
			PlayerPrefs.SetString(DEVICEIDKEY,id);
			PlayerPrefs.Save();
			
			
			return id;
			
		}
		
		public static string GetRandomString(int length)
		{
			var chars = new char[length];
			for (int i = 0; i < length; i++)
			{
				chars[i] = (char)(Random.Range(32, 127));
			}
			return new string(chars);
		}
	    
		public void GetLeaderboardPlz(bool show)
		{
			WebRequests.Get(GETFunctionURL,
			(string error)=>{
				Debug.Log("Error: "+error);
			},
			(string response)=>{
				Debug.Log("Response: "+response);
				
				Leaderboard leaderboard = JsonUtility.FromJson<Leaderboard>(response);
				Debug.Log(leaderboard.leaderboardSingleList[0].name);
				
				if(show)
					UI.ShowLeaderboard(leaderboard, myBest, score);
				else
				{
					for(int i = 0; i < leaderboard.leaderboardSingleList.Count; i++)
					{
						if(leaderboard.leaderboardSingleList[i].deviceId == score.deviceId)
						{
							myBest = leaderboard.leaderboardSingleList[i];
							score.name = myBest.name;
							newBPBool = (score.score > myBest.score);
							
							nameGenerator.SetPlaceholderName(myBest.name);
							
							if(!newBPBool)
							{
								nameGenerator.transform.parent.parent.gameObject.SetActive(false);
								this.gameObject.SetActive(true);
							}
							return;
						}
					}
				}
			}
			);
			
		}
		
		public void PostToLeaderbaord(LeaderboardSingle single)
		{
			WebRequests.PostJson(POSTFunctionURL, JsonUtility.ToJson(single),
			(string error)=>{
				Debug.Log("Error: "+error);
			},
			(string response)=>{
				Debug.Log("Response: "+response);
	    	
				GetLeaderboardPlz(true);
			}
			);
		}
	}

}