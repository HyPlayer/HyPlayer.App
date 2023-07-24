using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.App;
public class User
{
    public int Id { get; set; }
    public string Name { get; set; }

    public static User CreateSample()
    {
        var id = new Random().Next(100);
        return new User { Id = id, Name=$"UserName{id}" };
    }
}
