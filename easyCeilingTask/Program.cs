using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyCeilingTask {
	class Program {
		#region Task1Array
		static Random rnd = new Random();
		/*
		 * FillArray
		 * Заполнение массива неповторяющимися числами в случайном порядке. Идея: выбираем случайный индекс, в котором еще нет числа, и записываем туда значение. Затем вычеркиваем индекс из списка. Если просто рандомить индекс - неизвестно, с какой попытки мы попадем в незанятое место. А так мы сразу знаем, какие индексы не заняты.
		 * 
		 */
		static void FillArray(out int[] array) {
			int size = 100;
			array = new int[size];
			List<int> indexes = new List<int>();
			for (int i = 0; i < size; i++) {
				indexes.Add(i);
			}
			for (int i = 0; i < size; i++) {
				int index = indexes[rnd.Next(0, indexes.Count)];
				array[index] = i + 1;
				indexes.Remove(index);
			}
			int from = 0, to = 0;
			while (from == to) {
				from = rnd.Next(0, array.Length - 1);
				to = rnd.Next(0, array.Length - 1);
			}
			array[from] = array[to];
		}
		/*
		 * FindDuplicate
		 * Поиск повторяющегося елемента
		 * Идея: зная, что дубликат всего 1 -> повторящихся елемента всего 2. Группируем массив поэллементно, из полученного Enumerable вытаскиваем группу, в которой 2 элемента и берем первый из них. Не использую FirstOfDefault и обработку исключения, т.к известно, что дубликат есть 100%
		 */
		static int FindDuplicate(int[] arr) {
			return arr.GroupBy(elem => elem).Where(group => group.Count() == 2).First().First();
		}
		static string ArrayPrint(int[] array) {
			return string.Join(",", array);
		}
		#endregion
		#region Task2FigureRotating
		class Point {
			private double x, y, z;
			public double X {
				get { return x; }
				set { x = value; }
			}
			public double Y {
				get { return y; }
				set { y = value; }
			}
			public double Z {
				get { return z; }
				set { z = value; }
			}
			public Point(int x, int y) {
				X = x;
				Y = y;
				Z = 1;
			}
		}
		class Figure {
			private Point[] coords;
			public double this[int index, int componenet] {
				get {
					switch (componenet) {
						case 0:
							return coords[index].X;
						case 1:
							return coords[index].Y;
						case 2:
							return coords[index].Z;
					}
					return -1;
				}
				set {
					switch (componenet) {
						case 0:
							coords[index].X = value;
							break;
						case 1:
							coords[index].Y = value;
							break;
						case 2:
							coords[index].Z = value;
							break;
					}
				}
			}
			public Figure(int[,] points) {
				coords = new Point[points.GetLength(0)];
				for (int i = 0; i < coords.GetLength(0); i++) {
					coords[i] = new Point(points[i, 0], points[i, 1]);
				}
			}
			public void Rotate(double angle) {
				var fi  = angle * Math.PI / 180;
				double[,] matrix = {
					{ Math.Cos(fi), Math.Sin(fi), 0 },
					{ -Math.Sin(fi), Math.Cos(fi), 0 },
					{ 0, 0, 1 }
				};
				double[,] res = new double[coords.Length, matrix.GetLength(0)];
				for (int i = 0; i < coords.Length; i++) {
					for (int j = 0; j < matrix.GetLength(1); j++) {
						for (int k = 0; k < matrix.GetLength(0); k++) {
							res[i, j] += this[i, k] * matrix[k, j];
						}
					}
				}
				for (int i = 0; i < res.GetLength(0); i++) {
					for (int j = 0; j < res.GetLength(1); j++) {
						this[i, j] = res[i, j];
					}
				}
			}
			public override string ToString() {
				return string.Join("\n", coords.Select(fig => $"X:{fig.X.ToString("F"+0)} Y:{fig.Y.ToString("F" + 0)} Z:{fig.Z.ToString("F" + 0)}").ToList());
			}
		}
		#endregion
		static void Main(string[] args) {
			int[] arr;
			FillArray(out arr);
			Console.WriteLine(ArrayPrint(arr));
			Console.WriteLine($"Дубль = {FindDuplicate(arr)}");
			Figure figure = new Figure(new int[,] { { 3, -1 }, { 4, 1 }, { 2,1 }});
			Console.WriteLine("Координаты точек фигуры до преобразования");
			Console.WriteLine(figure.ToString());
			figure.Rotate(-21);
			Console.WriteLine("Координаты точек фигуры после преобразования");
			Console.WriteLine(figure.ToString());
		}
	}
}
