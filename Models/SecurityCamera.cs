using System;

namespace SmartHome.Models
{
    public class SecurityCamera : SmartDevice
    {
        public bool IsRecording { get; private set; }

        public SecurityCamera(string name) : base(name, 10) { }

        public override void TurnOn()
        {
            base.TurnOn();
            IsRecording = true;
            Console.WriteLine($"[{Name}] Начинает запись.");
        }

        public override void TurnOff()
        {
            IsRecording = false;
            base.TurnOff();
        }

        public override string GetStatus() => $"{Name}: {(IsOn ? "Вкл" : "Выкл")} | Запись: {(IsRecording ? "идёт" : "нет")} | {GetPowerConsumption()} Вт";
    }
}
