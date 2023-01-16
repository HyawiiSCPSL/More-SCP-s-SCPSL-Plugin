using Exiled.API.Interfaces;
using System.ComponentModel;

namespace SCP999x
{
    public sealed class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public const float HealResetTime = 10.0f;
        public const float HealPrimary = 4.0f;
        public const float HpReduceScale = 4.0f;
        public const float HealSecondary = HealPrimary / HpReduceScale;
    }
}
