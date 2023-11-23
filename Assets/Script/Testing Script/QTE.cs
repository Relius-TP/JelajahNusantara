using System.Collections.Generic;
using UnityEngine;

public class QTE
{
    private List<KeyCode> qteKey = new();

    public bool CheckKey(KeyCode key)
    {
        if (qteKey.Count > 0)
        {
            int result = qteKey[0].CompareTo(key);
            if (result == 0)
            {
                if (qteKey.Count != 0)
                {
                    qteKey.RemoveAt(0);
                }
                return false;
            }
        }
        return true;
    }

    public void GenerateRandomKey(int keysNeed)
    {
        int randomNumber;

        for (int i = 0; i < keysNeed; i++)
        {
            randomNumber = Random.Range(0, 4);

            switch (randomNumber)
            {
                case 0:
                    AddKey(KeyCode.UpArrow);
                    break;
                case 1:
                    AddKey(KeyCode.DownArrow);
                    break;
                case 2:
                    AddKey(KeyCode.LeftArrow);
                    break;
                case 3:
                    AddKey(KeyCode.RightArrow);
                    break;
            }
        }
    }

    public int ListCount()
    {
        return qteKey.Count;
    }

    public KeyCode ReadList(int index)
    {
        switch (qteKey[index])
        {
            case KeyCode.UpArrow:
                return KeyCode.UpArrow;
            case KeyCode.DownArrow:
                return KeyCode.DownArrow;
            case KeyCode.LeftArrow:
                return KeyCode.LeftArrow;
            case KeyCode.RightArrow:
                return KeyCode.RightArrow;
        }

        return KeyCode.None;
    }

    public void AddKey(KeyCode key)
    {
        qteKey.Add(key);
    }

    public void ResetList()
    {
        qteKey.Clear();
    }

    public bool IsQTEComplete()
    {
        return qteKey.Count == 0;
    }
}
