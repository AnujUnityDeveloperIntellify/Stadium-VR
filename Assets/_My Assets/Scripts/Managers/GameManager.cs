using UnityEngine;

namespace StadiumVR
{

    internal enum ObjectStatus
    {
        None,
        picked,
        placed

    }
    public class GameManager : MonoBehaviour
    {
        internal const float ThresholdDistance = 0.2f;
        internal const float BoxThresholdDistance = 0.2f;
        private void OnEnable()
        {

        }
    }
}

