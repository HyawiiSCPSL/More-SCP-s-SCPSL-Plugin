using Exiled.API.Features;
using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.Events.Handlers;
using System;

using Server = Exiled.Events.Handlers.Server;
using Player = Exiled.Events.Handlers.Player;


namespace SCP999x
{
    public class Plugin : Plugin<Config>
    {
        public override string Author { get; } = "CK";
        public override string Name { get; } = "SCP999x";
        public override Version Version { get; } = new Version(1, 0, 0);
        public override Version RequiredExiledVersion { get; } = new Version(2, 1, 0);

        internal static Plugin Instance { get; } = new Plugin();
        public override PluginPriority Priority { get; } = PluginPriority.Medium;

        private Handlers.Player player;
        private Handlers.Server server;
        public static Plugin singleton;

        public Exiled.API.Features.Player abilityUser;
        public bool ability999x;
        public SCP999x.Components.SCP999xPassiveComponent scp999xPassComp;

        private Plugin()
        {

        }

        public override void OnEnabled()
        {
            try
            {
                base.OnEnabled();
                RegisterEvents();
            } catch (Exception e)
            {
                Log.Error($"There was an error loading the plugin: {e}");
            }
        }

        public override void OnDisabled()
        {
            base.OnDisabled();
            UnRegisterEvents();
        }

        public void RegisterEvents()
        {
            singleton = this;
            server = new Handlers.Server(this);
            player = new Handlers.Player(this);

            Server.RoundStarted += server.OnRoundStarted;

            Player.Dying += player.OnDying;
            Player.Left += player.OnLeft;
        }

        public void UnRegisterEvents()
        {
            Server.RoundStarted -= server.OnRoundStarted;

            Player.Dying -= player.OnDying;
            Player.Left -= player.OnLeft;

            server = null;
            player = null;
        }
    }
}
