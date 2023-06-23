using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Level {
    public class TileLayingController {
        private Tilemap _map;

        public TileLayingController(Tilemap map) {
            _map = map;
        }

        public void highlights(BlocksLocation location, List<Point> points) {
            int x;
            int y;
            for (int i = 0; i < location.getRow(); i++) {
                for (int j = 0; j < location.getCol(); j++) {
                    _map.SetColor(new Vector3Int(i, j), Color.white);
                }
            }

            for (int i = 0; i < points.Count; i++) {
                x = points[i].x;
                y = points[i].y;
                _map.SetColor(new Vector3Int(x, y), Color.gray);
            }
        }
    }
}