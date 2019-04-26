using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{

}

[Serializable]
public class StringInt
{
    public string key;
    public int value;

    public StringInt(string k, int v)
    {
        key = k;
        value = v;
    }
}

[Serializable]
public class IntString
{
    public int key;
    public string value;

    public IntString(int k, string v)
    {
        key = k;
        value = v;
    }
}

[Serializable]
public class StringFloat
{
    public string key;
    public float value;

    public StringFloat(string k, float v)
    {
        key = k;
        value = v;
    }
}

[Serializable]
public class IntEnemy
{
    public int key;
    public Enemy enemy;

    public IntEnemy(int k, Enemy v)
    {
        key = k;
        enemy = v;
    }
}