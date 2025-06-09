using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace StadiumVR
{
    public class BricksController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI brickStatusTxt;
        private Grabbable bricksGrabbable;
        private HandGrabInteractable bricksHandGrabInteractable;
        [SerializeField] Transform placePoint;
        internal ObjectStatus status;
        private float duration = 0.4f;
        private void Awake()
        {
            bricksGrabbable = GetComponent<Grabbable>();
            bricksHandGrabInteractable = GetComponent<HandGrabInteractable>();
        }
        private void OnEnable()
        {
            status = ObjectStatus.None;
            brickStatusTxt.text = status.ToString();
        }
        private void OnBricksPlaced()
        {
            status = ObjectStatus.placed;
            brickStatusTxt.text = status.ToString();
            bricksGrabbable.enabled = false;
            bricksHandGrabInteractable.enabled = false;
            StartMovement();
        }
        public void OnBricksPicked()
        {
            status = ObjectStatus.picked;
            brickStatusTxt.text = status.ToString();
        }
        public void OnBricksLeave()
        {
            if (status != ObjectStatus.placed)
            {
                status = ObjectStatus.None;
                brickStatusTxt.text = status.ToString();
            }
        }

        private void Update()
        {
            if (status == ObjectStatus.picked)
            {
                CheckSnapping();
            }
        }
        private void CheckSnapping()
        {
            Vector3 pos = transform.position;
            if (Vector3.Distance(pos, placePoint.position) <= GameManager.ThresholdDistance)
            {
                OnBricksPlaced();
            }
        }

        private void StartMovement()
        {
            StartCoroutine(MoveObject());
        }

        private IEnumerator MoveObject()
        {
            Vector3 startPos = transform.position;
            Quaternion startRot = transform.rotation;
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float t = elapsedTime / duration;
                transform.position = Vector3.Lerp(startPos, placePoint.position, t);
                transform.rotation = Quaternion.Lerp(startRot, Quaternion.identity, t);
                yield return null;
            }
            transform.position = placePoint.position;
            transform.rotation = Quaternion.identity;
            ActionManager.OnBricksPlaySound?.Invoke();
        }
    }
}

