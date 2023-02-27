using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(TilePlacer))]
public class EditorTilePlacer : MonoBehaviour
{
	TilePlacer placer;
	public List<Tile> tilesPlaced;
	
	void Reset()
	{
		
		placer = this.GetComponent<TilePlacer>();
		placer.Setup();
		PlaceTiles(placer.number);
		
		// We need to tell Unity we're changing the component object too.
		Undo.RecordObject(placer, "Creating Tiles");
	}
    
	public void PlaceTiles(float numberOfTiles)
	{
		for(int i = 0; i < numberOfTiles; i++)
			PlaceNexTile();
	}
	
	public void PlaceNexTile()
	{
		Selection.activeObject = PrefabUtility.InstantiatePrefab(placer.tilePrefab as Object, placer.origin);
		Tile tile = Selection.activeObject as Tile;
		//Debug.Log(go);
		
		//Tile tile = go.GetComponent<Tile>();//, placer.next.position, placer.next.rotation, placer.origin);
		tile.transform.position = placer.next.position;
		tile.transform.rotation = placer.next.rotation;
		
		tilesPlaced.Add(tile);
		
		placer.next.transform.position += placer.next.transform.forward*tile.length;
		Selection.activeObject = this;
    }
}
