using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LeaderboardScript : MonoBehaviour
{

    // Obtain reference to the Singleton Game State object
    private GameState gameState = GameState.GetGameState();
    private Transform entryContainer;
    private Transform entryTemplate;
    private List<Transform> leaderboardEntryTransformList;

    private void Awake() {

        entryContainer = transform.Find("LeaderboardEntryContainer");
        entryTemplate = entryContainer.Find("LeaderboardEntryTemplate");

        entryTemplate.gameObject.SetActive(false);

        // Subscribe to the OnRaceOver action in the RaceController
        // gameState.GetRaceController().OnRaceOver += TestHandler;//AddScoreToLeaderboard;

        AddScoreToLeaderboard(gameState.currentLeaderboardEntry.name, gameState.currentLeaderboardEntry.time);

        string loadedScoresJson = PlayerPrefs.GetString("leaderboard");
        LeaderboardScores loadedScores = JsonUtility.FromJson<LeaderboardScores>(loadedScoresJson);
        loadedScores.SortScores();

        leaderboardEntryTransformList = new List<Transform>();

        for (int i = 0; i < loadedScores.leaderboardEntryList.Count; i++) {
            // Only display first 10 entries
            if (i == 10) { break; };
            CreateLeaderboardTransform(loadedScores.leaderboardEntryList[i], entryContainer, leaderboardEntryTransformList);
        }
    }

    private void CreateLeaderboardTransform(LeaderboardEntry leaderboardEntry, Transform container, List<Transform> transformList)
    {
        float templateHeight = 40f;

        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString = rank.ToString() + ".";

        entryTransform.Find("PosEntry").GetComponent<TextMeshProUGUI>().text = rankString;
        entryTransform.Find("NameEntry").GetComponent<TextMeshProUGUI>().text = leaderboardEntry.name;
        entryTransform.Find("TimeEntry").GetComponent<TextMeshProUGUI>().text = leaderboardEntry.time.ToString();

        transformList.Add(entryTransform);
    }

    private void AddScoreToLeaderboard(string n, float t)
    {
        Debug.Log("adding to leaderboard " + n + t);

        // Create entry
        LeaderboardEntry leaderboardEntry = new LeaderboardEntry { name = n, time = t};

        // Load saved scores
        string jsonLeaderboard = PlayerPrefs.GetString("leaderboard");

        // Add new entry to leaderboard
        LeaderboardScores leaderboardScores = null;
        if (jsonLeaderboard.Length > 0) {
            leaderboardScores = JsonUtility.FromJson<LeaderboardScores>(jsonLeaderboard);
            leaderboardScores.leaderboardEntryList.Add(leaderboardEntry);
        } else {
            leaderboardScores = new LeaderboardScores { leaderboardEntryList = new List<LeaderboardEntry> { leaderboardEntry } };
        }

        // Save updated scores
        string leaderboardJSON = JsonUtility.ToJson(leaderboardScores);
        PlayerPrefs.SetString("leaderboard", leaderboardJSON);
        PlayerPrefs.Save();
    }

    [System.Serializable]
    public class LeaderboardEntry
    {
        public string name;
        public float time;
    }

    private class LeaderboardScores
    {
        public List<LeaderboardEntry> leaderboardEntryList;

        public void SortScores()
        {
            leaderboardEntryList.Sort((x,y) => x.time.CompareTo(y.time));
        }
    }

    private void TestHandler(string s, float f)
    {
        Debug.Log("s: " + s + "f: " + f);
    }

}
