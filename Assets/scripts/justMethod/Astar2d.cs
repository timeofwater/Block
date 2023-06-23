using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point
{
    public int x, y;
    public float F, G, H;
    public Point parent;
    public Point(int _x,int _y)
    {
        this.x = _x;
        this.y = _y;
        F = 0;
        G = 0;
        H = 0;
    }
}

public class Astar2d
{

    private int Size;
    public int[,] map;
    List<Point> openList = new List<Point>();
    List<Point> closeList = new List<Point>();
    public List<Point> GizmosList;

    public void initMap(int[,] arr, int Size)
    {
        this.map = arr;
        this.Size = Size;
    }

    float CalculateG(Point start, Point tarpoint)
    {
        float tempG = Vector2.Distance(new Vector2(start.x, start.y), new Vector2(tarpoint.x, tarpoint.y));
        float parentG = tarpoint.parent == null ? 0 : tarpoint.parent.G;
        float g = tempG + parentG;
        return g;
    }//����Gֵ

    float CalculateH(Point point,Point end)
    {
        return (Mathf.Abs(end.x - point.x) + Mathf.Abs(end.y - point.y));//�����پ���

    }//����Hֵ

    float CalculateF(Point point)
    {
        return point.G + point.H;
    }//����Fֵ

    Point FindleastF()
    {
        if (openList.Count > 0)
        {
            Point curPoint = openList[0];
            foreach(Point point in openList)
            {
                if (point.F < curPoint.F)
                {
                    curPoint = point;
                }
            }
            return curPoint;
        }
        return null;
    }//ͨ������ȥ�ҵ���С��F


    Point findPath(Point startPoint,Point endPoint)
    {
        openList.Add(startPoint);
        while (openList.Count > 0)
        {
            Point curPoint = FindleastF();//��ȡ��ǰopenList��Fֵ��С�ĵ�
            openList.Remove(curPoint);//����ǰ���OpenList���Ƴ�
            closeList.Add(curPoint);//��ӵ��ձ���
            List<Point> surroundPoints = GetSurround4Point(curPoint);//��ȡ��Χ�ĵ�

            foreach(var target in surroundPoints)
            {
                if (isInList(openList, target) == null)//����Χ�㲻��openList�У������openList,��������G,F,Hֵ
                {
                    target.parent = curPoint;
                    target.G = CalculateG(target, curPoint);
                    target.H = CalculateH(target, endPoint);
                    target.F = CalculateF(target);

                    openList.Add(target);
                }
                else//�Ѿ���openList�У��������Gֵ��Fֵ����ΪHֵ��parent�޹أ����ù�
                {
                    Point temp = isInList(openList, target);
                    float tempG = CalculateG(target, curPoint);
                    if (tempG < temp.G)
                    {
                        target.parent = curPoint;
                        target.G = tempG;
                        target.F = CalculateF(target);
                    }
                }
            }
            Point resPoint = isInList(openList, endPoint);//���Ŀ�����openList�У���˵���ҵ���·��
            if (resPoint != null)
            {
                GizmosList = new List<Point>(openList);
                foreach(var p in closeList)
                {
                    GizmosList.Add(p);
                }
                //�ڵ�ͼ�ϻ�������·��
                return resPoint;
            }
        }
        return null;//����null��˵��û��·����Ŀ��㲻�ɴ
    }

    public List<Point> GetPath(Point start, Point end)
    {
        Point result = findPath(start, end);
        List<Point> Path = new List<Point>();
        while (result != null)
        {
            Path.Add(result);
            result = result.parent;
        }
        Path.Reverse();
        openList.Clear();
        //openlistһ�������GizmosListҲ�����������Ϊǰ�渳ֵ����������GizmosListָ����openList,Ҳ���������������ϣ����ǳ������
        closeList.Clear();
        return Path;
    }

    Point isInList(List<Point> list, Point point)
    {
        foreach (var p in list)
        {
            if (p.x == point.x && p.y == point.y)
                return p;
        }
        return null;
    }

    List<Point> GetSurround4Point(Point curPoint){
        List<Point> surPoint = new();
        List<Point> allPoint = new(){
            new Point(1 + curPoint.x,  0 + curPoint.y),
            new Point(-1 + curPoint.x, 0 + curPoint.y),
            new Point(0 + curPoint.x,  1 + curPoint.y),
            new Point(0 + curPoint.x, -1 + curPoint.y)
        };
        foreach(var p in allPoint){
            if(p.x < 0 || p.x >= Size) continue;
            if(p.y < 0 || p.y >= Size) continue;
            if(map[p.x,p.y]==1) continue;// ���ϰ�
            if(isInList(closeList, p) !=null ) continue;
            surPoint.Add(p);
        }
        return surPoint;
    }

    List<Point> GetSurround8Point(Point curPoint)
    {
        List<Point> surPoint = new List<Point>();
        for(int i = -1; i <= 1; i++)
        {
            for(int j = -1; j <= 1; j++)//
            {
                if (i == 0 && j == 0) continue;//��������
                if (curPoint.x + i < 0 ||
                    curPoint.x + i > Size - 1 ||
                    curPoint.y + j < 0 ||
                    curPoint.y + j > Size - 1 || // �ڷ�Χ��
                    map[curPoint.x+i,curPoint.y+j]==1 || // ���ϰ�
                    isInList(closeList, new Point(curPoint.x + i, curPoint.y + j)) !=null ){
                    continue;
                }
                // ���ֱ�������б��
                if (i == -1 && j == -1)
                {
                    if (map[curPoint.x + i + 1, curPoint.y + j] == 1 && map[curPoint.x + i, curPoint.y + j + 1] == 1) continue;
                }
                if (i == 1 && j == -1)
                {
                    if (map[curPoint.x + i - 1, curPoint.y + j] == 1 && map[curPoint.x + i, curPoint.y + j + 1] == 1) continue;
                }
                if (i == 1 && j == 1)
                {
                    if (map[curPoint.x + i - 1, curPoint.y + j] == 1 && map[curPoint.x + i, curPoint.y + j - 1] == 1) continue;
                }
                if (i == -1 && j == 1)
                {
                    if (map[curPoint.x + i, curPoint.y + j - 1] == 1 && map[curPoint.x + i + 1, curPoint.y + j] == 1) continue;
                }
                surPoint.Add(new Point(curPoint.x + i, curPoint.y + j));
            }
        }
        return surPoint;
    }

}
