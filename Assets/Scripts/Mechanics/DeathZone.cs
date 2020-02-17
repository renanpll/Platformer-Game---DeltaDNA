using System.Collections;
using System.Collections.Generic;
using Platformer.Gameplay;
using UnityEngine;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    /// <summary>
    /// DeathZone components mark a collider which will schedule a
    /// PlayerEnteredDeathZone event when the player enters the trigger.
    /// </summary>
    public class DeathZone : MonoBehaviour
    {

        public DeltaDNAManager deltaDNAManager;
        void OnTriggerEnter2D(Collider2D collider)
        {
            var p = collider.gameObject.GetComponent<PlayerController>();
            if (p != null)
            {
                //Sending the event DiedByDeathzone
                deltaDNAManager.KilledByDeathZone();
                deltaDNAManager.MissionFailed();

                var ev = Schedule<PlayerEnteredDeathZone>();
                ev.deathzone = this;

            }
        }
    }
}