using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    private static Vector3 moveVector = Vector3.zero;

    public static T GetRandomElement<T>(this IList<T> list) where T : class
    {
        if (list.Count > 0)
        {
            return list[Random.Range(0, list.Count - 1)];
        }
        else
        {
            Debug.LogError("Tried to get random element from empty list");
            return null;
        }
    }

    public static void MoveBy(this Transform originalTransform, float x = 0, float y = 0, float z = 0)
    {
        moveVector.x = originalTransform.localPosition.x + x;
        moveVector.y = originalTransform.localPosition.y + y;
        moveVector.z = originalTransform.localPosition.z + z;
        originalTransform.localPosition = moveVector;
    }

    public static void MoveBy(this Transform originalTransform, Vector3 modPosition)
    {
        MoveBy(originalTransform, modPosition.x, modPosition.y, modPosition.z);
    }

    /// <summary>
    /// �������� ��������� ������ �� ������
    /// </summary>
    /// <returns></returns>
    public static object OneOf(params object[] objects)
    {
        if (objects.Length > 0)
        {
            return objects[Random.Range(0, objects.Length - 1)];
        }
        else
        {
            Debug.LogError($"Not enough objects in params");
            return null;
        }
    }
}