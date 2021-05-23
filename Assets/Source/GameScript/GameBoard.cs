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
    public List<VegetableDoll> vegetableBlocks;


    public List<Texture2D> ltd;

    List<List<VegetableDoll>> _board;

    // Start is called before the first frame update
    void Start()
    {
        // 블럭 영역을 읽어옴
        Vector2 areaSize = dollArea.rect.size;
        _vegetableBlockRocalScale = (areaSize / _blockXYCount) / _RATIO;
        _blockOffset = _vegetableBlockRocalScale * _RATIO;

        // board 초기화
        _board = new List<List<VegetableDoll>>();
        
        for(int i = 0; i < _blockXYCount.x; ++i)
        {
            _board.Add(new List<VegetableDoll>());
        }

        // Blocks 배치
        ReFillBoard();
        Reposition();
    }

    // 블럭 삭제
    public void DestroyVegetable(List<Vector2Int> vegetablePositions)
    {
        foreach(Vector2Int vegetablePos in vegetablePositions)
        {
            Destroy(_board[vegetablePos.x][vegetablePos.y]);
            //_board[vegetablePos.x][vegetablePos.y] = null;
        }

        Refresh();
        ReFillBoard();
        Reposition();
    }

    // 블럭 재생성
    void ReFillBoard()
    {
        for (int x = 0; x < _blockXYCount.x; ++x)
        {
            while(_board[x].Count != _blockXYCount.y)
            {
                int vegetableType = Random.Range(0, vegetableBlocks.Count);
                // Block 생성
                _board[x].Add(Instantiate<VegetableDoll>(vegetableBlocks[vegetableType], transform));
                _board[x][_board[x].Count - 1].SetVegetableType(vegetableType);
                _board[x][_board[x].Count - 1].transform.localScale = _vegetableBlockRocalScale;
            }
        }
    }

    // 블럭 위치 재조정
    void Reposition()
    {
        Vector2 areaSize = dollArea.rect.size;

        for (int x = 0; x < _blockXYCount.x; ++x)
        {
            for (int y = 0; y < _blockXYCount.y; ++y)
            {
                _board[x][y].transform.localPosition = (_blockOffset - areaSize) / 2.0f + _blockOffset * new Vector2(y, x);
            }
        }
    }

    // List에서 제거된 block 삭제
    void Refresh()
    {
        for (int x = 0; x < _blockXYCount.x; ++x)
        {
            for (int y = 0; y < _blockXYCount.y; ++y)
            {
                if(_board[x][y] == null)
                {
                    _board[x].RemoveAt(y);
                    --y;
                }
            }
        }
    }

    // Get 함수
    public Vector2 GetBoardSize()
    {
        return _blockXYCount;
    }

    public List<List<VegetableDoll>> GetBoard()
    {
        return _board;
    }
}
