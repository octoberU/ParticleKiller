using MelonLoader;
using System.Reflection;

namespace ParticleKiller
{
    internal static class Config
    {
        public const string Category = "ParticleKiller";

        public static bool Enabled;
        public static bool KillCPUParticles;
        public static int ParticleCount;

        public static void RegisterConfig()
        {
            MelonPrefs.RegisterBool(Category, nameof(Enabled), true, "Enables the mod.");
            
            MelonPrefs.RegisterBool(Category, nameof(KillCPUParticles), true, "Disables a small puff of particles.");

            MelonPrefs.RegisterInt(Category, nameof(ParticleCount), 0, "Amount of GPU particles per shot. [0,50000,1000,0] {G}");

            OnModSettingsApplied();
        }

        public static void OnModSettingsApplied()
        {
            foreach (var fieldInfo in typeof(Config).GetFields(BindingFlags.Static | BindingFlags.Public))
            {
                if (fieldInfo.FieldType == typeof(bool))
                    fieldInfo.SetValue(null, MelonPrefs.GetBool(Category, fieldInfo.Name));
                
                if (fieldInfo.FieldType == typeof(int))
                    fieldInfo.SetValue(null, MelonPrefs.GetInt(Category, fieldInfo.Name));
            }
        }
    }
}
