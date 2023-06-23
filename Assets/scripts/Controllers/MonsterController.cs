using System.Collections.Generic;

namespace Level {
    public class MonsterController {

        public static float tick = 0.25f;

        private List<Monster> _monsters;

        public MonsterController() {
            _monsters = new List<Monster>();
            _monsters.Add(new Monster(MonsterType.COOKIE));
        }

        public void goahead() {

        }

    }
}