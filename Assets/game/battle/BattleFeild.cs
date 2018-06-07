using common.game.engine.runner;
using common.game.engine.scene;
using common.game.engine.unit;

namespace com.game.battle
{
    public class BattleFeild
    {
        private GameRunner battleField;
        private Scene sence;
        public BattleFeild()
        {
            sence = new Scene();
            battleField = new GameRunner(sence);
        }
        void Start()
        {
            battleField.Start();
        }
        public void AddUnit(Unit unit)
        {
            sence.Add(unit);
        }
        void Destroy()
        {
            battleField.Stop();
        }
    }
}