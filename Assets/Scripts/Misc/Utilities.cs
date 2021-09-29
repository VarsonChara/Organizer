using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ListHandler<T>
{
    public static T[] CopyList(List<T> listToCopy)
    {
        T[] array = new T[listToCopy.Count];
        listToCopy.CopyTo(array);

        return array;
    }
}
