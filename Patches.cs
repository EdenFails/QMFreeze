using System.Collections.Generic;
using System.Reflection;
using Il2CppSystem;
using MelonLoader;
using UnityEngine;
using VRC.SDKBase;
using VRCSDK2;

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
        public static bool isfrozen, QMFreeze, AlreadyImmobolized = false;
        private static void OnMenuOpened()
        {
            //MelonLogger.Msg("Opened"); // Debug
            if (VRCPlayer.field_Internal_Static_VRCPlayer_0 != null)
            {
                if(Physics.gravity != Vector3.zero)
                {
                    _origionalGravity = Physics.gravity;
                    _originalVelocity = Networking.LocalPlayer.GetVelocity();
                }
                Physics.gravity = Vector3.zero;
                Networking.LocalPlayer.SetVelocity(Vector3.zero);
                isfrozen = true;

            }
            //MelonLogger.Msg("InnerEndOpened Gravity = " + Physics.gravity.x + Physics.gravity.y + Physics.gravity.z + " OgGravity = " + _origionalGravity.x + _origionalGravity.y + _origionalGravity.z); //-- Used For Debugging 
        }

        private static void OnMenuClosed()
        {
            //MelonLogger.Msg("ClosedMenu"); // Debug
            if (VRCPlayer.field_Internal_Static_VRCPlayer_0 != null)
                if (isfrozen)
                {
                    //if (AlreadyImmobolized)
                    //{
                    //    VRCPlayer.field_Internal_Static_VRCPlayer_0.prop_VRCPlayerApi_0.Immobilize(false);
                    //}
                    Physics.gravity = _origionalGravity;
                    //Networking.LocalPlayer.SetVelocity(_originalVelocity);
                    isfrozen = false;
                    //MelonLogger.Msg("InnerEnd CLOSED Gravity = " + Physics.gravity.x + Physics.gravity.y + Physics.gravity.z + " OgGravity = " + _origionalGravity.x + _origionalGravity.y + _origionalGravity.z); //-- Used For Debugging 

                }
        }
    }
}
