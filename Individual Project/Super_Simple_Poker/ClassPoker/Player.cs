using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperSimplePoker;
namespace SuperSimplePoker;


public class Player : IPlayer
{
    public int Id { get; set; }
    public string Name { get; set; }

    public Player(int id, string name)
    {
        Id = id;
        Name = name;
    }
}


