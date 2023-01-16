using CommandSystem.Commands.Prefs;
using Exiled.API.Features;
using MEC;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SCP999x.Handlers
{
	public class Server
	{
		public Plugin plugin;
		public Server(Plugin plugin) => this.plugin = plugin;

		public void OnWaitingForPlayers()
		{
			Log.Info("Waiting for players...");
		}

		// Assign 1 random 
		public void OnRoundStarted()
		{
			Timing.CallDelayed(5f, () =>
			{
				Exiled.API.Features.Player player = null;
				List<Exiled.API.Features.Player> list = new List<Exiled.API.Features.Player>(Exiled.API.Features.Player.List);
				bool ability = false;
				Random rand = new Random();

				do
				{
					int choosen_id = rand.Next(list.Count);
					player = Exiled.API.Features.Player.List.ElementAt(choosen_id);
					list.RemoveAt(choosen_id);
				} while (player.Role != RoleType.ClassD && player.Role != RoleType.Scientist && player.Role != RoleType.FacilityGuard);//check for not d-class / scientist / guard

				Log.Debug($"Player: {player.Id} assigned 999x plugin ability");

				//set the shared player data
				plugin.abilityUser = player;
				ability = true;
				plugin.ability999x = ability;
				player.ShowHint($"<color=orange>You Have Been Chosen! {player.Nickname}</color>", 15f);

				// The SCP 999x passive status coroutine 'healing' effect will be called here
				plugin.scp999xPassComp = plugin.abilityUser.GameObject.GetComponent<SCP999x.Components.SCP999xPassiveComponent>();
				if (plugin.scp999xPassComp == null) { plugin.abilityUser.GameObject.AddComponent<SCP999x.Components.SCP999xPassiveComponent>(); }
			});
		}
	}
}
