using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LeaderboardScript : MonoBehaviour
{

    private Transform entryContainer;
    private Transform entryTemplate;
    // private List<LeaderboardEntry> leaderboardEntryList;
    private List<Transform> leaderboardEntryTransformList;

    private void Awake() {
        entryContainer = transform.Find("LeaderboardEntryContainer");
        entryTemplate = entryContainer.Find("LeaderboardEntryTemplate");

        entryTemplate.gameObject.SetActive(false);

        string loadedScoresJson = PlayerPrefs.GetString("leaderboard");
        LeaderboardScores loadedScores = JsonUtility.FromJson<LeaderboardScores>(loadedScoresJson);
        loadedScores.SortScores();

        leaderboardEntryTransformList = new List<Transform>();

        foreach (LeaderboardEntry leaderboardEntry in loadedScores.leaderboardEntryList) {
            CreateLeaderboardTransform(leaderboardEntry, entryContainer, leaderboardEntryTransformList);
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
        // Create entry
        LeaderboardEntry leaderboardEntry = new LeaderboardEntry { name = n, time = t};

        // Load saved scores
        string jsonLeaderboard = PlayerPrefs.GetString("leaderboard");
        LeaderboardScores loadedScores = JsonUtility.FromJson<LeaderboardScores>(jsonLeaderboard);

        // Add new entry to leaderboard
        loadedScores.leaderboardEntryList.Add(leaderboardEntry);

        // Save updated scores
        string leaderboardJSON = JsonUtility.ToJson(loadedScores);
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

}
