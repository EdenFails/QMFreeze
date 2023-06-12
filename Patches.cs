using System.Collections.Generic;
using System.Reflection;
using MelonLoader;
using UnityEngine;
using VRC.SDKBase;

namespace Patches
{

    internal static class PatchBS
    {


        public static List<GameObject> ClonedAvatar = new List<GameObject>();
        private static readonly HarmonyLib.Harmony instance = new HarmonyLib.Harmony("QMFreeze");

        public static unsafe void Patch()
        {
            instance.Patch(typeof(VRC.UI.Elements.QuickMenu).GetMethod(nameof(VRC.UI.Elements.QuickMenu.OnDisable)), new HarmonyLib.HarmonyMethod(typeof(PatchBS).GetMethod(nameof(OnMenuClosed), BindingFlags.Static | BindingFlags.NonPublic)), null, null, null, null);

            instance.Patch(typeof(VRC.UI.Elements.QuickMenu).GetMethod(nameof(VRC.UI.Elements.QuickMenu.OnEnable)), new HarmonyLib.HarmonyMethod(typeof(PatchBS).GetMethod(nameof(OnMenuOpened), BindingFlags.Static | BindingFlags.NonPublic)), null, null, null, null);

            MelonLogger.Msg("QMFreeze Patched");
        }

        public static Vector3 _origionalGravity, _originalVelocity;
        public static bool isfrozen, QMFreeze = false;
        private static void OnMenuOpened()
        {

            if (QMFreeze)
            {
                VRCPlayer.field_Internal_Static_VRCPlayer_0.prop_VRCPlayerApi_0.Immobilize(true);
                _origionalGravity = Physics.gravity;
                _originalVelocity = Networking.LocalPlayer.GetVelocity();
                Physics.gravity = Vector3.zero;
                Networking.LocalPlayer.SetVelocity(Vector3.zero);
                isfrozen = true;
            }
        }
        private static void OnMenuClosed()
        {

            if (isfrozen)
            {
                Physics.gravity = _origionalGravity;
                VRCPlayer.field_Internal_Static_VRCPlayer_0.prop_VRCPlayerApi_0.Immobilize(false);
                //Networking.LocalPlayer.SetVelocity(_originalVelocity);
                isfrozen = false;
            }
        }

    }
    
}

