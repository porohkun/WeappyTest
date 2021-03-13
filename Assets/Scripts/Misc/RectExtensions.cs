namespace UnityEngine
{
    public static class RectExtensions
    {
        public static bool Intersects(this Rect rectA, Rect rectB)
        {
            return rectB.xMin < rectA.xMax &&
                   rectA.xMin < rectB.xMax &&
                   rectB.yMin < rectA.yMax &&
                   rectA.yMin < rectB.yMax;
        }
    }
}
