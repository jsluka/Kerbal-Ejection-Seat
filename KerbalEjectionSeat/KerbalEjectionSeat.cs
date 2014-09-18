using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// This is where I go for tutorials 
/// http://wiki.kerbalspaceprogram.com/wiki/Plugins
/// 
/// Additionally, consult RPM's InternalEVAHatch code to figure out the beginnings of ejecting
/// https://github.com/Mihara/RasterPropMonitor/blob/master/RasterPropMonitor/Auxiliary%20modules/JSIInternalEVAHatch.cs
/// </summary>

namespace K
{
    public class KerbalEjectionSeat : PartModule
    {
        private Kerbal activeKerbal;

        /// <summary>
        /// Begins the EVA for the active kerbal
        /// </summary>
        private void GoEva()
        {
            print("GoEva starting...");
            InternalSeat commandSeat = part.internalModel.seats[0];
            print("commandSeat set...");
            if(commandSeat.taken == true){
                print("commandSeat taken...");
                activeKerbal = commandSeat.kerbalRef;
                print("activeKerbal set...");
                commandSeat.DespawnCrew();
                print("Crew despawned...");
                FlightEVA.SpawnEVA(activeKerbal);
                print("Spawn EVA...");
                CameraManager.Instance.SetCameraFlight();
                print("Setting camera...");
                activeKerbal = null;
                print("Setting activeKerbal to null...");
            }
        }

        /// <summary>
        /// Eject action
        /// </summary>
        [KSPAction("Eject")]
        public void Eject(KSPActionParam param)
        {
            print("Activating eject...");
            ActivateEject();
        }

        [KSPEvent(guiActive = true, guiName = "Eject")]
        public void ActivateEject()
        {
            print("GoEva...");
            //Events["Eject"].active = true;
            GoEva();
        }
    }
}