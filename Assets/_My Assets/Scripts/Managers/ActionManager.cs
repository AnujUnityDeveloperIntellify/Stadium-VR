using UnityEngine;
using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace StadiumVR
{
    public class ActionManager : MonoBehaviour
    {
        internal static Action OnBricksPlaySound;

        internal static Func<List<SnapPointController>> GetAllFreeSnapPoints;
        internal static Action<SnapPointController> OnSetAllocateSnapPoint; 
    }
}

