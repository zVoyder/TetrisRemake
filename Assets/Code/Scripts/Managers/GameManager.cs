namespace TetrisRemake.Managers
{
    using UnityEngine;
    using VUDK.Patterns.Singleton;

    public class GameManager : Singleton<GameManager>
    {
        [field: SerializeField]
        public GridManager GridManager { get; private set; }

        private void Start()
        {
            GridManager.GenerateGrid();
        }
    }
}
