using UnityEngine;

public static class ExtensionMethods 
{
    public static Vector2Int Vector2IntFromVector3(this Vector3 point)
    {
        int col = Mathf.FloorToInt(4f + point.x);
        int row = Mathf.FloorToInt(4f + point.z);
        return new Vector2Int(col, row);
    }

    public static Vector3 Vector3FromVector2Int(this Vector2Int gridPoint)
    {
        float x = -3.5f + 1.0f * gridPoint.x;
        float z = -3.5f + 1.0f * gridPoint.y;
        return new Vector3(x, 0, z);
    }
}
