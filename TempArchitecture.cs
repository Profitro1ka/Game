namespace MyGame;


//наверное в моногейме есть чет подобное но пока для себя расписываю как примерно будет выгляить программа
//Это все дело в модели, а вью и контроллер это приколы моногейма(наверное)
public class Point
{
    public int X { get; }
    public int Y { get; }
}

public interface IGameObj
{
    public Point Coords { get; }
}

public interface IMovable : IGameObj
{
    public void Move();
    public int Speed { get; }
}

public interface IStatic : IGameObj
{
    //возможно бесполезный интерфейс)))
}

interface IPlayer: IMovable
{
    public void Attack();
    public void Dash();
    
    public int HP { get; }
    public int AttackCD { get; }
    public int RangeAttack { get; }
}

public class Player : IPlayer
{
    public void Move()
    {
        throw new System.NotImplementedException();
    }
}

public interface IEnemy
{
    public void SearchPlayer();
    public void Attack();
    public int HP { get; }
    public int AttackCD { get; }
    public int RangeAttack { get; }
}

public class MovableEnemy : IEnemy, IMovable
{
    public void Move()
    {
        throw new System.NotImplementedException();
    }
}

public class StaticEnemy : IEnemy, IMovable
{
    public void Move()
    {
        throw new System.NotImplementedException();
    }
}

interface IBoss : IEnemy, IMovable
{
    public int WhenSpawn { get; }
}

public class Boss : IBoss
{

}


public class Bullet : IMovable
{
    
}

public interface ILocation : IGameObj
{
    public int[,] Size { get; }
    public string Name { get; }
    public Point Pass { get; }
}

public interface IHub
{
    public INPC[] Npcs { get; }
}

interface IGeneratorLocation
{
    public void MakePassageway();
}

public interface INPC : IStatic
{
    IShop Shop { get; }
    public void Sell();
    public void OpenShop();
}

public interface IShop
{
    // буду реаизовывать вместе с прокачкой перса
}

public interface IMenuObj
{
    // беспонятия как это будет реализовано
}

public interface IButton : IMenuObj
{
    
}

public interface IOptions : IMenuObj
{
    
}

