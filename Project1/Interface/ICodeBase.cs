/*
 * 커맨드 패턴으로 서로 다른 클래스들을 한 클래스에서 호출하기 위한 인터페이스
 */
public interface ICodeBase
{
	//내부적으로 클래스를 구분하는 단위. 소문자로 처리하며, 공백을 허용하지 않는다.
	string Name { get; }

	/// <summary>
	/// 유저의 입력 값에 따라 실행되는 함수. 일반적인 프로그램의 Main처럼 작동한다.
	/// </summary>
	/// <param name="args">실행 인자 값(0번은 무조건 클래스의 이름, 즉 ICodeBase.Name이다)</param>
	/// <returns>실행 결과 문구</returns>
	string Execute(string[] args);
}
