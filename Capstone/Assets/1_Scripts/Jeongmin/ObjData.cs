using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjData : MonoBehaviour
{
    public enum GameObjectTypes
    {
        NPC,
        Button,
    }

    public GameObjectTypes _type;
    public int _id;
}
