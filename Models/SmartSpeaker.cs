using System;

namespace SmartHome.Models
{
    public class SmartSpeaker : SmartDevice
    {
        public int Volume { get; private set; } = 50;

        public SmartSpeaker(string name) : base(name, 20) { }

        public override void TurnOn()
        {
            base.TurnOn();
            Console.WriteLine($"[{Name}] Громкость: {Volume}%.");
        }

        public override string GetStatus() => $"{Name}: {(IsOn ? "Вкл" : "Выкл")} | Громкость: {Volume}% | {GetPowerConsumption()} Вт";
    }
}
