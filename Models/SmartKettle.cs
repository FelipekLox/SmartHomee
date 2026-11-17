using System;

namespace SmartHome.Models
{
    // Класс помечен как sealed, так как это конечное устройство с фиксированной логикой нагрева,
    // и дальнейшее наследование от него не предполагается.
    public sealed class SmartKettle : SmartDevice
    {
        public int WaterTemperature { get; private set; }

        public SmartKettle(string name, int waterTemperature = 100) : base(name, 2000)
        {
            WaterTemperature = waterTemperature;
        }

        public override void TurnOn()
        {
            base.TurnOn();
            Console.WriteLine($"[{Name}] Начинает нагрев до {WaterTemperature}°C.");
        }

        public override string GetStatus() => $"{Name}: {(IsOn ? "Вкл" : "Выкл")} | Нагрев до: {WaterTemperature}°C | {GetPowerConsumption()} Вт";
    }
}
