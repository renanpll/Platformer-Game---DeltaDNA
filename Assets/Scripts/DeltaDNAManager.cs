using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DeltaDNA;
using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;
using static Platformer.Core.Simulation;

public class DeltaDNAManager : MonoBehaviour
{
    //Counter for how many tokens the player picked up
    int tokensPickedUp, playerLives;

    // Start is called before the first frame update
    void Start()
    {
        //DeltaDNA initialization
        DDNA.Instance.SetLoggingLevel(DeltaDNA.Logger.Level.DEBUG);
        DDNA.Instance.ClientVersion = "1.0.0";
        

        //Lauch the DeltaDNA SDK
        DDNA.Instance.StartSDK("player_03");

        tokensPickedUp = 0;
        playerLives = 3;

        //Sending the event MissionStarted()
        MissionStarted();

    }

    public void MissionStarted()
    {
        GameEvent myGameEvent = new GameEvent("missionStarted")
            .AddParam("missionName", "MainMission")
            .AddParam("isTutorial", false);

        DDNA.Instance.RecordEvent(myGameEvent);
    }

    public void MissionCompleted()
    {
        GameEvent myGameEvent = new GameEvent("missionCompleted")
            .AddParam("missionName", "MainMission")
            .AddParam("isTutorial", false)
            .AddParam("reward", new Params()
                .AddParam("rewardName", "MissionRewards")
                .AddParam("rewardProducts", new Product()
                    .AddVirtualCurrency("Token", "GRIND", tokensPickedUp)));

        DDNA.Instance.RecordEvent(myGameEvent);
    }

    public void PlayerJumped()
    {
        GameEvent myGameEvent = new GameEvent("playerJumped");

        DDNA.Instance.RecordEvent(myGameEvent);
    }

    public void EnemyKilled(GameObject enemy)
    {
        GameEvent myGameEvent = new GameEvent("playerKill")
            .AddParam("targetType", "NPC")
            .AddParam("targetID", enemy.name);

        DDNA.Instance.RecordEvent(myGameEvent);
    }

    public void KilledByDeathZone()
    {
        GameEvent myGameEvent = new GameEvent("playerDefeated")
            .AddParam("defeatedByName", "Death Zone");

        DDNA.Instance.RecordEvent(myGameEvent);
    }

    public void KilledByEnemy()
    {
        GameEvent myGameEvent = new GameEvent("playerDefeated")
            .AddParam("defeatedByName", "Enemy");

        DDNA.Instance.RecordEvent(myGameEvent);
    }

    public void TokenPickedUp()
    {
        tokensPickedUp++;
        GameEvent myGameEvent = new GameEvent("itemCollected");

        DDNA.Instance.RecordEvent(myGameEvent);
    }

    public void MenuOpened()
    {
        GameEvent myGameEvent = new GameEvent("options")
            .AddParam("action", "OpenMenu")
            .AddParam("option", "MainMenu");

        DDNA.Instance.RecordEvent(myGameEvent);
    }

    public void MissionFailed()
    {
        playerLives--;

        if (playerLives <= 0)
        {
            GameEvent myGameEvent = new GameEvent("missionFailed")
                .AddParam("missionName", "MainMission")
                .AddParam("isTutorial", false);

            DDNA.Instance.RecordEvent(myGameEvent);
        }
    }
}
