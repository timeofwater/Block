using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Level;

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