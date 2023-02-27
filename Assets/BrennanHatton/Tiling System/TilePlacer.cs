using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePlacer : MonoBehaviour
{
	public Tile tilePrefab;
	public List<Tile> tilesPlaced;
	public Transform origin;
	public Transform next;
	public float number = 10;
	
	void Reset()
	{
		origin = this.transform;
	}
	
    // Start is called before the first frame update
    void Start()
	{
		Setup();
	}
    
	public void Setup()
	{
		next.transform.position = origin.position;
		next.transform.rotation = origin.rotation;
	}
    
	public void PlaceTiles(float numberOfTiles)
	{
		for(int i = 0; i < numberOfTiles; i++)
			PlaceNexTile();
	}
	
	public void PlaceNexTile()
	{
		Tile tile = Instantiate<Tile>(tilePrefab, next.position, next.rotation, origin);
		
		tilesPlaced.Add(tile);
		
		next.transform.position += next.transform.forward*tile.length;
	
	}
}
