using System;
namespace D1_CalorieCounting
{
	public class Elf
	{
		public List<Food> Inventory { get; set; }

		public Elf()
		{
			Inventory = new List<Food>();
		}

		public int TotalCalories => Inventory.Sum(x => x.Calories);
	}
}

