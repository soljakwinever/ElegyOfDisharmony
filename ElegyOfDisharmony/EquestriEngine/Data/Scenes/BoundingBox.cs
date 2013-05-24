using BoundBox = Microsoft.Xna.Framework.BoundingBox;

namespace EquestriEngine.Data.Scenes
{
    public struct BoundingBox
    {
        BoundBox _boundingBox;

        private BoundingBox(BoundBox b)
        {
            _boundingBox = b;

        }

        #region Methods


        #endregion

        #region Operators

        public static implicit operator BoundBox(BoundingBox b)
        {
            return b._boundingBox;
        }

        public static implicit operator BoundingBox(BoundBox b)
        {
            return new BoundingBox(b);
        }

        #endregion
    }
}
