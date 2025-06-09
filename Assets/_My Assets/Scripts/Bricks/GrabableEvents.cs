using UnityEngine;

namespace StadiumVR
{
    public class GrabableEvents : OVRGrabbable
    {

        [SerializeField] private BricksController brickController;

        private void Awake()
        {
            brickController = GetComponent<BricksController>();
        }

        public override void GrabBegin(OVRGrabber hand, Collider grabPoint)
        {
            base.GrabBegin(hand, grabPoint);
            brickController.OnBricksPicked();
        }

        public override void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)
        {
            base.GrabEnd(linearVelocity, angularVelocity);
            brickController.OnBricksLeave();
        }

    }
}

