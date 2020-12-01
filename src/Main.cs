using MelonLoader;
using Harmony;
using ParticleKiller;
using System;
using UnityEngine;

[assembly: MelonGame("Harmonix Music Systems, Inc.", "Audica")]
[assembly: MelonInfo(typeof(ParticleKillerMod), "ParticleKiller", "0.1.1", "octo", "https://github.com/octoberU/ParticleKiller")]

public class ParticleKillerMod : MelonMod
{
    public override void OnApplicationStart()
    {
        Config.RegisterConfig();
    }

    public override void OnModSettingsApplied()
    {
        Config.OnModSettingsApplied();
    }

    [HarmonyPatch(typeof(UGPUEmitter), "Emit", new Type[] { typeof(int), typeof(bool) })]
    private static class ParticleEmmision
    {
        private static void Prefix(UGPUEmitter __instance, ref int count, bool immediate)
        {
            if (!Config.Enabled) return;
            count = Config.ParticleCount;
        }
    }

    [HarmonyPatch(typeof(UGPUEmitter), "EmitBurst", new Type[] { typeof(int) })]
    private static class ParticleEmmisionBurst
    {
        private static void Prefix(UGPUEmitter __instance, ref int count)
        {
            if (!Config.Enabled) return;
            count = Config.ParticleCount;
        }
    }

    [HarmonyPatch(typeof(ParticlePool), "Play", new Type[] { typeof(Vector3), typeof(Quaternion), typeof(float) })]
    private static class CPUParticleEmmisionNoParams
    {
        private static bool Prefix(ParticlePool __instance)
        {
            if (!Config.Enabled) return true;
            if (Config.KillCPUParticles) return false;
            else return true;
        }
    }
}

