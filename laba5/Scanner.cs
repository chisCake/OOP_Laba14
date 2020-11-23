namespace OOP_Laba5 {
	[System.Serializable]
	class Scanner : Technique{
		public (int X, int Y) MaxScanningResolution { get; private set; }
		public int ColorDepth { get; private set; }

		public Scanner(
			string brand,
			string name,
			string color,
			double price,
			double weight,
			(int Width, int Height, int Length) dimensions,
			double powerConsumption,
			(int X, int Y) maxScanningResolution,
			int colorDepth) :
			base(brand, name, color, price, weight, dimensions, powerConsumption) {
			MaxScanningResolution = maxScanningResolution;
			ColorDepth = colorDepth;
		}

		public override string ToString() {
			return "Сканер\n" + base.ToString() + $"\nМаксимальное разрешение: {MaxScanningResolution.X}*{MaxScanningResolution.Y}; Глубина цвета: {ColorDepth} бит";
		}
	}
}
