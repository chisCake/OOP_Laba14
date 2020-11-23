namespace OOP_Laba5 {
	[System.Serializable]
	public class Tablet : Computer {
		public double DisplaySize { get; set; }
		public (int X, int Y) DisplayResolution { get; set; }
		public int BatteryCapacity { get; set; }

		[System.NonSerialized]
		public readonly int PxlsAmt;

		public Tablet() :
			base(default, default, default, default, default, default, default, default, default, default, default, default) {
			DisplaySize = default;
			DisplayResolution = default;
			BatteryCapacity = default;
			PxlsAmt = DisplayResolution.X * DisplayResolution.Y;
		}

		public Tablet(
			string brand,
			string name,
			string color,
			double price,
			double weight,
			(int Width, int Height, int Length) dimensions,
			double powerConsumption,
			string os,
			string cpu,
			string gpu,
			double ram,
			double storageCapacity,
			double displaySize,
			(int X, int Y) displayResolution,
			int batteryCapacity) :
			base(brand, name, color, price, weight, dimensions, powerConsumption, os, cpu, gpu, ram, storageCapacity) {
			DisplaySize = displaySize;
			DisplayResolution = displayResolution;
			BatteryCapacity = batteryCapacity;
			PxlsAmt = displayResolution.X * displayResolution.Y;
		}

		public override string ToString() {
			return "Планшет\n" + base.ToString() + $"\nДиагональ экрана: {DisplaySize}\"; Разрешение экрана: {DisplayResolution.X}*{DisplayResolution.Y}";
		}
	}
}
