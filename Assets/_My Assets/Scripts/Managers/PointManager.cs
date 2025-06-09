using System.Collections;
using UnityEngine;
using System;
using System.Collections.Generic;

namespace StadiumVR
{
    public class PointManager : MonoBehaviour
    {
        [SerializeField] private List<SnapPointController> AllSnapPoints;
        [SerializeField] private List<SnapPointController> AllocateSnapPoints;
        [SerializeField] private List<SnapPointController> FreeSnapPoints;
        private void OnEnable()
        {
            AllocateSnapPoints.Clear();
            FreeSnapPoints.AddRange(AllSnapPoints);
            ActionManager.GetAllFreeSnapPoints += GetAllFreeSnapPoints;
            ActionManager.OnSetAllocateSnapPoint += SetAllocateSnapPoint;
        }
        private void OnDisable()
        {
            ActionManager.GetAllFreeSnapPoints -= GetAllFreeSnapPoints;
            ActionManager.OnSetAllocateSnapPoint -= SetAllocateSnapPoint;
        }

        private List<SnapPointController> GetAllFreeSnapPoints()
        {
            return FreeSnapPoints;
        }

        private void SetAllocateSnapPoint(SnapPointController snapPoint)
        {
            FreeSnapPoints.Remove(snapPoint);
            AllocateSnapPoints.Add(snapPoint);   
        }

    }
}

