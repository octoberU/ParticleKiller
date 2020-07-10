using MelonLoader;
using Harmony;

namespace AudicaModding
{
    public class AudicaMod : MelonMod
    {
        static public int particleCount;
        static public bool killCPUParticles;
        public static class BuildInfo
        {
            public const string Name = "ParticleKiller";  // Name of the Mod.  (MUST BE SET)
            public const string Author = "octo"; // Author of the Mod.  (Set as null if none)
            public const string Company = null; // Company that made the Mod.  (Set as null if none)
            public const string Version = "0.1.0"; // Version of the Mod.  (MUST BE SET)
            public const string DownloadLink = null; // Download Link for the Mod.  (Set as null if none)
        }

        public override void OnApplicationStart()
        {
            var i = HarmonyInstance.Create("ParticleKiller");
            Hooks.ApplyHooks(i);
        }

        public override void OnLevelWasLoaded(int level)
        {
            if (!ModPrefs.HasKey("ParticleKiller", "GPUParticlesCount"))
            {
                CreateConfig();
            }
            else
            {
                LoadConfig();
            }
        }

        private void CreateConfig()
        {
            ModPrefs.RegisterPrefInt("ParticleKiller", "GPUParticlesCount", 0);
            ModPrefs.RegisterPrefBool("ParticleKiller", "KillCPUParticles", true);
        }
        private void LoadConfig()
        {
            particleCount = ModPrefs.GetInt("ParticleKiller", "GPUParticlesCount");
            killCPUParticles = ModPrefs.GetBool("ParticleKiller", "KillCPUParticles");
            MelonModLogger.Log("Loaded config!: " + particleCount.ToString() + ", " + killCPUParticles.ToString());
        }
    }
}



