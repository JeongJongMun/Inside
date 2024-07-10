/* ISubject.cs
 * 옵저버 패턴의 주제가 되는 인터페이스
 * 옵저버들을 추가, 제거, 알림을 위한 메서드를 가짐
 */
public interface ISubject
{
    void AddObserver(Observer observer);
    void RemoveObserver(Observer observer);
    void Notify(Define.TrickName trickName);
}