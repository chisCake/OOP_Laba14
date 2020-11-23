namespace OOP_Laba5 {
	[System.Serializable]
	public abstract class Product : IGetInfo {
		public string Brand { get; set; }
		public string Name { get; set; }
		public string Color { get; set; }
		public double Price { get; set; }
		public double Weight { get; set; }
		public (int Width, int Height, int Lenght) Dimensions { get; set; }

		public Product(
			string brand,
			string name,
			string color,
			double price,
			double weight,
			(int Width, int Height, int Length) dimensions) {
			Brand = brand;
			Name = name;
			Color = color;
			Price = price;
			Weight = weight;
			Dimensions = dimensions;
		}

		public override string ToString() {
			return $"Производитель: {Brand}; Название: {Name}; Цвет: {Color}; Цена: {Price} руб; Вес: {Weight} кг; Габариты: {Dimensions.Width}мм, {Dimensions.Height}мм, {Dimensions.Lenght}мм";
		}

		public abstract void GetInfo();
	}
}
