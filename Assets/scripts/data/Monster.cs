using System;
using System.Collections.Generic;
using UnityEngine;

namespace Level {
    public class Monster {
        private float _HP;
        private float _speed;
        private bool _isFlying;
        private GameObject _gameObject;

        private int _originX;
        private int _originY;
        private float _x;
        private float _y;
        private int _nextBlockX;
        private int _nextBlockY;
        private int _step;
        private List<Point> _path;

        public Monster(MonsterType monsterType) {
            switch (monsterType) {
                case MonsterType.COOKIE:
                    _HP = 10;
                    _speed = 1;
                    _isFlying = false;
                    break;
                case MonsterType.CREAM_COOKIE:
                    _HP = 20;
                    _speed = 1;
                    _isFlying = false;
                    break;
                default:
                    _HP = 10;
                    _speed = 1;
                    _isFlying = false;
                    break;
            }

            _originX = -114;
            _originY = -514;
            _x = -114;
            _y = -514;
            _nextBlockX = -114;
            _nextBlockY = -514;
            _step = 0;
            _path = new List<Point>();
        }

        public void setGameObject(GameObject gameObject) {
            _gameObject = gameObject;
        }

        public bool setPoisition(BlocksLocation location, float x, float y) {
            int xI = (int)x;
            int yI = (int)y;
            switch (location.getBlockType(xI, yI)) {
                case BlockType.EMPTY:
                case BlockType.STONE:
                    if (_isFlying) {
                        _originX = xI;
                        _originY = yI;
                        _x = x;
                        _y = y;
                        _nextBlockX = xI;
                        _nextBlockY = yI;
                        _step = 0;
                        return true;
                    }
                    else {
                        return false;
                    }
                default:
                    _originX = xI;
                    _originY = yI;
                    _x = x;
                    _y = y;
                    _nextBlockX = xI;
                    _nextBlockY = yI;
                    _step = 0;
                    return true;
            }
        }

        public void setPath(List<Point> path) {
            _path = path;
            if (_path.Count >= 1) {
                _step = 0;
                _originX = _path[_step].x;
                _originY = _path[_step].y;
                _nextBlockX = _path[_step + 1].x;
                _nextBlockY = _path[_step + 1].y;
            }
        }

        public bool move() {
            if (_path.Count == 0) {
                return false;
            }
            else {
                if ((_x - _nextBlockX) * (_x - _nextBlockX) * (_y - _nextBlockY) * (_y - _nextBlockY) <
                    MonsterController.TICK * _speed * MonsterController.TICK * _speed) {
                    _step += 1;
                    if (_path.Count > 1 + _step) {
                        _originX = _path[_step].x;
                        _originY = _path[_step].y;
                        _nextBlockX = _path[_step + 1].x;
                        _nextBlockY = _path[_step + 1].y;
                    }
                }

                float deltaX = (_nextBlockX - _originX) * _speed * MonsterController.TICK;
                float deltaY = (_nextBlockY - _originY) * _speed * MonsterController.TICK;
                _x += deltaX;
                _y += deltaY;
                _gameObject.transform.Translate(new Vector3(deltaX, deltaY));
                return true;
            }
        }

        public bool damageAndIsDie(int d) {
            damage(d);
            if (_HP <= 0) {
                return true;
            }
            else {
                return false;
            }
        }

        // calculate (damage - defense) here
        private void damage(int d) {
            _HP -= d;
        }

        public void setNextBlock(int x, int y) {
            _nextBlockX = x;
            _nextBlockY = y;
        }

        public float getHP() {
            return _HP;
        }

        public float getSpeed() {
            return _speed;
        }

        public bool getIsFlying() {
            return _isFlying;
        }

        public float getX() {
            return _x;
        }

        public float getY() {
            return _y;
        }

        public int getIntX() {
            return (int)_x;
        }

        public int getIntY() {
            return (int)_y;
        }

        public int getNextBlockX() {
            return _nextBlockX;
        }

        public int getNextBlockY() {
            return _nextBlockY;
        }
    }
}