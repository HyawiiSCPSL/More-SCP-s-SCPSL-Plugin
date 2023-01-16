using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomPlayerEffects;
using Exiled.Events.EventArgs;
using Exiled.API.Features;


namespace SCP999x.Extensions
{
    public static class Extensions
    {
        public static void removeNegativeStatusEffect(Player player)// Corroding and decontaminating are void
        {
            player.ReferenceHub.playerEffectsController.DisableEffect<Amnesia>();
            player.ReferenceHub.playerEffectsController.DisableEffect<Asphyxiated>();
            player.ReferenceHub.playerEffectsController.DisableEffect<Bleeding>();
            player.ReferenceHub.playerEffectsController.DisableEffect<Blinded>();
            player.ReferenceHub.playerEffectsController.DisableEffect<Burned>();
            player.ReferenceHub.playerEffectsController.DisableEffect<Concussed>();
            player.ReferenceHub.playerEffectsController.DisableEffect<Deafened>();
            player.ReferenceHub.playerEffectsController.DisableEffect<Disabled>();
            player.ReferenceHub.playerEffectsController.DisableEffect<Ensnared>();
            player.ReferenceHub.playerEffectsController.DisableEffect<Exhausted>();
            player.ReferenceHub.playerEffectsController.DisableEffect<Flashed>();
            player.ReferenceHub.playerEffectsController.DisableEffect<Hemorrhage>();
            player.ReferenceHub.playerEffectsController.DisableEffect<Panic>();
            player.ReferenceHub.playerEffectsController.DisableEffect<Poisoned>();
            player.ReferenceHub.playerEffectsController.DisableEffect<SinkHole>();
            player.ReferenceHub.playerEffectsController.DisableEffect<Scp207>();//additions to the cola will be made
        }

        public static IList<T> Clone<T>(this IList<T> listToClone) where T:ICloneable
        {
            return listToClone.Select(item => (T)item.Clone()).ToList();
        }
    }
}
