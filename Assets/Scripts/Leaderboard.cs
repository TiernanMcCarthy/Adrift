using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour
{

    public static Leaderboard instance;

    public PlayerInfo infoPrefab;

    public List<PlayerInfo> leaderBoard;

    public Transform contentTransform;

    public List<Transform> spawnPoints;


    public TMP_Dropdown spawnLocation;
    public void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

    }

    public Transform GetSpawnTransform()
    {
        if(spawnLocation.value<spawnPoints.Count)
        {
            return spawnPoints[spawnLocation.value];
        }
        return null;
    }

    public void ClearLeaderboard()
    {
        foreach(var player in leaderBoard)
        {
            Destroy(player.gameObject);
        }

        leaderBoard.Clear();
    }
    public void CreateLeaderboard(List<PlayerDetails> players)
    {
        ClearLeaderboard();

        leaderBoard = new List<PlayerInfo>();

        foreach(var player in players)
        {
            PlayerInfo temp = Instantiate(infoPrefab);
            temp.transform.SetParent(contentTransform);
            temp.AddPlayer(player);
            leaderBoard.Add(temp);
        }
    }
  
}
