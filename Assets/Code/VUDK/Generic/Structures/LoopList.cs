namespace VUDK.Generic.Structures
{
    using System.Collections.Generic;

    [System.Serializable]
    public class LoopList<T>
    {
        public List<T> objects;
        private int _index;

        public T CurrentObject
        {
            get
            {
                if (_index >= objects.Count)
                    _index = 0;

                if (_index < 0)
                    _index = objects.Count - 1;

                return objects[_index];
            }
        }

        public LoopList(List<T> objects)
        {
            this.objects = objects;
            _index = 0;
        }

        /// <summary>
        /// Resets the index.
        /// </summary>
        public void Reset()
        {
            _index = 0;
        }

        /// <summary>
        /// Gets the Current object and move the index to the next object.
        /// </summary>
        /// <returns>Current Object.</returns>
        public T Next()
        {
            T obj = CurrentObject;
            _index++;
            return obj;
        }

        /// <summary>
        /// Gets the Current object and move the index to the previous object.
        /// </summary>
        /// <returns>Current Object.</returns>
        public T Previous()
        {
            T obj = CurrentObject;
            _index--;
            return obj;
        }

    }
}
