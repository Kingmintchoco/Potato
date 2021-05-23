using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRuleBase : MonoBehaviour
{
    protected readonly int MINIMUM_ERASE_COUNT = 3;
    protected Vector2 _gameBoardSize;
    protected GameBoard _gameBoard;

    virtual protected void Start()
    {
        _gameBoard = GetComponent<GameBoard>();
        _gameBoardSize = _gameBoard.GetBoardSize();
    }

    protected bool IsInside(int xpos, int ypos)
    {
        if (xpos < 0 || xpos >= _gameBoardSize.x)
        {
            return false;
        }
        else if (ypos < 0 || ypos >= _gameBoardSize.y)
        {
            return false;
        }

        return true;
    }
}
