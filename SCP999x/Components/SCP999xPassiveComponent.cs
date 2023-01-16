using UnityEngine;
using MEC;
using System.Collections.Generic;
using Exiled.API.Features;
using Exiled.Events.EventArgs;
using System.Collections;
using CustomPlayerEffects;

namespace SCP999x.Components
{
    public class SCP999xPassiveComponent : MonoBehaviour
    {
        private Player player;
        CoroutineHandle coro;

        public void Awake()
        {
            player = Player.Get(gameObject);//plugin.abilityUser;
            coro = Timing.RunCoroutine(passiveHeal());

            Exiled.Events.Handlers.Player.Hurting += OnHurt;
            Exiled.Events.Handlers.Player.ReceivingEffect += OnReceivingEffect;
        }
        public void OnDestroy()
        {
            Exiled.Events.Handlers.Player.Hurting -= OnHurt;
            Exiled.Events.Handlers.Player.ReceivingEffect -= OnReceivingEffect;
            
            Timing.KillCoroutines(coro);
            player = null;
        }


        public void OnReceivingEffect(ReceivingEffectEventArgs ev)
        {
            // Check that the player receiving the effect is the same player
            if (player != null && player.Id == ev.Player.Id)
            {
                // Remove the 'negative' effect
                SCP999x.Extensions.Extensions.removeNegativeStatusEffect(player);
            }
        }



        public void OnHurt(HurtingEventArgs ev)
        {
            if ((ev.Attacker.Role == RoleType.Scp93953 || ev.Attacker.Role == RoleType.Scp93989 || ev.Attacker.Role == RoleType.Scp0492) && (player.Id == ev.Target.Id))
            {// Remove the 'negative' effect
                SCP999x.Extensions.Extensions.removeNegativeStatusEffect(player);
            }
        }






        public IEnumerator<float> passiveHeal()
        {
            while(true)
            {
                // Check that player still has ability
                if (player != null)
                {
                    // Heal player by {amount} hp and Prevent hp over max
                    if (player.Health + SCP999x.Config.HealPrimary >= player.MaxHealth)
                    {
                        player.Health = player.MaxHealth;
                    }
                    else
                    {
                        player.Health = player.Health + SCP999x.Config.HealPrimary;
                    }
                    // Make a copy of the list of players on same team
                    // Remove the ability player from the list
                    List<Exiled.API.Features.Player> temp = new List<Player>();
                    foreach (Exiled.API.Features.Player e in Exiled.API.Features.Player.List)
                    {
                        if (player.Id != e.Id && player.Role == e.Role)
                        {
                            temp.Add(e);
                        }
                    }
                    // For all the remaining players on the list
                    foreach (Exiled.API.Features.Player e in temp)
                    {
                        // Check if the any player is in the same room as the ability player && are also alive
                        if (player.CurrentRoom.Name.CompareTo(e.CurrentRoom.Name) == 0)
                        {
                            // Grant (1/4)*{amount} hp and Prevent hp over max
                            if (e.Health + SCP999x.Config.HealSecondary >= e.MaxHealth)
                            {
                                e.Health = e.MaxHealth;
                            }
                            else
                            {
                                e.Health = e.Health + SCP999x.Config.HealSecondary;
                            }
                        }
                    }
                    temp = null;
                }
                yield return Timing.WaitForSeconds(SCP999x.Config.HealResetTime);
            }
        }
    }
}
