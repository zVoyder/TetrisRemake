namespace VUDK.Features.TurnBasedGDR.CombatSystem.Unit.UnitVisualEffects
{
    using UnityEngine;
    using VUDK.Features.TurnBasedGDR.CombatSystem.Units;

    [RequireComponent(typeof(UnitManager))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class UnitVisualEffects : MonoBehaviour
    {
        [SerializeField]
        private Material _deathMaterial;

        private Material _originalMaterial;
        private SpriteRenderer _renderer;
        private UnitManager _unitManager;

        private void Awake()
        {
            TryGetComponent(out _renderer);
            TryGetComponent(out _unitManager);
            _originalMaterial = new Material(_renderer.material);
        }

        private void OnEnable()
        {
            ChangeMaterial(_originalMaterial);
            _unitManager.OnBeforeDeath += () => ChangeMaterial(_deathMaterial);
        }

        private void OnDisable()
        {
            _unitManager.OnBeforeDeath -= () => ChangeMaterial(_deathMaterial);
        }

        private void ChangeMaterial(Material mat)
        {
            _renderer.material = mat;
        }
    }
}