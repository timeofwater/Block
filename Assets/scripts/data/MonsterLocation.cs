using System.Collections.Generic;

namespace Level {
    public class MonsterLocation {
        private int _col;
        private int _row;
        private int id;
        private Dictionary<int, Monster> _monsters;

        public MonsterLocation(int row, int col) {
            _col = col;
            _row = row;
            id = 114;
            _monsters = new Dictionary<int, Monster>();
        }

        public int addMonster(Monster monster) {
            _monsters.Add(id, monster);
            id += 1;
            return id - 1;
        }

        public bool removeMonster(int id) {
            if (_monsters.ContainsKey(id)) {
                _monsters.Remove(id);
                return true;
            }
            else {
                return false;
            }
        }

        /**
         * <p>*if the list don't have the monster, this function will <b>return null</b></p>
         */
        public Monster getMonster(int id) {
            if (_monsters.ContainsKey(id)) {
                return _monsters[id];
            }
            else {
                return null;
            }
        }
    }
}