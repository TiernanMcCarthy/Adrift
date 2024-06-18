using Fusion;
using Fusion.Addons.ConnectionManagerAddon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManagement : MonoBehaviour
{

    public static PlayerManagement instance;

    public List<PlayerDetails> playerList = new List<PlayerDetails>();

    public PlayerDetails localPlayer;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        if(instance==null)
        {
            instance = this;
        }
        else if(instance!=this)
        {
            Destroy(gameObject);
        }
    }

    public PlayerDetails GetPlayerFromPlayerRef(PlayerRef pr)
    {
        foreach(PlayerDetails pd in playerList)
        {
            if (pd.playerRef == pr)
            {
                return pd;
            }
        }
        return null;
    }

    public void RecreatePlayerList()
    {
        playerList.Clear();
        PlayerDetails[] temp = FindObjectsOfType<PlayerDetails>();

        foreach(PlayerDetails playerDetails in temp)
        {
            if(playerDetails.HasInputAuthority)
            {
                localPlayer = playerDetails;
            }
            playerList.Add(playerDetails);
        }

        //Leaderboard.instance.CreateLeaderboard(playerList);
    }


    

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Backspace))
        {
            
            localPlayer.LoadSceneRPC();
            
        }
    }
}
