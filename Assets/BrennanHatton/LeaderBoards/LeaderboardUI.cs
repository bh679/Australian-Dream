/* 
------------------- Code Monkey -------------------

Thank you for downloading this package
I hope you find it useful in your projects
If you have any questions let me know
Cheers!

unitycodemonkey.com
--------------------------------------------------
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using CodeMonkey.Utils;

namespace Leaderboard
{

	public class LeaderboardUI : MonoBehaviour {
	
		public static LeaderboardUI Instance { get; private set; }
	
		public Color GoldColor, SilverColor, BronzColor;
	
		[SerializeField] private GameObject loadingGameObject;
	
		LeaderboardSingle myScore, pb;
	
		private Transform entryContainer;
		private Transform entryTemplate;
		private List<Transform> highscoreEntryTransformList;
	
		private void Awake() {
			Instance = this;
	
			GetComponent<RectTransform>().anchoredPosition = Vector2.zero; // Reset Position
			GetComponent<RectTransform>().sizeDelta = Vector2.zero; // Reset Size
	
			entryContainer = transform.Find("highscoreEntryContainer");
			entryTemplate = entryContainer.Find("highscoreEntryTemplate");
	
			entryTemplate.gameObject.SetActive(false);
	
			Hide();
		}
	
		public void Hide() {
			gameObject.SetActive(false);
		}
	
		public void Show() {
			gameObject.SetActive(true);
			transform.SetAsLastSibling();
		}
	
		public void ShowLoading() {
			gameObject.SetActive(true);
			loadingGameObject.SetActive(true);
		}
	
		int rank = 0;
		private void CreateHighscoreEntryTransform(LeaderboardSingle leaderboardSingle, Transform container, List<Transform> transformList) {
			
			bool isMyScore = string.Compare(leaderboardSingle.deviceId,myScore.deviceId) == 0;
			
			float templateHeight = 31f;
			rank = rank + 1;//transformList.Count + 1;
			
			if(rank > 12 && !isMyScore)
				return;
			
			Transform entryTransform = Instantiate(entryTemplate, container);
			RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
			entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
			entryTransform.gameObject.SetActive(true);
	
			string rankString;
			switch (rank) {
			default:
				rankString = rank + "TH"; break;
	
			case 1: rankString = "1ST"; break;
			case 2: rankString = "2ND"; break;
			case 3: rankString = "3RD"; break;
			}
	
			entryTransform.Find("posText").GetComponent<Text>().text = rankString;
	
			int score = leaderboardSingle.score;
	
			entryTransform.Find("scoreText").GetComponent<Text>().text = score.ToString();
	
			string name = leaderboardSingle.name;
			entryTransform.Find("nameText").GetComponent<Text>().text = name;
	
			// Set background visible odds and evens, easier to read
			if(rank > 12 && isMyScore)
				entryTransform.Find("background").gameObject.SetActive(true);
			else
				entryTransform.Find("background").gameObject.SetActive(rank % 2 == 1);
	        
			// Highlight Mine
			if (isMyScore) {
				entryTransform.Find("posText").GetComponent<Text>().color = Color.green;
				entryTransform.Find("scoreText").GetComponent<Text>().color = Color.green;
				entryTransform.Find("nameText").GetComponent<Text>().color = Color.green;
			}
	
			// Set tropy
			switch (rank) {
			default:
				entryTransform.Find("trophy").gameObject.SetActive(false);
				break;
			case 1:
				entryTransform.Find("trophy").GetComponent<Image>().color = GoldColor;//UtilsClass.GetColorFromString("FFD200");
				break;
			case 2:
				entryTransform.Find("trophy").GetComponent<Image>().color = SilverColor;//UtilsClass.GetColorFromString("C6C6C6");
				break;
			case 3:
				entryTransform.Find("trophy").GetComponent<Image>().color = BronzColor;//UtilsClass.GetColorFromString("B76F56");
				break;
	
			}
	
			transformList.Add(entryTransform);
		}
	
		//bool personalBestBool;
		public void ShowLeaderboard(Leaderboard leaderboard, LeaderboardSingle _pb,  LeaderboardSingle _myScore) {
			myScore = _myScore;
			pb = _pb;
			
			if(myScore.score < pb.score || pb.score == 0)
			{
				leaderboard.leaderboardSingleList.Add(myScore);
			}
			
			rank = 0;
			
			gameObject.SetActive(true);
			loadingGameObject.SetActive(false);
	
			// Sort entry list by Score
			for (int i = 0; i < leaderboard.leaderboardSingleList.Count; i++) {
				for (int j = i + 1; j < leaderboard.leaderboardSingleList.Count; j++) {
					if (leaderboard.leaderboardSingleList[j].score > leaderboard.leaderboardSingleList[i].score) {
						// Swap
						LeaderboardSingle tmp = leaderboard.leaderboardSingleList[i];
						leaderboard.leaderboardSingleList[i] = leaderboard.leaderboardSingleList[j];
						leaderboard.leaderboardSingleList[j] = tmp;
					}
				}
			}
	
			if (highscoreEntryTransformList != null) {
				foreach (Transform highscoreEntryTransform in highscoreEntryTransformList) {
					Destroy(highscoreEntryTransform.gameObject);
				}
			}
	
			highscoreEntryTransformList = new List<Transform>();
			foreach (LeaderboardSingle leaderboardSingle in leaderboard.leaderboardSingleList) {
				CreateHighscoreEntryTransform(leaderboardSingle, entryContainer, highscoreEntryTransformList);
			}
		}
	
	}

}