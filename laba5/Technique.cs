using System;

namespace OOP_Laba5 {
	[Serializable]
	public abstract class Technique : Product, IGetInfo {
		public double PowerConsumption { get; set; }

		public Technique(
			string brand,
			string name,
			string color,
			double price,
			double weight,
			(int Width, int Height, int Length) dimensions,
			double powerConsumption) :
			base(brand, name, color, price, weight, dimensions) {
			PowerConsumption = powerConsumption;
		}

		public override string ToString() {
			return base.ToString() + $"\nПотребляемая мощность: {PowerConsumption} Вт";
		}

		void IGetInfo.GetInfo() {
			Console.WriteLine(ToString());
		}

		public override void GetInfo() {
			Console.WriteLine(ToString());
		}

	}
	interface IGetInfo {
		void GetInfo();
	}
}
