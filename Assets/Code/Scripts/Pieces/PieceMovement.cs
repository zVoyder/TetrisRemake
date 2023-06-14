namespace TetrisRemake.Pieces
{
    using UnityEngine;
    using VUDK.Generic.Structures;

    public class PieceMovement : MonoBehaviour
    {
        [SerializeField]
        private float _moveGridSnap = 1;

        [SerializeField]
        private float _dropInterval = 1;

        private int _gridXSize;

        [SerializeField, Header("Wall Checks")]
        private LoopList<Vector2> _offsetsBasedOnRotation;
        [SerializeField]
        private float _pivotOffset = 0.5f;

        [SerializeField]
        private bool _canRotate = true;

        public void Init(int gridXSize)
        {
            _gridXSize = gridXSize;
            InvokeRepeating("Drop", _dropInterval, _dropInterval);
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.LeftArrow) && CheckWallLeft())
            {
                MoveLeft();
            }

            if (Input.GetKeyDown(KeyCode.RightArrow) && CheckWallRight())
            {
                MoveRight();
            }

            if (Input.GetKeyDown(KeyCode.R) && /*!IsCloseToWalls()*/ _canRotate)
            {
                Rotate();
            }
        }

        private void MoveLeft()
        {
            Move(-_moveGridSnap);
        }

        private void MoveRight()
        {
            Move(_moveGridSnap);
        }

        private void Move(float direction)
        {
            transform.position = new Vector3(transform.position.x + direction, transform.position.y, transform.position.z);
        }

        private void Rotate()
        {
            transform.Rotate(new Vector3(0, 0, 90));
            _offsetsBasedOnRotation.Next();
        }

        private void Drop()
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - _moveGridSnap, transform.position.z);
        }

        private void StopDrop()
        {
            CancelInvoke("Drop");
        }

        private bool CheckWallLeft()
        {
            return transform.position.x + -_pivotOffset + _offsetsBasedOnRotation.CurrentObject.x > -_gridXSize / 2;
        }

        private bool CheckWallRight()
        {
            return transform.position.x + _pivotOffset + _offsetsBasedOnRotation.CurrentObject.y < _gridXSize / 2;
        }

        private bool IsCloseToWalls() // TO FIX
        {
            return Mathf.Abs(transform.position.x + _pivotOffset + _offsetsBasedOnRotation.CurrentObject.x) <= _gridXSize / 2;
        }
    }
}