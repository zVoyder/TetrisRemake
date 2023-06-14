namespace TetrisRemake.Pieces
{
    using TetrisRemake.Managers;
    using UnityEngine;
    using VUDK.Patterns.ObjectPool;
    using VUDK.Patterns.ObjectPool.Interfaces;

    [RequireComponent (typeof(PieceMovement))]
    public class FullPiece : MonoBehaviour, IPooledObject
    {
        private PieceMovement _movement;

        public Pool RelatedPool { get; private set; }

        public void AssociatePool(Pool associatedPool)
        {
            RelatedPool = associatedPool;
        }

        public void Dispose()
        {
            RelatedPool.Dispose(gameObject);
        }

        private void Awake()
        {
            TryGetComponent(out _movement);
        }

        private void Start()
        {
            _movement.Init(GameManager.Instance.GridManager.GridDimension.x);
        }

        public void Init()
        {
        }
    }
}
