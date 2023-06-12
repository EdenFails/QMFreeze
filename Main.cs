
using MelonLoader;
using System;
using Patches;
using System.Reflection;


[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyTitle("QMFreezeVrchat")]
[assembly: AssemblyProduct("QMFreeze")]
[assembly: MelonInfo(typeof(Helix.Main), "QMFreeze-ReMade", "OwO", "EdenFails", null)]
[assembly: MelonGame("VRChat", null)]
[assembly: MelonColor(ConsoleColor.DarkRed)]
//[assembly: TargetFramework(".NETFramework,Version=v4.7.2", FrameworkDisplayName = ".NET Framework 4.7.2")]




namespace Helix
{

    public class Main : MelonMod
    {
        public Main() 
        {


        }

        public override void OnApplicationStart()
        {
            PatchBS.Patch();
            PatchBS.QMFreeze = true;
        }

      

    } 
}
