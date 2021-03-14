namespace UnityEngine
{
    public static class VectorExtensions
    {
        public static Vector2 ToVector2(this Vector3 vector)
        {
            return new Vector2(vector.x, vector.y);
        }

        public static Vector3 ToVector3(this Vector2 vector, float z = 0)
        {
            return new Vector3(vector.x, vector.y, z);
        }
    }
}
