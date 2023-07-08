
using MelonLoader;
using System;
using Patches;
using System.Reflection;
using VRC.SDKBase;
using UnityEngine;


[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyTitle("QMFreezeVrchat")]
[assembly: AssemblyProduct("QMFreeze")]
[assembly: MelonInfo(typeof(Helix.Main), "QMFreeze-ReMade", "OwO", "EdenFails", null)]
[assembly: MelonGame("VRChat", null)]
[assembly: MelonColor(ConsoleColor.Blue)]
//[assembly: TargetFramework(".NETFramework,Version=v4.7.2", FrameworkDisplayName = ".NET Framework 4.7.2")]

namespace Helix
{

    public class Main : MelonMod
    {
        public static bool checknull = false;
        public Main() 
        {
            

        }

        public override void OnApplicationStart()
        {
            PatchBS.Patch();
            PatchBS.QMFreeze = true;
            MelonLogger.Msg("QMFreeze Loaded - Eden");
        }

        public override void OnSceneWasLoaded(int index, string name)
        {
            if (index != -1) return;
            MelonCoroutines.Start(checknulls());
        }
        public override void OnSceneWasUnloaded(int buildIndex, string sceneName)
        {
            PatchBS._origionalGravity = new Vector3(0, -9.81f, 0); // earth standard - 
        }
        public static System.Collections.IEnumerator checknulls()
        {
            //MelonLogger.Msg("CheckingNulls");
            while (RoomManager.field_Internal_Static_ApiWorldInstance_0 == null || VRCPlayer.field_Internal_Static_VRCPlayer_0 == null)
            {
                //MelonLogger.Msg("Checking World Validity!");//debugging
                yield return new WaitForSeconds(0.8f);
            }
            while  (VRCPlayer.field_Internal_Static_VRCPlayer_0.transform == null || Vector3.Distance(VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position, Vector3.zero) < 0.1f)
            {
                //MelonLogger.Msg("Checking Player Validity!");//Debugging
                yield return new WaitForSeconds(0.8f);//will keep checking to make sure
            }
            //MelonLogger.Msg("Player Exists!");//Debugging
            PatchBS._origionalGravity = Physics.gravity;
            PatchBS._originalVelocity = Networking.LocalPlayer.GetVelocity();
            yield break;
        }
    } 
}
