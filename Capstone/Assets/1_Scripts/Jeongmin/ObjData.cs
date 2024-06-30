using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjData : MonoBehaviour
{
    public enum GameObjectTypes
    {
        Player,
        Object,
    }

    public GameObjectTypes _type;
    public int _id;
    public bool _isBtn;
}
