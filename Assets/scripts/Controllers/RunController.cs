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

        private Dictionary<BlockType, Sprite> sprites;
        private BlocksLocation location;
        private PathController pathController;
        private TileLayingController tileLayingController;

        private void Start() {
            sprites = new Dictionary<BlockType, Sprite>();
            sprites.Add(BlockType.OUT, outBlock);
            sprites.Add(BlockType.IN, inBlock);
            sprites.Add(BlockType.EMPTY, emptyBlock);
            sprites.Add(BlockType.COMMON, commonBlock);
            sprites.Add(BlockType.STONE, stoneBlock);

            location = new BlocksLocation(row, col);
            pathController = new PathController();
            tileLayingController = new TileLayingController(tilemap, sprites);

            generateLv1();
        }

        private void generateLv1() {
            location = tileLayingController.lay(location, 1, 2, BlockType.COMMON);
            location = tileLayingController.lay(location, 2, 2, BlockType.COMMON);
            location = tileLayingController.lay(location, 3, 2, BlockType.COMMON);
            location = tileLayingController.lay(location, 3, 3, BlockType.COMMON);
            location = tileLayingController.lay(location, 3, 4, BlockType.COMMON);
            location = tileLayingController.lay(location, 4, 3, BlockType.COMMON);
            location = tileLayingController.lay(location, 4, 4, BlockType.COMMON);
            location = tileLayingController.generateWall(location);
            location = tileLayingController.generateOutAndIn(location, start[0], start[1], end[0], end[1]);

            Point s = new Point(start[0], start[1]);
            Point e = new Point(end[0], end[1]);
            List<Point> path = pathController.highlightPath(location, s, e);
        }
    }
}