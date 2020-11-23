namespace OOP_Laba5 {
	enum PrintingTechnology {
		Laser,
		Inkjet,
		LED,
		Matrix
	}
	
	[System.Serializable]
	class PrintingDevice : Technique {
		public PrintingTechnology PrintingTechnology { get; private set; }
		public (int X, int Y) MaxPrintingResolution { get; private set; }
		public double PrintingSpeed { get; private set; }

		public PrintingDevice(
			string brand,
			string name,
			string color,
			double price,
			double weight,
			(int Width, int Height, int Length) dimensions,
			double powerConsumption,
			PrintingTechnology printingTechnology,
			(int X, int Y) maxPrintingResolution,
			double printingSpeed) :
			base(brand, name, color, price, weight, dimensions, powerConsumption) {
			PrintingTechnology = printingTechnology;
			MaxPrintingResolution = maxPrintingResolution;
			PrintingSpeed = printingSpeed;
		}

		public override string ToString() {
			return "Печатающее устройство\n" + base.ToString() + $"\nТехнология печати: {PrintingTechnology}; Максимальное разрешение: {MaxPrintingResolution.X}*{MaxPrintingResolution.Y} dpi; Скорость печати: {PrintingSpeed} стр/мин";
		}
	}
}
