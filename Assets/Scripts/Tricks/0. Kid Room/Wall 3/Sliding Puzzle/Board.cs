using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
	[SerializeField]
	private	GameObject	tilePrefab;
	[SerializeField]
	private	Transform	tilesParent;

	[SerializeField]
	private List<Tile> tileList = new List<Tile>();

	private	Vector2Int	puzzleSize = new Vector2Int(3, 3);
	private	float		neighborTileDistance = 322;

	public	Vector3		EmptyTilePosition { set; get; }

	public GameObject	slidingPuzzlePanel;
	public GameObject	console;


    private void OnEnable()
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

        yield return new WaitForEndOfFrame();

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

		EmptyTilePosition = tileList[tileList.Count-1].GetComponent<RectTransform>().localPosition;
	}

	public void IsMoveTile(Tile tile)
	{
		if ( Vector3.Distance(EmptyTilePosition, tile.GetComponent<RectTransform>().localPosition) == neighborTileDistance)
		{
			GameManager.Instance.soundManager.Play("dragslide");

			Vector3 goalPosition = EmptyTilePosition;

			EmptyTilePosition = tile.GetComponent<RectTransform>().localPosition;

			tile.OnMoveTo(goalPosition);
		}
	}

	public void IsGameOver()
	{
		List<Tile> tiles = tileList.FindAll(x => x.IsCorrected == true);

		if ( tiles.Count == puzzleSize.x * puzzleSize.y - 1  && !console.GetComponent<Console>().IsComplete)
		{
			console.GetComponent<Console>().Clear();
		}
	}
}
