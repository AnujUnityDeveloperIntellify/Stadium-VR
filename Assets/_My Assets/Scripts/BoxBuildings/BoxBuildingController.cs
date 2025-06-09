using NUnit.Framework;
using Oculus.Interaction.HandGrab;
using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace StadiumVR
{
    public class BoxBuildingController : MonoBehaviour
    {
        internal ObjectStatus status;
        private float duration = 0.4f;
        private Grabbable boxGrabbable;
        private HandGrabInteractable boxHandGrabInteractable;
        [SerializeField] Transform placePoint;
        //private SnapPointController currentSnapPoint;
        [SerializeField] private TextMeshProUGUI brickStatusTxt;
        [SerializeField] private Transform UpperPlacePoint;

        private void Awake()
        {
            boxGrabbable = GetComponent<Grabbable>();
            boxHandGrabInteractable = GetComponent<HandGrabInteractable>();
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            status = ObjectStatus.None;
            brickStatusTxt.text = status.ToString();
        }

        // Update is called once per frame
        void Update()
        {
            if(status == ObjectStatus.picked && placePoint.gameObject.activeSelf)
            {
                CheckSnapping();
            }
        }

        private void CheckSnapping()
        {
            Vector3 pos = transform.position;
            if (Vector3.Distance(pos, placePoint.position) <= GameManager.ThresholdDistance)
            {
                OnBoxPlaced();
            }
            // OnBoxPlaced(GetNearestSnapPoints());
        }
        private void OnBoxPlaced()
        {
            status = ObjectStatus.placed;
            brickStatusTxt.text = status.ToString();
            boxGrabbable.enabled = false;
            boxHandGrabInteractable.enabled = false;
            StartMovement();

        }
        /*private void OnBoxPlaced(SnapPointController snapPoint)
        {
            if(snapPoint!=null)
            {
                status = ObjectStatus.placed;
                brickStatusTxt.text = status.ToString();
                ActionManager.OnSetAllocateSnapPoint(snapPoint);
                StartMovement(snapPoint);
            }
            
        }*/
        private SnapPointController GetNearestSnapPoints()
        {
            SnapPointController nearestSnapPoints = null;

            /*List<SnapPointController> AllFreeSnapPoints = ActionManager.GetAllFreeSnapPoints();
            Vector3 snapPointPos = Vector3.zero;
            foreach (var SnapPoint in AllFreeSnapPoints) 
            {
                snapPointPos = SnapPoint.transform.position;
                if(Vector3.Distance(transform.position,snapPointPos) <= GameManager.BoxThresholdDistance)
                {
                    nearestSnapPoints = SnapPoint;  
                }
            }*/
            
            return nearestSnapPoints;
        }
       
        public void OnLeave()
        {
            if (status != ObjectStatus.placed)
            {
                status = ObjectStatus.None;
                brickStatusTxt.text = status.ToString();
            }
        }
        public void OnPicked()
        {
            status = ObjectStatus.picked;
            brickStatusTxt.text = status.ToString();
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
            if(UpperPlacePoint!=null)
            {
                UpperPlacePoint.gameObject.SetActive(true);
            }
        }


        /*
                private void StartMovement(SnapPointController snapPoint)
                {
                    StartCoroutine(MoveObject(snapPoint.transform));
                }

                private IEnumerator MoveObject(Transform placePoint)
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
                }*/


    }
}

