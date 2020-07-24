using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MH_Room : MonoBehaviour
{
    public MH_Doorway[] doorways;
    public MeshCollider meshCollider;

    public Bounds RoomBounds
    {
        get { return meshCollider.bounds; }
    }
}
