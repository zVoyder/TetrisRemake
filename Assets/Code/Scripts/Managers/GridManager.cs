namespace TetrisRemake.Managers
{
    using TetrisRemake.Grid;
    using UnityEngine;

    public class GridManager : MonoBehaviour
    {
        [SerializeField]
        public TetrisGrid _grid;
        private TileSlot[,] _currentGridTiles;

        [field: SerializeField]
        public Vector2Int GridDimension { get; private set; }

        public void GenerateGrid()
        {
            _grid.Init(GridDimension);
            _currentGridTiles = _grid.GenerateGrid();
        }
    }
}
