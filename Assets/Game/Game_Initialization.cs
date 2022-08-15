using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Game : MonoBehaviour
{
    void Start()
    {
        initializeGame();   
    }

    void initializeGame()
    {
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
    }
}
