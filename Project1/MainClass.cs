using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

/*
 * 다양한 디자인 패턴 예제를 실행해볼 수 있도록 지원하는 클래스
 * 
 * 실행 시 가장 먼저 실행되며, 커맨드 패턴으로 구현되었다.
 */
public class MainClass
{
	/*
	 * 유저의 입력값을 토대로 적절한 클래스를 동작시키는 클래스
	 * 
	 * 리플렉션을 활용해 ICodeBase를 베이스로 만들어진 클래스를 모두 불러온다.
	 */
	public class CodeManager
	{
		//입력값과 ICodeBase를 연결해주는 딕셔너리
		public Dictionary<string, ICodeBase> codes = new Dictionary<string, ICodeBase>();

		/// <summary>
		/// 리플렉션을 활용해 ICodeBase인 클래스들을 딕셔너리에 등록한다.
		/// </summary>
		public void RegisterCode()
		{
			Type codeType = typeof(ICodeBase);
			IEnumerable<Type> types = Assembly.GetExecutingAssembly().GetTypes()
										.Where(type => type.GetInterfaces().Contains(codeType) && !type.IsAbstract);

			foreach (Type type in types)
			{
				ICodeBase command = Activator.CreateInstance(type) as ICodeBase;
				if (command != null && !codes.ContainsKey(command.Name.ToLower()))
				{
					codes.Add(command.Name.ToLower(), command);
				}
			}
		}

		/// <summary>
		/// 유저의 입력 값에 따라 적절한 클래스를 호출한다.
		/// 이 과정에서 유저의 입력 값을 공백에 따라 나누어 실행 함수에 인자로 전달한다.
		/// </summary>
		/// <param name="input">유저의 입력 값</param>
		public void ExecuteCode(string input)
		{
			string[] args = input.Split(' ');

			Console.WriteLine();
			if (codes.ContainsKey(args[0]))
			{
				Console.WriteLine($"Result: {codes[args[0]].Execute(args)}");
			}
			else
			{
				Console.WriteLine($"Result: wrong value => {args[0]}");
			}
			Console.WriteLine("----------\n");
			return;
		}
	}

	public static int Main(string[] args)
	{
		CodeManager codeManager = new CodeManager();
		string input;

		codeManager.RegisterCode();
		while (true)
		{
			Console.Write("=>");
			input = Console.ReadLine().Trim().ToLower();
			if (input.Equals("break") || input.Equals("exit"))
				break;
			codeManager.ExecuteCode(input);
		}
		return (0);
	}
}

