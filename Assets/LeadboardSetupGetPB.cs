using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leaderboard;

public class LeadboardSetupGetPB : MonoBehaviour
{
	public GetLeaderboard leaderboard;
	
	
    // Start is called before the first frame update
    void Start()
    {
        
	    leaderboard.Setup();
	    leaderboard.GetLeaderboardPlz(false);
    
    }
}
