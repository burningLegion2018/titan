using common.game.engine.unit;
using System.Collections.Generic;
using UnityEngine;

namespace com.game.battle.unit
{
    public class Hero : MonoBehaviour
    {
        private Unit module;
        public Unit Module { get { return module; } }
        private common.game.engine.Vector3 bronPoint;
        public common.game.engine.Vector3 BronPoint { get { return bronPoint; } set { bronPoint = value; } }
        private List<IController> controllers;
        void Start()
        {
            module = new Unit(bronPoint);
            controllers = new List<IController>();
            BindController();
        }
        public void Update()
        {
            updateController();
        }
        /// <summary>
        /// 绑定controller
        /// </summary>
        public void BindController()
        {
            MoveController moveController = new MoveController(module, transform);
            module.MoveController = moveController;
            controllers.Add(moveController);
        }
        /// <summary>
        /// 执行Controller
        /// </summary>
        private void updateController()
        {
            for (int i = 0; i < controllers.Count; i++)
                controllers[i].Update();
        }
    }
}