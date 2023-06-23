using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;

namespace Level {
    public class MonsterController : MonoBehaviour {
        public static float TICK = 0.25f;
        public GameObject parent;
        public GameObject rabbitPrefab;

        private List<Monster> _monsters;
        private float t;

        private void Start() {
            _monsters = new List<Monster>();
            t = 0;

            Monster testMonster = new Monster(MonsterType.RABBIT);
            RunController runController = this.gameObject.GetComponent<RunController>();

            GameObject rabbit = GameObject.Instantiate(rabbitPrefab, parent.transform);
            testMonster.setGameObject(rabbit);
            BlocksLocation location = runController.getBlockLocation();
            int[] s = runController.start;
            testMonster.setPoisition(location, s[0], s[1]);
            List<Point> path = runController.getPath();
            testMonster.setPath(path);
            _monsters.Add(testMonster);
            Debug.Log("monsters loaded.");
        }

        private void Update() {
            if (t >= TICK) {
                for (int i = 0; i < _monsters.Count; i++) {
                    if (!_monsters[i].move()) {
                        _monsters.RemoveAt(i);
                    }

                    _monsters[i].move();
                }

                t = 0;
            }
            else {
                t += Time.deltaTime;
            }
        }
    }
}