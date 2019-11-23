using Harmony;

namespace Rumble_Controller.HarmonyPatches
{
    class HitNotePatch
    {
        [HarmonyPatch(typeof(NoteCutHapticEffect))]
        [HarmonyPatch("HitNote")]
        class noteCutPatch
        {
            static bool Prefix(Saber.SaberType saberType)
            {
                if (!PluginConfig.noteCut && PluginConfig.enabled)
                    return false;
                else
                    return true;
            }

        }
    }
}