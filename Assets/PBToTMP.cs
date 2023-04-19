using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Leaderboard
{
public class PBToTMP : MonoBehaviour
{
	public TMP_Text text;
	public GetLeaderboard leaderboard;

    // Update is called once per frame
    void Update()
    {
	    text.text = leaderboard.myBest.score.ToString();
    }
}

}