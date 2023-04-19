using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Leaderboard
{

	[System.Serializable]
	public class Leaderboard {
		public List<LeaderboardSingle> leaderboardSingleList;
	}
		
	[System.Serializable]
	public class LeaderboardSingle {
		public string name;
		public int score;
		public string deviceId;
		public long timeStamp;
	}
}