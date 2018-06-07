﻿using common.game.engine.effect;
using common.game.engine.unit;
using System;

namespace common.game.engine.action
{
    /// <summary>
    /// 移动动作
    /// </summary>
    public class MoveAction : IAction
    {
        float? speed;
        Vector3 vector3;
        public MoveAction(float? speed,Vector3 vector3)
        {
            this.speed = speed;
            this.vector3 = vector3;
        }
        public IEffect Proc(Unit unit,DateTime runtime)
        {
            return new MoveEffect(unit,speed,vector3);
        }
    }
}