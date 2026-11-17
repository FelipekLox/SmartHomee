using System;

namespace SmartHome.Models
{
    public class Thermostat : SmartDevice
    {
        public double TargetTemperature { get; private set; } = 22.0;

        public Thermostat(string name) : base(name, 1500) { }

        public override void TurnOn()
        {
            base.TurnOn();
            Console.WriteLine($"[{Name}] Температура: {TargetTemperature}°C.");
        }

        public override string GetStatus() => $"{Name}: {(IsOn ? "Вкл" : "Выкл")} | Температура: {TargetTemperature}°C | {GetPowerConsumption()} Вт";
    }
}
