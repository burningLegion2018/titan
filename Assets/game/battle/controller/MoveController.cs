using common.game.engine.controller;
using common.game.engine.unit;
using UnityEngine;

public class MoveController : IMoveController,IController
{
    private Unit unit;
    private Transform transform;
    private Vector3 position;
    public MoveController(Unit unit, Transform transform)
    {
        this.unit = unit;
        this.transform = transform;
        position = new Vector3(unit.BronPoint.X, unit.BronPoint.Y, unit.BronPoint.Z);
    }
    public void move(common.game.engine.Vector3 position)
    {
        this.position = new Vector3(position.X, position.Y, position.Z);
    }
    public void Update()
    {
        transform.position = position;
    }
}