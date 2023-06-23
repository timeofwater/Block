using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.WSA;

namespace Level {
    public class RunController : MonoBehaviour {
        public int col = 6; // ÁÐ
        public int row = 6; // ÐÐ

        public int[] start = new int[] { 1, 1 };
        public int[] end = new int[] { 3, 4 };

        public Tilemap tilemap;
        public Sprite outBlock;
        public Sprite inBlock;
        public Sprite emptyBlock;
        public Sprite commonBlock;
        public Sprite stoneBlock;

        private Dictionary<BlockType, Sprite> _sprites;
        private BlocksLocation _blocksLocation;
        private PathController _pathController;
        private TileLayingController _tileLayingController;
        private MonsterController _monsterController;

        private void Start() {
            _sprites = new Dictionary<BlockType, Sprite>();
            _sprites.Add(BlockType.OUT, outBlock);
            _sprites.Add(BlockType.IN, inBlock);
            _sprites.Add(BlockType.EMPTY, emptyBlock);
            _sprites.Add(BlockType.COMMON, commonBlock);
            _sprites.Add(BlockType.STONE, stoneBlock);

            _blocksLocation = new BlocksLocation(row, col);
            _pathController = new PathController();
            _tileLayingController = new TileLayingController(tilemap, _sprites);
            _monsterController = new MonsterController();

            generateLv1();
        }

        private void Update() {
            _monsterController.goahead();
        }

        private void generateLv1() {
            _blocksLocation = _tileLayingController.lay(_blocksLocation, 1, 2, BlockType.COMMON);
            _blocksLocation = _tileLayingController.lay(_blocksLocation, 2, 2, BlockType.COMMON);
            _blocksLocation = _tileLayingController.lay(_blocksLocation, 3, 2, BlockType.COMMON);
            _blocksLocation = _tileLayingController.lay(_blocksLocation, 3, 3, BlockType.COMMON);
            _blocksLocation = _tileLayingController.lay(_blocksLocation, 3, 4, BlockType.COMMON);
            _blocksLocation = _tileLayingController.lay(_blocksLocation, 4, 3, BlockType.COMMON);
            _blocksLocation = _tileLayingController.lay(_blocksLocation, 4, 4, BlockType.COMMON);
            _blocksLocation = _tileLayingController.generateWall(_blocksLocation);
            _blocksLocation = _tileLayingController.generateOutAndIn(_blocksLocation,
                start[0],
                start[1],
                end[0],
                end[1]);

            Point s = new Point(start[0], start[1]);
            Point e = new Point(end[0], end[1]);
            List<Point> path = _pathController.highlightPath(_blocksLocation, s, e);
        }
    }
}