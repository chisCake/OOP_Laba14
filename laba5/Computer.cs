namespace OOP_Laba5 {
	[System.Serializable]
	public class Computer : Technique {
		public string OS { get; set; }
		public string CPU { get; set; }
		public string GPU { get; set; }
		public double RAM { get; set; }
		public double StorageCapacity { get; set; }

		public Computer (
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
			double storageCapacity) :
			base(brand, name, color, price, weight, dimensions, powerConsumption) {
			OS = os;
			CPU = cpu;
			GPU = gpu;
			RAM = ram;
			StorageCapacity = storageCapacity;
		}

		public override string ToString() {
			return "Компьютер\n" + base.ToString() + $"\nОС: {OS}; ЦПУ: {CPU}; ГПУ: {GPU}; Вместимость хранилища: {StorageCapacity} ГБ";
		}
	}
}
