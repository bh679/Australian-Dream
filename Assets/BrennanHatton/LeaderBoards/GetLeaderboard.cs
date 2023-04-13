using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Leaderboard
{

	public class GetLeaderboard : MonoBehaviour
	{
		public TextAsset GetLeaderboardEndPoint, AddScoreEndPoint;
		
		public string GETFunctionURL,
			POSTFunctionURL;
		public LeaderboardUI UI;
		
		public LeaderboardSingle score;
	    // Start is called before the first frame update
	    void Start()
		{
			GETFunctionURL = GetLeaderboardEndPoint.text;
			POSTFunctionURL = AddScoreEndPoint.text;
	    	
		    GetLeaderboardPlz();
		    PostToLeaderbaord(score);
	    }
	    
		public void GetLeaderboardPlz()
		{
			WebRequests.Get(GETFunctionURL,
			(string error)=>{
				Debug.Log("Error: "+error);
			},
			(string response)=>{
				Debug.Log("Response: "+response);
				
				Leaderboard leaderboard = JsonUtility.FromJson<Leaderboard>(response);
				Debug.Log(leaderboard.leaderboardSingleList[0].name);
				
				UI.ShowLeaderboard(leaderboard);
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
			}
			);
		}
	}

}