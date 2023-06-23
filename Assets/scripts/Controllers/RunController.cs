using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.WSA;

namespace Level {
    public class RunController : MonoBehaviour {
        public int col = 6; // ÁÐ
        public int row = 6; // ÐÐ

        public int[] start = new int[] { 0, 0 };
        public int[] end = new int[] { 3, 4 };

        public Tilemap tilemap;

        private void Start() {
            BlocksLocation location = new BlocksLocation(row, col);
            PathController pathController = new PathController();
            TileLayingController tileLayingController = new TileLayingController(tilemap);


            Point startPoint = new Point(start[0], start[1]);
            Point endPoint = new Point(end[0], end[1]);
            List<Point> path = pathController.highlightPath(location, startPoint, endPoint);
            tileLayingController.highlights(location, path);
        }
    }
}