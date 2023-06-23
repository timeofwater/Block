namespace Level {
    public class BlocksLocation {
        private int _col; // x
        private int _row; // y
        private BlockType[,] _typeArray;

        public BlocksLocation(int row, int col) {
            _row = row;
            _col = col;
            _typeArray = new BlockType[row, col];
        }

        public void setBlockType(int x, int y, BlockType blockType) {
            _typeArray[x, y] = blockType;
        }

        public BlockType getBlockType(int x, int y) {
            if (0 <= x && x < _col && 0 <= y && y < _row) {
                return _typeArray[x, y];
            }
            else {
                return BlockType.EMPTY;
            }
        }

        public int getCol() {
            return _col;
        }

        public int getRow() {
            return _row;
        }

        public BlockType[,] getMap() {
            return _typeArray;
        }
    }
}