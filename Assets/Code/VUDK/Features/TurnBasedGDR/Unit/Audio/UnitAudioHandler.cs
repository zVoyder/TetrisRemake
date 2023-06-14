namespace VUDK.Features.TurnBasedGDR.CombatSystem.Units
{
    using VUDK.Features.TurnBasedGDR.SOData;
    using UnityEngine;

    [RequireComponent(typeof(UnitManager))]
    [RequireComponent(typeof(AudioSource))]
    public class UnitAudioHandler : MonoBehaviour
    {
        private AudioClip _getHitClip;
        private AudioClip _attackClip;
        private AudioClip _healClip;

        private UnitManager _unitManager;
        private AudioSource _audioSource;

        private void Awake()
        {
            TryGetComponent(out _unitManager);
            TryGetComponent(out _audioSource);
        }

        /// <summary>
        /// Initializes the <see cref="UnitAudioHandler"/>.
        /// </summary>
        /// <param name="getHitClip">Get hit <see cref="AudioClip"/> of the unit.</param>
        public void Init(AudioClip getHitClip)
        {
            _getHitClip = getHitClip;
        }

        /// <summary>
        /// Plays an <see cref="AudioClip"/> in the unit's <see cref="AudioSource"/>.
        /// </summary>
        /// <param name="clip"><see cref="AudioClip"/> to play.</param>
        public void PlayClip(AudioClip clip)
        {
            _audioSource.clip = clip;
            _audioSource.Play();
        }

        /// <summary>
        /// Plays the Hurt <see cref="AudioClip"/>.
        /// </summary>
        public void PlayHurtClip()
        {
            PlayClip(_getHitClip);
        }
    }
}