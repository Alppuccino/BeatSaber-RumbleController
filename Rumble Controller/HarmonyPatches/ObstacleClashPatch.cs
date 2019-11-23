using System;
using Harmony;
using IPA.Utilities;
using UnityEngine;
using UnityEngine.XR;

namespace Rumble_Controller.HarmonyPatches
{
    class ObstacleClashPatch
    {
        [HarmonyPatch(typeof(ObstacleSaberSparkleEffectManager))]
        [HarmonyPatch("Update")]
        class obstacleRumblePatch
        {
            static bool Prefix(ActiveObstaclesManager ____activeObstaclesManager, ObstacleSaberSparkleEffect ____obstacleSaberSparkleEffectPefab, PlayerController ____playerController, HapticFeedbackController ____hapticFeedbackController, ref Saber[] ____sabers, ref ObstacleSaberSparkleEffect[] ____effects, ref Transform[] ____effectsTransforms, ref bool[] ____isSystemActive, ref bool[] ____wasSystemActive, ref Vector3[] ____burnMarkPositions, ObstacleSaberSparkleEffectManager __instance)
            {
                if (!PluginConfig.obstacleRumble && PluginConfig.enabled)
                {
                    ____wasSystemActive[0] = ____isSystemActive[0];
                    ____wasSystemActive[1] = ____isSystemActive[1];
                    ____isSystemActive[0] = false;
                    ____isSystemActive[1] = false;
                    foreach (ObstacleController obstacleController in ____activeObstaclesManager.activeObstacleControllers)
                    {
                        Bounds bounds = obstacleController.bounds;
                        for (int i = 0; i < 2; i++)
                        {
                            Vector3 vector;
                            if (____sabers[i].isActiveAndEnabled && __instance.GetBurnMarkPos(bounds, obstacleController.transform, ____sabers[i].saberBladeBottomPos, ____sabers[i].saberBladeTopPos, out vector))
                            {
                                ____isSystemActive[i] = true;
                                ____burnMarkPositions[i] = vector;
                                ____effects[i].SetPositionAndRotation(vector, __instance.GetEffectRotation(vector, obstacleController.transform, bounds));
                                XRNode node = (i == 0) ? XRNode.LeftHand : XRNode.RightHand;
                                //____hapticFeedbackController.ContinuousRumble(node);
                                if (!____wasSystemActive[i])
                                {
                                    ____effects[i].StartEmission();
                                    __instance.GetPrivateField<Action<Saber.SaberType>>("sparkleEffectDidStartEvent")(____sabers[i].saberType);
                                }
                            }
                        }
                    }
                    for (int j = 0; j < 2; j++)
                    {
                        if (!____isSystemActive[j] && ____wasSystemActive[j])
                        {
                            ____effects[j].StopEmission();
                            __instance.GetPrivateField<Action<Saber.SaberType>>("sparkleEffectDidEndEvent")(____sabers[j].saberType);
                        }
                    }
                    return false;
                }
                else
                    return true;
            }

        }
    }
}