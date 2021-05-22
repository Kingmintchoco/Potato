using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameBoard : MonoBehaviour
{
    readonly float _RATIO = 100.0f;

    [SerializeField] Vector2 _blockXYCount;
    Vector2 _blockOffset;
    Vector2 _vegetableBlockRocalScale;

    public RectTransform dollArea;
    public List<Transform> vegetableBlocks;

    List<List<Transform>> _board;

    // �ӽ�
    List<List<int>> _vegetableType;

    // Start is called before the first frame update
    void Start()
    {
        // �� ������ �о��
        Vector2 areaSize = dollArea.rect.size;
        _vegetableBlockRocalScale = (areaSize / _blockXYCount) / _RATIO;
        _blockOffset = _vegetableBlockRocalScale * _RATIO;

        // board �ʱ�ȭ
        _board = new List<List<Transform>>();
        _vegetableType = new List<List<int>>();

        for(int i = 0; i < _blockXYCount.x; ++i)
        {
            _board.Add(new List<Transform>());
            _vegetableType.Add(new List<int>());
        }

        // Blocks ��ġ
        ReFillBoard();
        Refresh();

        //FindPangVegetable();
    }

     bool IsInside(int xpos, int ypos)
     {
        if(xpos < 0 || xpos >= _blockXYCount.x)
        {
            return false;
        }
        else if(ypos < 0 || ypos >= _blockXYCount.x)
        {
            return false;
        }

        return true;
     }

    // �¿�� ���� ���� ������ ���� �Լ�
    int FindRowPangBlockCount(int xpos, int ypos, int dir, int type)
    {
        if(!IsInside(xpos, ypos))
        {
            return 0;
        }
        else if(_vegetableType[xpos][ypos] != type)
        {
            return 0;
        }

        return 1 + FindRowPangBlockCount(xpos + dir, ypos, dir, type);
    }

    int FindColumnPangBlockCount(int xpos, int ypos, int dir, int type)
    {
        if (!IsInside(xpos, ypos))
        {
            return 0;
        }
        else if (_vegetableType[xpos][ypos] != type)
        {
            return 0;
        }

        return 1 + FindRowPangBlockCount(xpos, ypos + dir, dir, type);
    }

    // ���� �� �ִ��� Ȯ��
    bool FindPangVegetable()
    {
        for (int y = 0; y < _blockXYCount.y; ++y)
        {
            for (int x = 0; x < _blockXYCount.x; ++x)
            {
                int rightPangCount = FindRowPangBlockCount(x + 1, y, 1, _vegetableType[x][y]);
                int leftPangCount = FindRowPangBlockCount(x - 1, y, -1, _vegetableType[x][y]);
                int rowPangCount = rightPangCount + leftPangCount + 1;

                int upPangCount = FindColumnPangBlockCount(x, y + 1, 1, _vegetableType[x][y]);
                int downPangCount = FindColumnPangBlockCount(x, y - 1, -1, _vegetableType[x][y]);
                int columnPangCount = upPangCount + downPangCount + 1;

                if(rowPangCount >= 3 || columnPangCount >= 3)
                {
                    Destroy(_board[x][y]);
                    _vegetableType[x].RemoveAt(y);

                    if(rowPangCount >= 3)
                    {
                        DestroyRowVegetable(x, y, leftPangCount, rightPangCount);
                    }
                    if(columnPangCount >= 3)
                    {
                        DestroyColumnVegetable(x, y, upPangCount, downPangCount);
                    }

                    ReFillBoard();
                    //Refresh();
                }
            }
        }

        return false;
    }


    // �� ���� � ����


    // �� ����
    void DestroyRowVegetable(int xpos, int ypos, int leftCount, int rightCount)
    {
        for(int x = xpos - leftCount; x <= xpos + rightCount; ++x)
        {
            if(x == xpos)
            {
                continue;
            }
            Destroy(_board[x][ypos].gameObject);
            _board[x].RemoveAt(ypos);
            _vegetableType[x].RemoveAt(ypos);
        }
    }

    void DestroyColumnVegetable(int xpos, int ypos, int upCount, int downCount)
    {
        for (int y = ypos + upCount - 1; y <= xpos + downCount; ++y)
        {
            Destroy(_board[xpos][y].gameObject);
            _board[xpos].RemoveAt(y);
            _vegetableType[xpos].RemoveAt(y);
        }
    }


    // �� �����
    void ReFillBoard()
    {
        for (int x = 0; x < _blockXYCount.x; ++x)
        {
            Debug.Log(x);
            Debug.Log(_board[x]);

            while(_board[x].Count != _blockXYCount.y)
            {

                int vegetableType = Random.Range(0, vegetableBlocks.Count);
                // Block ����
                _board[x].Add(Instantiate(vegetableBlocks[vegetableType], transform));
                _board[x][_board[x].Count - 1].localScale = _vegetableBlockRocalScale;
                _vegetableType[x].Add(vegetableType);
            }
        }
    }

    // �� ��ġ ����
    void Refresh()
    {
        Vector2 areaSize = dollArea.rect.size;

        for (int x = 0; x < _blockXYCount.x; ++x)
        {
            for (int y = 0; y < _blockXYCount.y; ++y)
            {
                _board[x][y].localPosition = (_blockOffset - areaSize) / 2.0f + _blockOffset * new Vector2(y, x);
            }
        }
    }
}
