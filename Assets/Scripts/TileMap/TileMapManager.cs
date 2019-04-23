using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using System.Linq;
using TiledSharp;
using UnityEngine;

public class TileMapManager : MonoBehaviour
{
    private bool inLevel = false;
    private int currentLevelIndex = -1;
    [SerializeField]
    private LevelConfig levelConfig = null;
    [SerializeField]
    private Texture2D spriteAtlas = null;
    private Sprite[] sprites = null;
    [SerializeField]
    private Tile tilePrefab = null;

    private GameObject background = null;
    private GameObject colliders = null;

    // Start is called before the first frame update
    void Start()
    {
        sprites = Resources.LoadAll<Sprite>("Tilesets/"+spriteAtlas.name);
        GameObject empty = new GameObject();
        background = Instantiate(empty, transform);
        background.name = "Background";
        colliders = Instantiate(empty, transform);
        colliders.name = "Colliders";
    }

    // Update is called once per frame
    void Update()
    {
        //TODO this is game manager stuff
        if(!inLevel)
        {
            //init game
            LoadLevel(levelConfig.Levels[0].text, levelConfig.Tileset.text);
            inLevel = true;
        }
    }

    public void LoadLevel(string level, string tilesetRef)
    {
        Debug.Log(sprites.Length);
        //TextAsset asset = Resources.Load<TextAsset>(level);
        XDocument doc = XDocument.Parse(level);
        XDocument tilesetDoc = XDocument.Parse(tilesetRef);
        TmxMap map = new TmxMap(doc);
        map.AddTilesetsAsXContainers(tilesetDoc.Elements("tileset").ToList());
        TmxTileset tileset = map.Tilesets[0];
        /*TmxTileset tileset = new TmxTileset(
            doc.Element("map").Elements("tileset").FirstOrDefault(),
            tilesetDoc.Element("tileset"),
            map.TmxDirectory);*/

        int layerInt = 0;
        foreach (TmxLayer layer in map.Layers)
        {
            bool disableCollider = layer.Name != "colliders" && layer.Name != "objects";
            GameObject parent = disableCollider ? background : colliders;

            StringBuilder row = new StringBuilder();
            foreach (TmxLayerTile tile in layer.Tiles)
            {
                if (tile.Gid <= 0 || !tileset.Tiles.ContainsKey(tile.Gid)) continue;
                TmxTilesetTile tilesetTile = tileset.Tiles[tile.Gid-1];

                row.Append(tile.Gid + ", ");

                Tile tileObj = Instantiate(tilePrefab, parent.transform);
                SpriteRenderer renderer = tileObj.GetComponent<SpriteRenderer>();
                renderer.sprite = sprites[tile.Gid-1];
                tileObj.transform.position = new Vector3(tile.X-10, -tile.Y+8.5f, 0);
                tileObj.gameObject.layer = LayerMask.NameToLayer(layer.Name);
                tileObj.SetLayerInt(layerInt);

                tileObj.SetCollider(tilesetTile.Properties["collider"]);
                /*if(tilesetTile.Properties["collider"] == "full"
                    || tilesetTile.Properties["collider"] == "semi"
                    || tilesetTile.Properties["collider"] == "stairs")
                {
                    tileObj.GetComponent<BoxCollider2D>().enabled = true;
                }
                else
                {
                    tileObj.GetComponent<BoxCollider2D>().enabled = false;
                }*/
            }
            //Debug.Log(row.ToString());
            layerInt++;
        }
    }
}
