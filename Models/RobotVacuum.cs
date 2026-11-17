using System;

namespace SmartHome.Models
{
    public class RobotVacuum : SmartDevice
    {
        public int BatteryLevel { get; private set; }

        public RobotVacuum(string name, int batteryLevel = 100) : base(name, 45)
        {
            BatteryLevel = batteryLevel;
        }

        public override void TurnOn()
        {
            base.TurnOn();
            Console.WriteLine($"[{Name}] Уровень заряда: {BatteryLevel}%.");
        }

        public override string GetStatus() => $"{Name}: {(IsOn ? "Вкл" : "Выкл")} | Заряд: {BatteryLevel}% | {GetPowerConsumption()} Вт";
    }
}
