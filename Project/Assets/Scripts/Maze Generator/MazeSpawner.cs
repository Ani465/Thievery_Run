using UnityEngine;

//<summary>
//Game object, that creates maze and instantiates it in scene
//</summary>
namespace Maze_Generator
{
	public class MazeSpawner : MonoBehaviour
	{
		public enum MazeGenerationAlgorithm
		{
			PureRecursive,
			RecursiveTree,
			RandomTree,
			OldestTree,
			RecursiveDivision,
		}

		public MazeGenerationAlgorithm algorithm = MazeGenerationAlgorithm.PureRecursive;
		public GameObject floor;
		public GameObject wall;
		public int rows = 10;
		public int columns = 10;
		public float cellWidth = 10;
		public float cellHeight = 10;
		public bool addGaps;
	
		private BasicMazeGenerator _mMazeGenerator;

		private void Start()
		{
			_mMazeGenerator = algorithm switch
			{
				MazeGenerationAlgorithm.PureRecursive => new RecursiveMazeGenerator(rows, columns),
				MazeGenerationAlgorithm.RecursiveTree => new RecursiveTreeMazeGenerator(rows, columns),
				MazeGenerationAlgorithm.RandomTree => new RandomTreeMazeGenerator(rows, columns),
				MazeGenerationAlgorithm.OldestTree => new OldestTreeMazeGenerator(rows, columns),
				MazeGenerationAlgorithm.RecursiveDivision => new DivisionMazeGenerator(rows, columns),
				_ => _mMazeGenerator
			};

			_mMazeGenerator.GenerateMaze();
			for (var row = 0; row < rows; row++)
			{
				for (var column = 0; column < columns; column++)
				{
					var x = column * (cellWidth + (addGaps ? .2f : 0));
					var z = row * (cellHeight + (addGaps ? .2f : 0));
					var cell = _mMazeGenerator.GetMazeCell(row, column);
					var tmp = Instantiate(floor, new Vector3(x, 0, z), Quaternion.Euler(0, 0, 0)) as GameObject;
					tmp.transform.parent = transform;
					if (cell.WallRight)
					{
						tmp = Instantiate(wall, new Vector3(x + cellWidth / 2, 0, z) + wall.transform.position,
							Quaternion.Euler(0, 90, 0)) as GameObject; // right
						tmp.transform.parent = transform;
					}

					if (cell.WallFront)
					{
						tmp = Instantiate(wall, new Vector3(x, 0, z + cellHeight / 2) + wall.transform.position,
							Quaternion.Euler(0, 0, 0)) as GameObject; // front
						tmp.transform.parent = transform;
					}

					if (cell.WallLeft)
					{
						tmp = Instantiate(wall, new Vector3(x - cellWidth / 2, 0, z) + wall.transform.position,
							Quaternion.Euler(0, 270, 0)) as GameObject; // left
						tmp.transform.parent = transform;
					}

					if (cell.WallBack)
					{
						tmp = Instantiate(wall, new Vector3(x, 0, z - cellHeight / 2) + wall.transform.position,
							Quaternion.Euler(0, 180, 0)) as GameObject; // back
						tmp.transform.parent = transform;
					}
				}
			}
		}
	}
}
