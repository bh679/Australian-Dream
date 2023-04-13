using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Leaderboard
{

	public class GetLeaderboard : MonoBehaviour
	{
		public string GETFunctionURL = "https://australiandreamleaderboard.azurewebsites.net/api/GetLeaderboard?code=BGGDR2LFFCPYObNE5F1CnVUFBz-IN4SeZZZAbta_tT6_AzFuPiSC7Q==",
			POSTFunctionURL = "https://australiandreamleaderboard.azurewebsites.net/api/AddScore?code=Hk5k0vQbkIEmuDSc_4f4Hly22u0PXHEIsQtEGSQcSTwMAzFu-PlFsg==";
		public LeaderboardUI UI;
		
		public LeaderboardSingle score;
	    // Start is called before the first frame update
	    void Start()
	    {
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