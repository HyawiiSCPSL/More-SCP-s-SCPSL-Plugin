using Exiled.API.Features;
using Exiled.Events.EventArgs;
using Exiled.Events.Extensions;
using static Exiled.Events.Events;
using CustomPlayerEffects;
using User = Exiled.API.Features.Player;
using Exiled.Events.EventArgs;
using System.Collections.Generic;

namespace SCP999x.Handlers
{
    public class Player
    {
        public Plugin plugin;
        public Player(Plugin plugin) => this.plugin = plugin;

        public void OnDying(DyingEventArgs ev)
        {
            // Check that the player receiving the effect is the same player and that the player still has the effect
            if (plugin.abilityUser.Id == ev.Target.Id && plugin.abilityUser != null && plugin.ability999x)
            {
                plugin.ability999x = false;
                plugin.abilityUser.ShowHint($"<color=red>ABILITY 999x LOST! {plugin.abilityUser.Nickname}</color>", 5f);
                clearSCP999xPassive();
            }
        }

        public void OnLeft(LeftEventArgs ev)
        {
            // Check that the player receiving the effect is the same player and that the player still has the effect
            if (plugin.abilityUser.Id == ev.Player.Id && plugin.abilityUser != null && plugin.ability999x)
            {
                plugin.ability999x = false;
                clearSCP999xPassive();
            }
        }

        private void clearSCP999xPassive()//Exiled.API.Features.Player player
        {
            //plugin.scp999xPassComp = player.GameObject.GetComponent<SCP999x.Components.SCP999xPassiveComponent>();
            //if (plugin.scp999xPassComp != null)
            //{
            UnityEngine.Object.Destroy(plugin.scp999xPassComp);
            //}
        }
    }
}
