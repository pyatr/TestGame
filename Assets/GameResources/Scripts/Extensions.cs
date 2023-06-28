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
            return list[Random.Range(0, list.Count)];
        }
        else
        {
            Debug.LogError("Tried to get random element from empty list");
            return null;
        }
    }

    public static void DestroyGameObjects<T>(this IList<T> gameObjectList) where T : Component
    {
        int objectCount = gameObjectList.Count;
        for (int i = 0; i < objectCount; i++)
        {
            if (gameObjectList[i])
            {
                Object.Destroy(gameObjectList[i].gameObject);
            }
            else
            {
                Debug.LogError($"Tried to destroy null object {i}");
            }
        }
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