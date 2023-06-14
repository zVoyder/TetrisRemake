namespace TetrisRemake.Grid
{
    using UnityEngine;
    using TetrisRemake.Pieces;

    public class TileSlot : MonoBehaviour
    {
        private SinglePiece _associatedPiece;

        private void OnTriggerStay2D(Collider2D other) // TO DO: Change this with Enter
        {
            if(other.TryGetComponent(out SinglePiece piece))
            {
                transform.name = piece.name;
                _associatedPiece = piece;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            transform.name = "Tile";
            _associatedPiece = null;
        }
    }
}
