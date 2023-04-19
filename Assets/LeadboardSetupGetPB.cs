using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leaderboard;

public class LeadboardSetupGetPB : MonoBehaviour
{
	
    // Start is called before the first frame update
    void Start()
    {
        
	    GetLeaderboard.Instance.Setup();
	    GetLeaderboard.Instance.GetLeaderboardPlz(false);
    
    }
}
