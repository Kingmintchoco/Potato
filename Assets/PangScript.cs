using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PangScript : GameRuleBase
{
    override protected void Start()
    {
        base.Start();
        FindPangVegetable();
    }

    // ���� �� �ִ��� Ȯ��
    void FindPangVegetable()
    {
        List<List<VegetableDoll>> board = _gameBoard.GetBoard();

        for (int y = 0; y < _gameBoardSize.y; ++y)
        {
            for (int x = 0; x < _gameBoardSize.x; ++x)
            {
                Vector2Int pos = new Vector2Int(x, y);
                List<Vector2Int> pangPositions = new List<Vector2Int>();

                pangPositions.Add(pos);
                pangPositions.AddRange(FindRowPangBlockIndices(pos, board));
                pangPositions.AddRange(FindColumnPangBlockIndices(pos, board));

                if (pangPositions.Count >= MINIMUM_ERASE_COUNT)
                {
                    Debug.Log("������");

                    _gameBoard.DestroyVegetable(pangPositions);
                    board = _gameBoard.GetBoard();
                }
            }
        }
    }

    // �ش� ���⿡ �ִ� ���ӵ� ������ ������ ä�� �������� ������ �Լ�
    List<Vector2Int> GetVegetablePositionInDirection(Vector2Int position, Vector2Int direction, List<List<VegetableDoll>> board)
    {
        List<Vector2Int> indices = new List<Vector2Int>();
        int vegetableType = board[position.x][position.y].GetVegetableType();

        while(true)
        {
            position += direction;
            if (!IsSameVegatableType(position, vegetableType, board))
            {
                break;
            }
            else
            {
                indices.Add(position);
            }
        }

        return indices;
    }

    // �¿�� ���� ���� ������ ���� �Լ�
    List<Vector2Int> FindRowPangBlockIndices(Vector2Int position, List<List<VegetableDoll>> board)
    {
        List<Vector2Int> indices = new List<Vector2Int>();
        Vector2Int dir = new Vector2Int(-1, 0);

        indices.AddRange(GetVegetablePositionInDirection(position, dir, board));
        indices.AddRange(GetVegetablePositionInDirection(position, -dir, board));

        if (indices.Count < 2)
        {
            indices.Clear();
        }

        return indices;
    }

    List<Vector2Int> FindColumnPangBlockIndices(Vector2Int position, List<List<VegetableDoll>> board)
    {
        List<Vector2Int> indices = new List<Vector2Int>();
        Vector2Int dir = new Vector2Int(0, -1);

        indices.AddRange(GetVegetablePositionInDirection(position, dir, board));
        indices.AddRange(GetVegetablePositionInDirection(position, -dir, board));

        if (indices.Count < 2)
        {
            indices.Clear();
        }

        return indices;
    }
    
    bool IsSameVegatableType(Vector2Int position, int type, List<List<VegetableDoll>> board)
    {
        if(!IsInside(position.x, position.y))
        {
            return false;
        }

        return type.Equals(board[position.x][position.y].GetVegetableType());
    }
}
