using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
	[SerializeField]
	private	GameObject	tilePrefab;								// 숫자 타일 프리팹
	[SerializeField]
	private	Transform	tilesParent;                            // 타일이 배치되는 "Board" 오브젝트의 Transform

	[SerializeField]
	private List<Tile> tileList = new List<Tile>();				// 생성한 타일 정보 저장

	private	Vector2Int	puzzleSize = new Vector2Int(4, 4);		// 4x4 퍼즐
	//private	float		neighborTileDistance = 102;				// 인접한 타일 사이의 거리. 별도로 계산할 수도 있다.
	private	float		neighborTileDistance = 242;				// 인접한 타일 사이의 거리. 별도로 계산할 수도 있다.

	public	Vector3		EmptyTilePosition { set; get; }			// 빈 타일의 위치

	public GameObject	slidingPuzzlePanel;                     // 슬라이딩 퍼즐 패널
	public GameObject	kidRoomConsole;

	// 사운드 매니저
	SoundManager soundManager;

    private void Awake()
    {
        soundManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<SoundManager>();
    }

    // 콘솔 클릭 시 보드 트릭 시작
    public void BoardStart()
	{
		StartCoroutine(BStart());
	}

	private IEnumerator BStart()
	{
		foreach (var tile in tileList)
		{
			Destroy(tile.gameObject);
		}
		tileList.Clear();

        SpawnTiles();

        LayoutRebuilder.ForceRebuildLayoutImmediate(tilesParent.GetComponent<RectTransform>());

        // 현재 프레임이 종료될 때까지 대기
        yield return new WaitForEndOfFrame();

        // tileList에 있는 모든 요소의 SetCorrectPosition() 메소드 호출
        tileList.ForEach(x => x.SetCorrectPosition());

        StartCoroutine("OnSuffle");
    }

	private void SpawnTiles()
	{
		for ( int y = 0; y < puzzleSize.y; ++ y )
		{
			for ( int x = 0; x < puzzleSize.x; ++ x )
			{
				GameObject clone = Instantiate(tilePrefab, tilesParent);
				Tile tile = clone.GetComponent<Tile>();

				tile.Setup(this, puzzleSize.x * puzzleSize.y, y * puzzleSize.x + x + 1);

				tileList.Add(tile);
			}
		}
	}

	private IEnumerator OnSuffle()
	{
		float current	= 0;
		float percent	= 0;
		float time		= 1.5f;

		while ( percent < 1 )
		{
			current += Time.deltaTime;
			percent = current / time;

			int index = Random.Range(0, puzzleSize.x * puzzleSize.y);
			tileList[index].transform.SetAsLastSibling();

			yield return null;
		}

		// 원래 셔플 방식은 다른 방식이었는데 UI, GridLayoutGroup을 사용하다보니 자식의 위치를 바꾸는 것으로 설정
		// 그래서 현재 타일리스트의 마지막에 있는 요소가 무조건 빈 타일
		EmptyTilePosition = tileList[tileList.Count-1].GetComponent<RectTransform>().localPosition;
	}

	public void IsMoveTile(Tile tile)
	{
		if ( Vector3.Distance(EmptyTilePosition, tile.GetComponent<RectTransform>().localPosition) == neighborTileDistance)
		{
			SoundManager.instance.SFXPlay("dragslide");

			Vector3 goalPosition = EmptyTilePosition;

			EmptyTilePosition = tile.GetComponent<RectTransform>().localPosition;

			tile.OnMoveTo(goalPosition);
		}
	}

	public void IsGameOver()
	{
		List<Tile> tiles = tileList.FindAll(x => x.IsCorrected == true);

		Debug.Log("Correct Count : "+tiles.Count);
		if ( tiles.Count == puzzleSize.x * puzzleSize.y - 1  && !kidRoomConsole.GetComponent<KidRoomConsole>().IsSolved())
		{
			Debug.Log("GameClear");
			kidRoomConsole.GetComponent<KidRoomConsole>().isGameWin = true;
			kidRoomConsole.GetComponent<KidRoomConsole>().TrySolve(kidRoomConsole);
		}
	}
}
