using UnityEngine;

namespace StadiumVR
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioSource brickesPlacedAudioSource;


        private void OnEnable()
        {
            ActionManager.OnBricksPlaySound += OnPlayerBricksPlaced;
        }
        private void OnDisable()
        {
            ActionManager.OnBricksPlaySound -= OnPlayerBricksPlaced;
        }

        private void OnPlayerBricksPlaced()
        {
            brickesPlacedAudioSource.Play();    
        }
    }

}
