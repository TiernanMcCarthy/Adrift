using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Fusion;

public class PlayerInfo : MonoBehaviour
{
    public TMP_Text playerName;

    public TMP_Text joinTime;

    public PlayerDetails assignedPlayer;


    public void AddPlayer(PlayerDetails player)
    {
        assignedPlayer=player;
        transform.parent = Leaderboard.instance.contentTransform;
        playerName.text = player.playerName;
        joinTime.text = player.joinTime;
    }

    void UpdateDetails()
    {
        if (assignedPlayer != null)
        {
            playerName.text = assignedPlayer.playerName;
            joinTime.text = assignedPlayer.joinTime;
        }
    }

    public void MovePlayer()
    {
        Transform spawnPoint = Leaderboard.instance.GetSpawnTransform();
        if (spawnPoint != null)
        {
            assignedPlayer.MoveAndLookAtRPC(spawnPoint.transform.position, spawnPoint.transform.forward);
        }
    }

    public void ViewPlayer()
    {
        PictureDisplay.instance.SetTarget(assignedPlayer);
        PictureDisplay.instance.ShowPIP();
    }

    public void LateUpdate()
    {
       // base.FixedUpdateNetwork();
       
        UpdateDetails();
    }
}
