using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Blocks {
    public class PathController : MonoBehaviour {
        void Start() {
            Debug.Log("path is start.");
            highlightPath();
        }

        private void Update() {
        }

        public void highlightPath() {
            Astar2d path = new Astar2d();
            path.initMap(new int[2, 2] { { 0, 0 }, { 0, 0 } }, 2);
            List<Point> points = path.GetPath(new Point(0, 0), new Point(1, 1));
            Debug.Log("points.Count: " + points.Count);
            for (int i = 0; i < points.Count; i++) {
                Debug.Log(i + ":" + points[i].x + ", " + points[i].y);
            }
        }
    }
}