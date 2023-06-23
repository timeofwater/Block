using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Level {
    public class TileLayingController {
        private Tilemap _map;
        private Dictionary<BlockType, Tile> _tiles;
        private Tile _outTile;
        private Tile _inTile;
        private Tile _emptyTile;
        private Tile _commonTile;
        private Tile _stoneTile;

        public TileLayingController(Tilemap map, Dictionary<BlockType, Sprite> sprites) {
            _map = map;

            createAllTile(sprites);
        }

        private void createAllTile(Dictionary<BlockType, Sprite> sprites) {
            //
            _outTile = ScriptableObject.CreateInstance<Tile>();
            _outTile.sprite = sprites[BlockType.OUT];
            _outTile.transform = new Matrix4x4(new Vector4(2, 0, 0, 0),
                new Vector4(0, 2, 0, 0),
                new Vector4(0, 0, 2, 0),
                new Vector4(0.5f, 0.5f, 0, 1));
            //
            _inTile = ScriptableObject.CreateInstance<Tile>();
            _inTile.sprite = sprites[BlockType.IN];
            _inTile.transform = new Matrix4x4(new Vector4(2, 0, 0, 0),
                new Vector4(0, 2, 0, 0),
                new Vector4(0, 0, 2, 0),
                new Vector4(0.5f, 0.5f, 0, 1));
            //
            _emptyTile = ScriptableObject.CreateInstance<Tile>();
            _emptyTile.sprite = sprites[BlockType.EMPTY];
            _emptyTile.transform = new Matrix4x4(new Vector4(1.5f, 0, 0, 0),
                new Vector4(0, 1.5f, 0, 0),
                new Vector4(0, 0, 1.5f, 0),
                new Vector4(0.5f, 0.5f, 0, 1));
            //
            _commonTile = ScriptableObject.CreateInstance<Tile>();
            _commonTile.sprite = sprites[BlockType.COMMON];
            _commonTile.transform = new Matrix4x4(new Vector4(2, 0, 0, 0),
                new Vector4(0, 2, 0, 0),
                new Vector4(0, 0, 2, 0),
                new Vector4(0.5f, 0.5f, 0, 1));
            //
            _stoneTile = ScriptableObject.CreateInstance<Tile>();
            _stoneTile.sprite = sprites[BlockType.STONE];
            _stoneTile.transform = new Matrix4x4(new Vector4(2, 0, 0, 0),
                new Vector4(0, 2, 0, 0),
                new Vector4(0, 0, 2, 0),
                new Vector4(0.5f, 0.5f, 0, 1));

            _tiles = new Dictionary<BlockType, Tile>();
            _tiles.Add(BlockType.OUT, _outTile);
            _tiles.Add(BlockType.IN, _inTile);
            _tiles.Add(BlockType.EMPTY, _emptyTile);
            _tiles.Add(BlockType.COMMON, _commonTile);
            _tiles.Add(BlockType.STONE, _stoneTile);
        }

        /**
         * <p><br/><b>warning: x,y will -1</b><br/><br/></p>
         * <p>tile name:<ul>
         * <li>out</li>
         * <li>in</li>
         * <li>empty</li>
         * <li>common</li>
         * <li>stone</li>
         * <li>out</li>
         * </ul></p>
         */
        public BlocksLocation lay(BlocksLocation location, int x, int y, BlockType tileType) {
            if (_tiles.ContainsKey(tileType)) {
                _map.SetTile(new Vector3Int(x - 1, y - 1), _tiles[tileType]);
            }

            if (0 <= x && x < location.getCol() && 0 <= y && y < location.getRow()) {
                location.setBlockType(x, y, tileType);
            }

            return location;
        }

        public BlocksLocation generateWall(BlocksLocation location) {
            int col = location.getCol();
            int row = location.getRow();
            for (int i = 0; i < col + 2; i++) {
                location = lay(location, i, 0, BlockType.STONE);
            }

            for (int i = 0; i < col + 2; i++) {
                location = lay(location, i, row + 1, BlockType.STONE);
            }

            for (int i = 0; i < row + 1; i++) {
                location = lay(location, 0, i, BlockType.STONE);
            }

            for (int i = 0; i < row + 1; i++) {
                location = lay(location, col + 1, i, BlockType.STONE);
            }

            return location;
        }

        public BlocksLocation generateOutAndIn(BlocksLocation location, int startX, int startY, int endX, int endY) {
            if (0 <= startX && startX < location.getCol() && 0 <= startY && startY < location.getRow()) {
                location = lay(location, startX, startY, BlockType.OUT);
            }
            else {
                Debug.LogWarning("the out block is out of the map. (runController.cs)");
            }

            if (0 <= endX && endX < location.getCol() && 0 <= endY && endY < location.getRow()) {
                location = lay(location, endX, endY, BlockType.IN);
            }
            else {
                Debug.LogWarning("the in block is out of the map. (runController.cs)");
            }

            return location;
        }
    }
}