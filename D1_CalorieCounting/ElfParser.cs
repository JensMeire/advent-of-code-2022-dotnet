using System;
namespace D1_CalorieCounting
{
	public static class ElfParser
	{
		public static List<Elf> Parse(string filePath)
		{
			var elfs = new List<Elf>();
			var foods = new List<Food>();
			foreach(var line in File.ReadLines(filePath))
			{
				if (string.IsNullOrEmpty(line))
				{
					elfs.Add(CreateElf(foods));
					foods = new List<Food>();
					continue;
				}

				var food = new Food();
				food.Calories = int.Parse(line);
				foods.Add(food);
			}

            elfs.Add(CreateElf(foods));

            return elfs;
		}

        public static Elf CreateElf(List<Food> foods)
        {
            var elf = new Elf();
            elf.Inventory = foods;
            return elf;

        }
    }
}