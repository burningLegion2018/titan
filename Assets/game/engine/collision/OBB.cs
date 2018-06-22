using common.game.engine;
using System;
/// <summary>
/// 矩形OBB碰撞盒
/// </summary>
public class OBB
{
    private Vector3 centerPoint;//中心点
    private float halfWidth;//宽半径
    private float halfHeight;//高半径
    private Vector3 axisX;//矩形x投影轴向量
    private Vector3 axisY;//矩形y投影轴向量
    private float rotation;//矩形旋转角度
    public OBB(Vector3 bronCenterPoint,float halfWidth,float halfHeight,float rotation)
    {
        centerPoint = bronCenterPoint;
        this.halfWidth = halfWidth;
        this.halfHeight = halfHeight;
        SetRotation(rotation);
    }
    public OBB(Vector3 bronCenterPoint, float halfWidth, float halfHeight):this(bronCenterPoint,halfWidth,halfHeight,0.0f)
    {
    }
    public OBB(float halfWidth, float halfHeight) : this(new Vector3(0.0f,0.0f,0.0f), halfWidth, halfHeight, 0.0f)
    {
    }
    /// <summary>
    /// 设置旋转角度
    /// </summary>
    /// <param name="rotation"></param>
    /// <returns></returns>
    public OBB SetRotation(float rotation)
    {
        this.rotation = rotation;
        axisX = new Vector3((float)Math.Cos((float)rotation), (float)Math.Sin((float)rotation), 0);
        axisY = new Vector3(-(float)Math.Sin((float)rotation), (float)Math.Cos((float)rotation), 0);
        return this;
    }
    /// <summary>
    /// 向量点乘
    /// </summary>
    /// <param name="axisA"></param>
    /// <param name="axisB"></param>
    /// <returns></returns>
    private float Dot(Vector3 axisA, Vector3 axisB)
    {
        return Math.Abs(axisA.X * axisB.X + axisA.Y * axisB.Y);
    }
    /// <summary>
    /// 获得在向量上得投影半径
    /// </summary>
    /// <param name="axis"></param>
    /// <returns></returns>
    public float GetProjectionRadius(Vector3 axis)
    {
        float projectionAxisX = Dot(axis,axisX);
        float projectionAxisY = Dot(axis, axisY);
;       return halfWidth * projectionAxisX + halfHeight * projectionAxisY;
    }
    /// <summary>
    /// 是否发生碰撞
    /// </summary>
    /// <param name="obb"></param>
    /// <returns></returns>
    public bool IsCollision(OBB obb)
    {
        Vector3 centerDistanceVector3 = Vector3.Reduce(centerPoint,obb.centerPoint);
        Vector3[] axises = { axisX, axisY, obb.axisX, obb.axisY };
        for(int i=0;i<axises.Length;i++)
        {
            if (GetProjectionRadius(axises[i]) + obb.GetProjectionRadius(axises[i]) <= Dot(centerDistanceVector3, axises[i]))
                return false;
        }
        return true;
    }
}