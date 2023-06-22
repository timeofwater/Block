using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Blocks;

public class BlocksLocation : MonoBehaviour {
    public int row = 6; // ÐÐ
    public int col = 6; // ÁÐ

    public int[] outBlockLocation = new int[] { 1, 1 };
    public int[] inBlockLocation = new int[] { 0, 0 };

    private static int _numX;
    private static int _numY;
    private static BlockType[,] _typeArray;

    public BlocksLocation() {
        _numX = col;
        _numY = row;
        _typeArray = new BlockType[row, col];
        iniSpecialBlocks();
    }

    void Start() {
    }

    private void iniSpecialBlocks() {
        _typeArray[outBlockLocation[0], outBlockLocation[1]] = BlockType.OUT;
        _typeArray[inBlockLocation[0], inBlockLocation[1]] = BlockType.IN;
        Debug.Log("reset out and in block.");
    }

    public static int getNumX() {
        return _numX;
    }

    public static int getNumY() {
        return _numY;
    }

    public static BlockType[,] getTypeArray() {
        return _typeArray;
    }
}