using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class CanvasScore : MonoBehaviour
{
    public GameObject PanelSubmit;
    public GameObject PanelRanking;

    public TMP_InputField nameText;
    //public TMP_InputField scoreText;

    public PlayerInfo[] rankPlayers;

    public PlayerInfo currentPlayer;

    public static CanvasScore instance;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if(PlayerName.Text != string.Empty)
        {
            nameText.text = PlayerName.Text;
        }
    }

    public void SaveScore()
    {
        if (string.IsNullOrWhiteSpace(nameText.text))
        {
            nameText.text = "Bubbly";
        }
        else
        {
            PlayerName.Text = nameText.text;
        }

        LeaderboardEntry entry = new LeaderboardEntry(name, "00:00");

        ScoreManager.instance.RefreshScores();
        PanelSubmit.SetActive(false);
        PanelRanking.SetActive(true);
    }

    public void UpdateScoreboard(List<LeaderboardEntry> list)
    {

        List<LeaderboardEntry> newList = list.OrderByDescending(data => data.time).ToList();

        ClearTextPlayer();
        ShowRankPlayers(ref newList);
        ShowCurrentPositionPlayer(ref  newList);
    }

    private void ShowCurrentPositionPlayer(ref List<LeaderboardEntry> newList)
    {
        if (ScoreManager.instance.id != string.Empty && newList.Count > 0)
        {
            LeaderboardEntry player = newList.First(data => data.id == ScoreManager.instance.id);
            int playerScorePosition = newList.FindIndex(data => data.id == ScoreManager.instance.id);
            currentPlayer.textPlace.text = (playerScorePosition + 1).ToString();
            currentPlayer.novoTextName.text = player.name;
            currentPlayer.novotextScore.text = player.time;

        }
    }

    private void ShowRankPlayers(ref List<LeaderboardEntry> newList)
    {
        int index = 0;
        while (index < newList.Count && index < 10)
        {
            rankPlayers[index].textPlace.text = (index + 1).ToString();
            rankPlayers[index].novoTextName.text = newList[index].name.ToString();
            rankPlayers[index].novotextScore.text = newList[index].time;
            index++;
        }
    }

    private void ClearTextPlayer()
    {

        for (int i = 0; i < rankPlayers.Length; i++)
        {
            rankPlayers[i].textPlace.text = (i + 1).ToString();
            rankPlayers[i].novoTextName.text = string.Empty;
            rankPlayers[i].novotextScore.text = string.Empty;
        }

        currentPlayer.textPlace.text = string.Empty;
        currentPlayer.novoTextName.text = string.Empty;
        currentPlayer.novotextScore.text = string.Empty;
    }

    public void PlayAgain()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    public void GoBackMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void Refresh()
    {
        ScoreManager.instance.RefreshScores();
    }


    public void NextRanking()
    {
        // TODO
    }

}
