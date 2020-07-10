using Harmony;
using UnityEngine;
using System.Reflection;
using System;

namespace AudicaModding
{
    internal static class Hooks
    {
        public static void ApplyHooks(HarmonyInstance instance)
        {
            instance.PatchAll(Assembly.GetExecutingAssembly());
        }

        [HarmonyPatch(typeof(UGPUEmitter), "Emit", new Type[] { typeof(int), typeof(bool) })]
        private static class ParticleEmmision
        {
            private static void Prefix(UGPUEmitter __instance, ref int count, bool immediate)
            {
                //MelonModLogger.Log(AudicaMod.particleCount.ToString());
                count = AudicaMod.particleCount;
                //__instance.emit = false;
            }
        }

        [HarmonyPatch(typeof(UGPUEmitter), "EmitBurst", new Type[] { typeof(int) })]
        private static class ParticleEmmisionBurst
        {
            private static void Prefix(UGPUEmitter __instance, ref int count)
            {
                count = AudicaMod.particleCount;
                //__instance.emit = false;
            }
        }

        [HarmonyPatch(typeof(ParticlePool), "Play", new Type[] { typeof(Vector3), typeof(Quaternion), typeof(float)})]
        private static class CPUParticleEmmisionNoParams
        {
            private static bool Prefix(ParticlePool __instance)
            {
                //MelonModLogger.Log(AudicaMod.killCPUParticles.ToString());
                if (AudicaMod.killCPUParticles)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

    }
}
