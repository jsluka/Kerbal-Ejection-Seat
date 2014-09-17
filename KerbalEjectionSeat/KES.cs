using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// This is where I go for tutorials 
/// http://wiki.kerbalspaceprogram.com/wiki/Plugins
/// </summary>

public class KES : PartModule
{
    public override void OnStart(PartModule.StartState state)
    {
        print("Hello, Kerbin!");
    }
}