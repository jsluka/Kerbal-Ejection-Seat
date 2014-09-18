using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// This is where I go for tutorials 
/// http://wiki.kerbalspaceprogram.com/wiki/Plugins
/// </summary>

namespace K
{
    public class KerbalEjectionSeat : PartModule
    {
        private Kerbal activeKerbal;
        private KerbalEVA currentKerbalEVA;

        /// <summary>
        /// Begins the EVA for the active kerbal. Based on Mihara's Internal Module code for EVA. 
        /// https://github.com/Mihara/RasterPropMonitor/blob/master/RasterPropMonitor/Auxiliary%20modules/JSIInternalEVAHatch.cs
        /// </summary>
        private void GoEva()
        {
            InternalSeat commandSeat = part.internalModel.seats[0];
            if(commandSeat.taken == true){
                activeKerbal = commandSeat.kerbalRef;
                commandSeat.DespawnCrew();
                FlightEVA.SpawnEVA(activeKerbal);
                CameraManager.Instance.SetCameraFlight();
                activeKerbal = null;
            }
        }

        /// <summary>
        /// Ejects the Kerbal from the capsule
        /// </summary>
        private void GoEject()
        {
            print("Go eject...");
            InternalSeat commandSeat = part.internalModel.seats[0];
            if (commandSeat.taken == true)
            {
                activeKerbal = commandSeat.kerbalRef;
                commandSeat.DespawnCrew();
                FlightEVA.SpawnEVA(activeKerbal);

                bool test = FlightGlobals.ActiveVessel;

                currentKerbalEVA = FlightGlobals.ActiveVessel.GetComponent<KerbalEVA>();
                print("OK...");

                if (currentKerbalEVA.OnALadder)
                {
                    print("On ladder...");
                    currentKerbalEVA.OnVesselGoOffRails(FlightGlobals.ActiveVessel);
                }

                CameraManager.Instance.SetCameraFlight();
                activeKerbal = null;
            }
        }

        /// <summary>
        /// Eject action
        /// </summary>
        [KSPAction("Eject From Cockpit")]
        public void Eject(KSPActionParam param)
        {
            GoEject();
        }

        [KSPEvent(guiActive = true, guiName = "Eject", active=true)]
        public void ActivateEject()
        {
            print("GoEva...");
            //TODO: Figure out why this doesn't work
            //Events["Eject"].active = false;
            GoEject();
        }
    }
}