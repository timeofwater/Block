using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Level {
    public class PathController {
        public List<Point> highlightPath(BlocksLocation location, Point startP, Point endP) {
            Astar2d path = new Astar2d();
            path.initMap(turnBlockToInteneger(location.getMap()), location.getRow());
            List<Point> points = path.GetPath(startP, endP);

            Debug.Log("points.Count: " + points.Count);

            string pathLog = "";
            for (int i = 0; i < points.Count; i++) {
                pathLog += "[" + i + "](" + points[i].x + "," + points[i].y + ")  ->";
            }

            Debug.Log(pathLog);

            return points;
        }

        private int[,] turnBlockToInteneger(BlockType[,] blockTypes) {
            int[,] rs = new int[blockTypes.GetLength(0), blockTypes.GetLength(1)];
            for (int i = 0; i < blockTypes.GetLength(0); i++) {
                for (int j = 0; j < blockTypes.GetLength(1); j++) {
                    switch (blockTypes[i, j]) {
                        case BlockType.EMPTY:
                        case BlockType.STONE:
                            rs[i, j] = 1;
                            break;
                        default:
                            rs[i, j] = 0;
                            break;
                    }
                }
            }

            return rs;
        }
    }
}